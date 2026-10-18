using Atlas.API.DTO;
using Atlas.API.Responses.IDNow;
using Atlas.API.Responses.RefinitivWorldCheck;

namespace Atlas.Services.RefinitivWorldCheckService
{
    /// <summary>
    /// Service regarding Refinitiv World-Check functionality - processing PEP and screening.
    /// </summary>
    public class RefinitivWorldCheckService : IRefinitivWorldCheckService
    {
        private const string ProcessingURL = "https://api.refinitiv.example/worldcheck/v2/screen";
        private readonly HttpClient httpClient;

        public RefinitivWorldCheckService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RefinitivWorldCheckResponse?> ProcessScreening(RefinitivWorldCheckScreeningDTO screeningData)
        {
            // Authorization left out - unknown what kind of authorization this third-party API uses
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(ProcessingURL, screeningData);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RefinitivWorldCheckResponse?>();
        }
    }
}
