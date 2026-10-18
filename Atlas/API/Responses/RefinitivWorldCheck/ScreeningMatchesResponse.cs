using Atlas.Domain.Enum;

namespace Atlas.API.Responses.RefinitivWorldCheck
{
    public class ScreeningMatchesResponse
    {
        public ScreeningListType ListType { get; set; }

        public float Score { get; set; }

        public string Subject { get; set; } = string.Empty;
    }
}
