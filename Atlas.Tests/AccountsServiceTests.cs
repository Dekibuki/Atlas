using Atlas.Domain.Models.CustomerApplication;
using Atlas.Domain.Models.Account;
using Atlas.Repository.AccountsRepository;
using Atlas.Services.AccountsService;
using Atlas.API.DTO;
using Moq;

namespace Atlas.Tests
{
    public class AccountsServiceTests
    {
        [Fact]
        public async Task NoAccountExists_CreateAccount()
        {
            // Arrange
            string firstName = "A";
            string nationalId = "123";

            Mock<IAccountsRepository> repoMock = new Mock<IAccountsRepository>();
            repoMock.Setup(x => x.CheckIfAccountExistsForNationalIdOrNameAndDateOfBirth(It.IsAny<string?>(), It.IsAny<string>(), It.IsAny<System.DateTime>()))
                .ReturnsAsync(false);
            repoMock.Setup(x => x.CreateAccount(It.IsAny<Account>())).Returns(Task.CompletedTask).Verifiable();

            AccountsService service = new AccountsService(repoMock.Object);

            CustomerApplication application = new CustomerApplication
            {
                FirstName = firstName,
                LastName = "B",
                NationalId = nationalId,
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                Email = "a@b.com",
                Phone = "123"
            };

            // Act
            AccountCreationDto dto = new AccountCreationDto
            {
                Name = string.Join(" ", application.FirstName, application.LastName),
                NationalId = application.NationalId,
                DateOfBirth = application.DateOfBirth,
                Email = application.Email,
                Phone = application.Phone
            };

            await service.CreateAccount(dto);

            // Assert
            repoMock.Verify(x => x.CreateAccount(It.Is<Account>(acc => acc.NationalId == nationalId && acc.Name.Contains(firstName))), Times.Once);
        }

        [Fact]
        public async Task AccountExists_SkipCreation()
        {
            // Arrange
            Mock<IAccountsRepository> repoMock = new Mock<IAccountsRepository>();
            repoMock.Setup(x => x.CheckIfAccountExistsForNationalIdOrNameAndDateOfBirth(It.IsAny<string?>(), It.IsAny<string>(), It.IsAny<System.DateTime>()))
                .ReturnsAsync(true);
            repoMock.Setup(x => x.CreateAccount(It.IsAny<Account>())).Returns(Task.CompletedTask).Verifiable();

            AccountsService service = new AccountsService(repoMock.Object);

            CustomerApplication application = new CustomerApplication
            {
                FirstName = "X",
                LastName = "Y",
                NationalId = "999",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
            };

            // Act
            AccountCreationDto dto = new AccountCreationDto
            {
                Name = string.Join(" ", application.FirstName, application.LastName),
                NationalId = application.NationalId,
                DateOfBirth = application.DateOfBirth,
                Email = application.Email,
                Phone = application.Phone
            };

            await service.CreateAccount(dto);

            // Assert
            repoMock.Verify(x => x.CreateAccount(It.IsAny<Account>()), Times.Never);
        }
    }
}
