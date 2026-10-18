using Atlas.Domain.Models.CustomerApplication;

namespace Atlas.Services.AccountsService
{
    /// <summary>
    /// Interface for functions regarding manipulating user accounts.
    /// </summary>
    public interface IAccountsService
    {
        public Task CreateAccountFromApplication(CustomerApplication customerApplication);
    }
}
