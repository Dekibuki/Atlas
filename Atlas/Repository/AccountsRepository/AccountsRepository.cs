using Atlas.Domain.Models.Account;
using Atlas.Repository.AtlastDb;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Repository.AccountsRepository
{
    /// <summary>
    /// Repository for managing user accounts data.
    /// </summary>
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AtlasDbContext atlasDbContext;

        public AccountsRepository(AtlasDbContext atlasDbContext)
        {
            this.atlasDbContext = atlasDbContext;
        }

        public async Task CreateAccount(Account account)
        {
            await atlasDbContext.Accounts.AddAsync(account);
            await atlasDbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfAccountExistsForNationalIdOrNameAndDateOfBirth(string? nationalId, string name, DateTime dateOfBirth)
        {
            return await atlasDbContext.Accounts
                .AnyAsync(x => (nationalId != null && nationalId == x.NationalId) || (x.DateOfBirth == dateOfBirth && x.Name.Equals(name)));
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await atlasDbContext.Accounts.ToListAsync();
        }
    }
}
