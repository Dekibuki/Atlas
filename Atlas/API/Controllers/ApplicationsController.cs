using Atlas.API.Responses.CustomerApplication;
using Atlas.Services.ApplicationsService;
using Atlas.Services.AccountsService;
using Atlas.Services.CardsService;
using Atlas.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Atlas.Domain.Models.CustomerApplication;
using Atlas.API.Requests.CustomerApplication;
using Atlas.API.Responses.Account;
using Atlas.API.Responses.Card;

namespace Atlas.API.Controllers
{
    [ApiController]
    [Route("atlas")]
    [Produces("application/json")]
    public class AtlasController : ControllerBase
    {
        private const int TimeoutMinutes = 3;

        private readonly IApplicationsService applicationsService;
        private readonly IAccountsService accountsService;
        private readonly ICardsService cardsService;
        private readonly ILogger<AtlasController> logger;

        public AtlasController(
            IApplicationsService applicationsService,
            IAccountsService accountsService,
            ICardsService cardsService,
            ILogger<AtlasController> logger)
        {
            this.applicationsService = applicationsService;
            this.accountsService = accountsService;
            this.cardsService = cardsService;
            this.logger = logger;
        }

        /// <summary>
        /// Receives a customer's application containing all their data, passing it to a service which then validates the data, 
        /// creates the application and awaits it's approval. The application's approval status is returned, along with the application's ID.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("applications")]
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
                    Status = ApplicationStatus.REJECTED.ToString()
                };

                return Created(failedResponse.ApplicationId.ToString(), failedResponse);
            }
        }

        /// <summary>
        /// Returns a list of all customer applications
        /// </summary>
        /// <returns>Customer applications</returns>
        [HttpGet("applications")]
        [SwaggerOperation(Summary = "An API endpoint for retrieving a list of all customer applications")]
        [SwaggerResponse(StatusCodes.Status200OK, "Customer applications retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error while retrieving customer applications.")]
        public async Task<ActionResult<CustomerApplicationListResponse>> GetApplications()
        {
            return Ok(await applicationsService.GetAllApplications());
        }

        /// <summary>
        /// Returns a list of all customer accounts
        /// </summary>
        /// <returns>Customer accounts</returns>
        [HttpGet("accounts")]
        [SwaggerOperation(Summary = "An API endpoint for retrieving a list of all accounts")]
        [SwaggerResponse(StatusCodes.Status200OK, "Accounts retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error while retrieving accounts.")]
        public async Task<ActionResult<AccountsListResponse>> GetAccounts()
        {
            return Ok(await accountsService.GetAllAccounts());
        }

        /// <summary>
        /// Returns a list of all customer cards
        /// </summary>
        /// <returns>Customer cards</returns>
        [HttpGet("cards")]
        [SwaggerOperation(Summary = "An API endpoint for retrieving a list of all cards")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cards retrieved successfully.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error while retrieving cards.")]
        public async Task<ActionResult<CardsListResponse>> GetCards()
        {
            return Ok(await cardsService.GetAllCards());
        }
    }
}
