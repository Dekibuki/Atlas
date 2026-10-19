using Atlas.API.DTO;
using Atlas.API.Responses.Account;
using Atlas.Domain.Models.Account;
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

        public async Task CreateAccount(AccountCreationDto accountDto)
        {
            // One customer should have one account
            if (!await accountsRepository.CheckIfAccountExistsForNationalIdOrNameAndDateOfBirth(
                accountDto.NationalId,
                accountDto.Name,
                accountDto.DateOfBirth))
            {
                Account account = new Account
                {
                    CreatedOn = DateTime.Now,
                    Name = accountDto.Name,
                    NationalId = accountDto.NationalId,
                    DateOfBirth = accountDto.DateOfBirth,
                    Email = accountDto.Email,
                    Phone = accountDto.Phone
                };

                await accountsRepository.CreateAccount(account);
            }
        }

        public async Task<AccountsListResponse> GetAllAccounts()
        {
            List<Account> accounts = await accountsRepository.GetAllAccounts() ?? new List<Account>();
            return new AccountsListResponse { Accounts = accounts, TotalCount = accounts.Count };
        }
    }
}
