using Atlas.API.Requests.CustomerApplication;
using Atlas.API.Responses.CustomerApplication;

namespace Atlas.Services.ApplicationsService
{
    /// <summary>
    /// Interface for service regarding application creation, validation and approval.
    /// </summary>
    public interface IApplicationsService
    {
        public Task<CustomerApplicationResponse> CreateApplication(CustomerApplicationRequest request);

        public Task<CustomerApplicationListResponse> GetAllApplicationsForCustomer(CustomerApplicationsListRequest request);
    }
}
