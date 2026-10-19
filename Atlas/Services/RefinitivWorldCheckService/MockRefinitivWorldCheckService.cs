using Atlas.API.DTO;
using Atlas.API.Responses.RefinitivWorldCheck;
using Atlas.Domain.Enum;

namespace Atlas.Services.RefinitivWorldCheckService
{
    public class MockRefinitivWorldCheckService : IRefinitivWorldCheckService
    {
        public Task<RefinitivWorldCheckResponse?> ProcessScreening(RefinitivWorldCheckScreeningDto screeningData)
        {
            return Task.FromResult(new RefinitivWorldCheckResponse
            {
                CaseId = Guid.NewGuid().ToString(),
                Matches = new List<ScreeningMatchesResponse> {
                    new ScreeningMatchesResponse { ListType = Domain.Enum.ScreeningListType.PEP, Score = 0.8f, Subject = "abc" }
                },
                Status = ScreeningResult.CLEAR
            });
        }
    }
}
