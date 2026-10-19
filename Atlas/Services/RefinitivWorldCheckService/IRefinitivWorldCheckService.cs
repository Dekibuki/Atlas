using Atlas.API.DTO;
using Atlas.API.Responses.RefinitivWorldCheck;

namespace Atlas.Services.RefinitivWorldCheckService
{
    /// <summary>
    /// Interface regarding Refinitiv World-Check functionality - processing PEP and screening.
    /// </summary>
    public interface IRefinitivWorldCheckService
    {
        public Task<RefinitivWorldCheckResponse?> ProcessScreening(RefinitivWorldCheckScreeningDto screeningData);
    }
}
