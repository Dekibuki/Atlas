using Atlas.API.Requests.CustomerApplication;
using Atlas.API.Responses.CustomerApplication;
using Atlas.Domain.Enum;
using Atlas.Domain.Models.CustomerApplication;
using Atlas.Repository.ApplicationsRepository;
using Atlas.Services.ApplicationsService;
using Atlas.Services.IDNowService;
using Atlas.Services.RefinitivWorldCheckService;
using Microsoft.Extensions.Logging;
using Moq;
using Atlas.Services.AccountsService;
using Atlas.Services.CardsService;
using Atlas.API.DTO;
using Atlas.API.Responses.IDNow;
using Atlas.API.Responses.RefinitivWorldCheck;

namespace Atlas.Tests
{
    public class ApplicationsServiceTests
    {
        [Fact]
        public async Task CreateApplication_IdentityAndScreeningClear_ReturnsApproved()
        {
            // Arrange
            var idNowMock = new Mock<IIDNowService>();
            var worldCheckMock = new Mock<IRefinitivWorldCheckService>();
            var repoMock = new Mock<IApplicationsRepository>();
            var loggerMock = new Mock<ILogger<ApplicationService>>();
            var accountsServiceMock = new Mock<IAccountsService>();
            var cardsServiceMock = new Mock<ICardsService>();

            idNowMock.Setup(x => x.ProcessIdentifications(It.IsAny<IDNowDocumentDto>()))
                .ReturnsAsync(new IDNowDocumentResponse { DocumentResult = DocumentResult.VALID, FaceMatch = true, Confidance = 0.9f, IdentificationId = "id" });

            worldCheckMock.Setup(x => x.ProcessScreening(It.IsAny<RefinitivWorldCheckScreeningDto>()))
                .ReturnsAsync(new RefinitivWorldCheckResponse { Status = ScreeningResult.CLEAR, CaseId = "case" });

            repoMock.Setup(x => x.CreateCustomerApplication(It.IsAny<CustomerApplication>())).ReturnsAsync(123);
            repoMock.Setup(x => x.CreateCustomerDocuments(It.IsAny<List<ApplicationDocument>>())).Returns(Task.CompletedTask);

            var service = new ApplicationService(loggerMock.Object, idNowMock.Object, worldCheckMock.Object, repoMock.Object, accountsServiceMock.Object, cardsServiceMock.Object);

            var request = new CustomerApplicationRequest
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                Country = Country.MA,
                Documents = new List<ApplicationDocumentRequest>
                {
                    new ApplicationDocumentRequest { Type = DocumentType.PASSPORT, Image = new byte[] { 1 } },
                    new ApplicationDocumentRequest { Type = DocumentType.SELFIE, Image = new byte[] { 2 } }
                }
            };

            // Act
            CustomerApplicationResponse result = await service.CreateApplication(request);

            // Assert
            Assert.Equal(123, result.ApplicationId);
            Assert.Equal(ApplicationStatus.APPROVED.ToString(), result.Status);
            repoMock.Verify(x => x.CreateCustomerApplication(It.IsAny<CustomerApplication>()), Times.Once);
            repoMock.Verify(x => x.CreateCustomerDocuments(It.IsAny<List<ApplicationDocument>>()), Times.Once);
        }

        [Fact]
        public async Task CreateApplication_WhenIdentityInvalid_ReturnsRejected()
        {
            // Arrange
            var idNowMock = new Mock<IIDNowService>();
            var worldCheckMock = new Mock<IRefinitivWorldCheckService>();
            var repoMock = new Mock<IApplicationsRepository>();
            var loggerMock = new Mock<ILogger<ApplicationService>>();
            var accountsServiceMock = new Mock<IAccountsService>();
            var cardsServiceMock = new Mock<ICardsService>();

            idNowMock.Setup(x => x.ProcessIdentifications(It.IsAny<IDNowDocumentDto>()))
                .ReturnsAsync((IDNowDocumentResponse?)null);

            worldCheckMock.Setup(x => x.ProcessScreening(It.IsAny<RefinitivWorldCheckScreeningDto>()))
                .ReturnsAsync(new RefinitivWorldCheckResponse { Status = ScreeningResult.CLEAR, CaseId = "case" });

            repoMock.Setup(x => x.CreateCustomerApplication(It.IsAny<CustomerApplication>())).ReturnsAsync(200);
            repoMock.Setup(x => x.CreateCustomerDocuments(It.IsAny<List<ApplicationDocument>>())).Returns(Task.CompletedTask);

            var service = new ApplicationService(loggerMock.Object, idNowMock.Object, worldCheckMock.Object, repoMock.Object, accountsServiceMock.Object, cardsServiceMock.Object);

            var request = new CustomerApplicationRequest
            {
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = DateTime.UtcNow.AddYears(-25),
                Country = Country.MA,
                Documents = new List<ApplicationDocumentRequest>
                {
                    new ApplicationDocumentRequest { Type = DocumentType.ID_CARD, Image = new byte[] { 1 } },
                    new ApplicationDocumentRequest { Type = DocumentType.SELFIE, Image = new byte[] { 2 } }
                }
            };

            // Act
            CustomerApplicationResponse result = await service.CreateApplication(request);

            // Assert
            Assert.Equal(200, result.ApplicationId);
            Assert.Equal(ApplicationStatus.REJECTED.ToString(), result.Status);
            repoMock.Verify(x => x.CreateCustomerApplication(It.IsAny<CustomerApplication>()), Times.Once);
        }
    }
}
