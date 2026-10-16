using Atlas.API.Requests;

namespace Atlas.Services.ApplicationsService
{
    /// <summary>
    /// Interface for service regarding application creation, validation and approval.
    /// </summary>
    public interface IApplicationsService
    {
        public Task<int> CreateApplication(CustomerApplicationRequest request);
    }
}
