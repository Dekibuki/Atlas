using Atlas.API.DTO;
using Atlas.API.Responses.Account;

namespace Atlas.Services.AccountsService
{
    /// <summary>
    /// Interface for functions regarding manipulating user accounts.
    /// </summary>
    public interface IAccountsService
    {
        public Task CreateAccount(AccountCreationDto accountDto);

        public Task<AccountsListResponse> GetAllAccounts();
    }
}
