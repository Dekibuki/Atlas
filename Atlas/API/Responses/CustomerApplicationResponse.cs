using Atlas.Domain.Enum;

namespace Atlas.API.Responses
{
    /// <summary>
    /// The response the API send back to the customer after they have made their application.
    /// </summary>
    public class CustomerApplicationResponse
    {
        public int ApplicationId { get; set; }

        public ApplicationStatus Status { get; set; }
    }
}
