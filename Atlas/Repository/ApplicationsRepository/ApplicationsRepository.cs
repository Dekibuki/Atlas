using Atlas.Domain.Models.CustomerApplication;
using Atlas.Repository.AtlastDb;
using System.Data.Entity;

namespace Atlas.Repository.ApplicationsRepository
{
    /// <summary>
    /// Repository for handling customer application database operations.
    /// </summary>
    public class ApplicationsRepository : IApplicationsRepository
    {
        private readonly AtlasDbContext atlasDbContext;

        public ApplicationsRepository(AtlasDbContext atlasDbContext)
        {
            this.atlasDbContext = atlasDbContext;
        }

        public async Task<int> CreateCustomerApplication(CustomerApplication customerApplication)
        {
            await atlasDbContext.CustomerApplications.AddAsync(customerApplication);
            await atlasDbContext.SaveChangesAsync();

            return customerApplication.Id;
        }

        public async Task CreateCustomerDocuments(List<ApplicationDocument> applicationDocuments)
        {
            await atlasDbContext.ApplicationDocuments.AddRangeAsync(applicationDocuments);
            await atlasDbContext.SaveChangesAsync();
        }

        public async Task<List<CustomerApplication>> GetCustomerApplicationsByNationalIdOrNameAndDateOfBirth(string? nationalId, string firstName, string lastName, DateTime dateOfBirth)
        {
            return await atlasDbContext.CustomerApplications
                .Include(x => x.Documents)
                .Where(x => (nationalId != null && x.NationalId == nationalId)
                         || (x.DateOfBirth == dateOfBirth && firstName == x.FirstName && lastName == x.LastName))
                .ToListAsync();
        }

    }
}
