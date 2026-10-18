using Atlas.API.Requests;
using Atlas.API.Responses.CustomerApplication;
using Atlas.Services.ApplicationsService;
using Atlas.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Atlas.API.Controllers
{
    [ApiController]
    [Route("applications")]
    [Produces("application/json")]
    public class ApplicationsController : ControllerBase
    {
        private const int TimeoutMinutes = 3;

        private readonly IApplicationsService applicationsService;
        private readonly ILogger<ApplicationsController> logger;

        public ApplicationsController(IApplicationsService applicationsService, ILogger<ApplicationsController> logger)
        {
            this.applicationsService = applicationsService;
            this.logger = logger;
        }

        /// <summary>
        /// Receives a customer's application containing all their data, passing it to a service which then validates the data, 
        /// creates the application and awaits it's approval. The application's approval status is returned, along with the application's ID.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "An API endpoint for creating customer applications.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Customer application created successfully.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error while creating customer application.")]
        public async Task<ActionResult<CustomerApplicationResponse>> CreateApplication([FromBody] CustomerApplicationRequest request)
        {
            try
            {
                CustomerApplicationResponse response = await applicationsService.CreateApplication(request).WaitAsync(TimeSpan.FromMinutes(TimeoutMinutes));

                return Created(response.ApplicationId.ToString(), response);
            }
            catch (TimeoutException e)
            {
                logger.LogError("Process has exceeded 3 minutes.");
                CustomerApplicationResponse failedResponse = new CustomerApplicationResponse
                {
                    ApplicationId = 0,
                    Status = ApplicationStatus.REJECTED
                };

                return Created(failedResponse.ApplicationId.ToString(), failedResponse);
            }
        }
    }
}
