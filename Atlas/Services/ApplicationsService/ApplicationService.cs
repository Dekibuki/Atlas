using Atlas.API.DTO;
using Atlas.API.Requests.CustomerApplication;
using Atlas.API.Responses.CustomerApplication;
using Atlas.API.Responses.IDNow;
using Atlas.API.Responses.RefinitivWorldCheck;
using Atlas.Domain.Enum;
using Atlas.Domain.Models.CustomerApplication;
using Atlas.Repository.ApplicationsRepository;
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
            Status = ApplicationStatus.REJECTED
        };

        private readonly ILogger<ApplicationService> logger;
        private readonly IIDNowService iDNowService;
        private readonly IRefinitivWorldCheckService refinitivWorldCheckService;
        private readonly IApplicationsRepository applicationsRepository;

        public ApplicationService(
            ILogger<ApplicationService> logger,
            IIDNowService iDNowService,
            IRefinitivWorldCheckService refinitivWorldCheckService,
            IApplicationsRepository applicationsRepository)
        {
            this.logger = logger;
            this.iDNowService = iDNowService;
            this.refinitivWorldCheckService = refinitivWorldCheckService;
            this.applicationsRepository = applicationsRepository;
        }

        public async Task<CustomerApplicationResponse> CreateApplication(CustomerApplicationRequest request)
        {
            // Since there is to be no human intervention - if a failure is found, a rejected application is sent back.
            try
            {
                IDNowDocumentDTO customerDocumentData = request.Documents
                    .Where(x => x.Type != DocumentType.SELFIE)
                    .Select(x => new IDNowDocumentDTO { DocumentImage = x.Image, DocumentType = x.Type })
                    .FirstOrDefault()!;

                customerDocumentData.SelfieImage = request.Documents.FirstOrDefault(x => x.Type == DocumentType.SELFIE)!.Image;

                RefinitivWorldCheckScreeningDTO screeningData = new RefinitivWorldCheckScreeningDTO
                {
                    Name = String.Join(" ", request.FirstName, request.LastName),
                    DateOfBirth = request.DateOfBirth,
                    Nationality = request.Country
                };

                bool applicationApproved = await ProcessIdentityAndScreening(customerDocumentData, screeningData);
                ApplicationStatus status = applicationApproved ? ApplicationStatus.APPROVED : ApplicationStatus.REJECTED;

                int applicationId = await InsertApplicationAndDocuments(request, status);

                if (status == ApplicationStatus.APPROVED)
                {
                    // TO DO: Implement card creation and account creation
                }

                return new CustomerApplicationResponse
                {
                    ApplicationId = applicationId,
                    Status = status
                };
            }
            catch (Exception e)
            {
                logger.LogError($"There was an issue during the application process: {e.Message}", e);
                return FailedResponse;
            }
        }

        public async Task<CustomerApplicationListResponse> GetAllApplicationsForCustomer(CustomerApplicationsListRequest request)
        {
            List<CustomerApplication> applications =  await applicationsRepository.GetCustomerApplicationsByNationalIdOrNameAndDateOfBirth(
                                                            request.NationalId,
                                                            request.FirstName,
                                                            request.LastName,
                                                            request.DateOfBirth);

            return new CustomerApplicationListResponse { Applications = applications };
        }

        private async Task<bool> ProcessIdentityAndScreening(IDNowDocumentDTO customerDocumentData, RefinitivWorldCheckScreeningDTO screeningData)
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
                Image = x.Image,
                Type = x.Type,
                CustomerApplicationId = applicationId
            }).ToList();

            await applicationsRepository.CreateCustomerDocuments(applicationDocuments);
            return applicationId;
        }
    }
}
