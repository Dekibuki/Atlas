using Atlas.API.Requests;
using Atlas.API.Responses;
using Atlas.Domain.Enum;

namespace Atlas.Services.ApplicationsService
{
    /// <summary>
    /// Service implementation regarding application creation, validation and approval.
    /// </summary>
    public class ApplicationService : IApplicationsService
    {
        public Task<CustomerApplicationResponse> CreateApplication(CustomerApplicationRequest request)
        {
            // TO DO: Implement application creation logic here
        }
    }
}
