using Atlas.Domain.Models.Account;

namespace Atlas.Repository.AccountsRepository
{
    /// <summary>
    /// Interface for managing user accounts data.
    /// </summary>
    public interface IAccountsRepository
    {
        public Task CreateAccount(Account account);

        public Task<bool> CheckIfAccountExistsForNationalIdOrNameAndDateOfBirth(string? nationalId, string name, DateTime dateOfBirth);
    }
}
