using Atlas.Domain.Enum;
using Newtonsoft.Json;

namespace Atlas.API.Responses.CustomerApplication
{
    /// <summary>
    /// The response the API send back to the customer after they have made their application.
    /// </summary>
    [Serializable]
    public class CustomerApplicationResponse
    {
        [JsonProperty("applicationId")]
        public int ApplicationId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
