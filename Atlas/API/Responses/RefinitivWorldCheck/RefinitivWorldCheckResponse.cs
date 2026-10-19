using Atlas.Domain.Enum;
using Newtonsoft.Json;

namespace Atlas.API.Responses.RefinitivWorldCheck
{
    [Serializable]
    public class RefinitivWorldCheckResponse
    {
        [JsonProperty("caseId")]
        public string CaseId { get; set; } = string.Empty;

        [JsonProperty("status")]
        public ScreeningResult Status { get; set; }

        [JsonProperty("matches")]
        public List<ScreeningMatchesResponse> Matches { get; set; } = new List<ScreeningMatchesResponse>();
    }
}
