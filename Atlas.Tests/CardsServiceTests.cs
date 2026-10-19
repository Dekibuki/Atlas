using Atlas.Domain.Models.CustomerApplication;
using Atlas.Domain.Models.Card;
using Atlas.Repository.CardsRepository;
using Atlas.Services.CardsService;
using Moq;
using Atlas.API.DTO;

namespace Atlas.Tests
{
    public class CardsServiceTests
    {
        [Fact]
        public async Task CreateCardFromCustomerApplication_CreatesCard_WhenNotExists()
        {
            // Arrange
            string firstName = "Card";
            string nationalId = "321";

            Mock<ICardsRepository> repoMock = new Mock<ICardsRepository>();
            repoMock.Setup(x => x.CheckIfCardExistsByNationalIdOrNameAndDateOfBirth(It.IsAny<string?>(), It.IsAny<string>(), It.IsAny<System.DateTime>()))
                .ReturnsAsync(false);
            repoMock.Setup(x => x.CreateCard(It.IsAny<Card>())).Returns(Task.CompletedTask).Verifiable();

            CardsService service = new CardsService(repoMock.Object);

            CustomerApplication application = new CustomerApplication
            {
                FirstName = firstName,
                LastName = "User",
                NationalId = nationalId,
                DateOfBirth = DateTime.UtcNow.AddYears(-22),
            };

            // Act
            CardCreationDto dto = new CardCreationDto
            {
                Name = string.Join(" ", application.FirstName, application.LastName),
                NationalId = application.NationalId,
                DateOfBirth = application.DateOfBirth
            };

            await service.CreateCard(dto);

            // Assert
            repoMock.Verify(x => x.CreateCard(It.Is<Card>(c => c.NationalId == nationalId && c.Name.Contains(firstName))), Times.Once);
        }

        [Fact]
        public async Task CreateCardFromCustomerApplication_DoesNotCreate_WhenExists()
        {
            // Arrange
            Mock<ICardsRepository> repoMock = new Mock<ICardsRepository>();
            repoMock.Setup(x => x.CheckIfCardExistsByNationalIdOrNameAndDateOfBirth(It.IsAny<string?>(), It.IsAny<string>(), It.IsAny<System.DateTime>()))
                .ReturnsAsync(true);
            repoMock.Setup(x => x.CreateCard(It.IsAny<Card>())).Returns(Task.CompletedTask).Verifiable();

            CardsService service = new CardsService(repoMock.Object);

            CustomerApplication application = new CustomerApplication
            {
                FirstName = "Existing",
                LastName = "Card",
                NationalId = "555",
                DateOfBirth = DateTime.UtcNow.AddYears(-40),
            };

            // Act
            CardCreationDto dto = new CardCreationDto
            {
                Name = string.Join(" ", application.FirstName, application.LastName),
                NationalId = application.NationalId,
                DateOfBirth = application.DateOfBirth
            };

            await service.CreateCard(dto);

            // Assert
            repoMock.Verify(x => x.CreateCard(It.IsAny<Card>()), Times.Never);
        }
    }
}
