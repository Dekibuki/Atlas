using Newtonsoft.Json;

namespace Atlas.API.Responses.CustomerApplication
{
    [Serializable]
    public class CustomerApplicationListResponse
    {
        [JsonProperty("applications")]
        public List<Domain.Models.CustomerApplication.CustomerApplication> Applications { get; set; } = new List<Domain.Models.CustomerApplication.CustomerApplication>();

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
