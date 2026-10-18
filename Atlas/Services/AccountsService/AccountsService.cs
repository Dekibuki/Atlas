using Atlas.Domain.Models.Account;
using Atlas.Domain.Models.CustomerApplication;
using Atlas.Repository.AccountsRepository;

namespace Atlas.Services.AccountsService
{
    /// <summary>
    /// Service for functions regarding manipulating user accounts.
    /// </summary>
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }

        public async Task CreateAccountFromApplication(CustomerApplication customerApplication)
        {
            string fullCustomerName = String.Join(" ", customerApplication.FirstName, customerApplication.LastName);

            // One customer should have one account
            if (!await accountsRepository.CheckIfAccountExistsForNationalIdOrNameAndDateOfBirth(
                customerApplication.NationalId,
                fullCustomerName,
                customerApplication.DateOfBirth))
            {
                Account account = new Account
                {
                    Name = fullCustomerName,
                    NationalId = customerApplication.NationalId,
                    DateOfBirth = customerApplication.DateOfBirth,
                    Email = customerApplication.Email,
                    Phone = customerApplication.Phone
                };

                await accountsRepository.CreateAccount(account);
            }
        }
    }
}
