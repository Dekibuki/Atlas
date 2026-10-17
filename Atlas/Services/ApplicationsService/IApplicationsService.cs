using Atlas.API.Requests;
using Atlas.API.Responses;

namespace Atlas.Services.ApplicationsService
{
    /// <summary>
    /// Interface for service regarding application creation, validation and approval.
    /// </summary>
    public interface IApplicationsService
    {
        public Task<CustomerApplicationResponse> CreateApplication(CustomerApplicationRequest request);
    }
}
