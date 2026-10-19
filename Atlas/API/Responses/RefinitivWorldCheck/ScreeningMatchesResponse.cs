using Atlas.Domain.Enum;
using Newtonsoft.Json;

namespace Atlas.API.Responses.RefinitivWorldCheck
{
    [Serializable]
    public class ScreeningMatchesResponse
    {
        [JsonProperty("listType")]
        public ScreeningListType ListType { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; } = string.Empty;
    }
}
