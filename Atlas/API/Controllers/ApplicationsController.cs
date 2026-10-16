using Atlas.API.Requests;
using Atlas.API.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Atlas.API.Controllers
{
    [ApiController]
    [Route("applications")]
    [Produces("application/json")]
    public class ApplicationsController : ControllerBase
    {
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
            // TO DO: Implement application creation logic here
            return Ok();
        }
    }
}
