using Atlas.API.DTO;
using Atlas.API.Requests.CustomerApplication;
using Atlas.API.Responses.CustomerApplication;
using Atlas.API.Responses.IDNow;
using Atlas.API.Responses.RefinitivWorldCheck;
using Atlas.Domain.Enum;
using Atlas.Domain.Models.CustomerApplication;
using Atlas.Repository.ApplicationsRepository;
using Atlas.Services.AccountsService;
using Atlas.Services.CardsService;
using Atlas.Services.IDNowService;
using Atlas.Services.RefinitivWorldCheckService;
using System.Net.NetworkInformation;

namespace Atlas.Services.ApplicationsService
{
    /// <summary>
    /// Service implementation regarding application creation, validation and approval.
    /// </summary>
    public class ApplicationService : IApplicationsService
    {
        private const int MaximumRetries = 3;

        private readonly CustomerApplicationResponse FailedResponse = new CustomerApplicationResponse
        {
            ApplicationId = 0,
            Status = ApplicationStatus.REJECTED.ToString()
        };

        private readonly ILogger<ApplicationService> logger;
        private readonly IIDNowService iDNowService;
        private readonly IRefinitivWorldCheckService refinitivWorldCheckService;
        private readonly IApplicationsRepository applicationsRepository;

        private readonly ICardsService cardsService;

        private readonly IAccountsService accountsService;

        public ApplicationService(
            ILogger<ApplicationService> logger,
            IIDNowService iDNowService,
            IRefinitivWorldCheckService refinitivWorldCheckService,
            IApplicationsRepository applicationsRepository,
            IAccountsService accountsService,
            ICardsService cardsService)
        {
            this.logger = logger;
            this.iDNowService = iDNowService;
            this.refinitivWorldCheckService = refinitivWorldCheckService;
            this.applicationsRepository = applicationsRepository;
            this.accountsService = accountsService;
            this.cardsService = cardsService;
        }

        public async Task<CustomerApplicationResponse> CreateApplication(CustomerApplicationRequest request)
        {
            // Since there is to be no human intervention - if a failure is found, a rejected application is sent back.
            try
            {
                IDNowDocumentDto customerDocumentData = request.Documents
                    .Where(x => x.Type != DocumentType.SELFIE)
                    .Select(x => new IDNowDocumentDto { DocumentImage = x.Image, DocumentType = x.Type })
                    .FirstOrDefault()!;

                customerDocumentData.SelfieImage = request.Documents.FirstOrDefault(x => x.Type == DocumentType.SELFIE)!.Image;

                RefinitivWorldCheckScreeningDto screeningData = new RefinitivWorldCheckScreeningDto
                {
                    Name = String.Join(" ", request.FirstName, request.LastName),
                    DateOfBirth = request.DateOfBirth,
                    Nationality = request.Country
                };

                bool applicationApproved = await ProcessIdentityAndScreening(customerDocumentData, screeningData);
                ApplicationStatus status = applicationApproved ? ApplicationStatus.APPROVED : ApplicationStatus.REJECTED;

                int applicationId = await InsertApplicationAndDocuments(request, status);

                // MD is currently not allowed to remotely create account, and therefore card as well.
                if (status == ApplicationStatus.APPROVED && request.Country != Country.MD)
                {
                    await CreateAccountAndCardForRequest(request);
                }

                return new CustomerApplicationResponse
                {
                    ApplicationId = applicationId,
                    Status = status.ToString()
                };
            }
            catch (Exception e)
            {
                logger.LogError($"There was an issue during the application process: {e.Message}", e);
                return FailedResponse;
            }
        }

        public async Task<CustomerApplicationListResponse> GetAllApplications()
        {
            List<CustomerApplication> applications = await applicationsRepository.GetAllCustomerApplications() ?? new List<CustomerApplication>();

            return new CustomerApplicationListResponse { Applications = applications, TotalCount = applications.Count };
        }

        private async Task<bool> ProcessIdentityAndScreening(IDNowDocumentDto customerDocumentData, RefinitivWorldCheckScreeningDto screeningData)
        {
            IDNowDocumentResponse? IDresponse = await iDNowService.ProcessIdentifications(customerDocumentData);
            RefinitivWorldCheckResponse? refinitivRepsonse = await refinitivWorldCheckService.ProcessScreening(screeningData);

            return IDresponse != null
                && IDresponse.DocumentResult == DocumentResult.VALID
                && refinitivRepsonse != null
                && refinitivRepsonse.Status == ScreeningResult.CLEAR;
        }

        private async Task<int> InsertApplicationAndDocuments(CustomerApplicationRequest request, ApplicationStatus status)
        {
            CustomerApplication customerApplication = new CustomerApplication
            {
                CreatedOn = DateTime.Now,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Country = request.Country,
                NationalId = request.NationalId,
                Email = request.Email,
                Phone = request.Phone,
                TermsAccepted = request.TermsAccepted,
                Status = status
            };

            int applicationId = await applicationsRepository.CreateCustomerApplication(customerApplication);
            List<ApplicationDocument> applicationDocuments = request.Documents.Select(x => new ApplicationDocument
            {
                CreatedOn = DateTime.Now,
                Image = x.Image,
                Type = x.Type,
                CustomerApplicationId = applicationId
            }).ToList();

            await applicationsRepository.CreateCustomerDocuments(applicationDocuments);
            return applicationId;
        }

        private async Task CreateAccountAndCardForRequest(CustomerApplicationRequest request)
        {
            string fullName = String.Join(" ", request.FirstName, request.LastName);

            AccountCreationDto accountDto = new AccountCreationDto
            {
                Name = fullName,
                NationalId = request.NationalId,
                DateOfBirth = request.DateOfBirth,
                Phone = request.Phone,
                Email = request.Email
            };

            await accountsService.CreateAccount(accountDto);

            CardCreationDto cardDto = new CardCreationDto
            {
                DateOfBirth = request.DateOfBirth,
                Name = fullName,
                NationalId = request.NationalId
            };

            await cardsService.CreateCard(cardDto);
        }
    }
}
