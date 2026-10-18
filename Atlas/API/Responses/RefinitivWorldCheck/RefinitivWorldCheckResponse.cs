using Atlas.Domain.Enum;

namespace Atlas.API.Responses.RefinitivWorldCheck
{
    public class RefinitivWorldCheckResponse
    {
        public string CaseId { get; set; } = string.Empty;

        public ScreeningResult Status { get; set; }

        public List<ScreeningMatchesResponse> Matches { get; set; } = new List<ScreeningMatchesResponse>();
    }
}
