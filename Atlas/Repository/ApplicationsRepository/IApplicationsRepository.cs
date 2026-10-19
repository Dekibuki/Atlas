using Atlas.Domain.Models.CustomerApplication;

namespace Atlas.Repository.ApplicationsRepository
{
    /// <summary>
    /// Interface for handling customer application database operations.
    /// </summary>
    public interface IApplicationsRepository
    {
        public Task<int> CreateCustomerApplication(CustomerApplication customerApplication);

        public Task CreateCustomerDocuments(List<ApplicationDocument> applicationDocuments);

        public Task<List<CustomerApplication>> GetAllCustomerApplications();
    }
}
