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

        private const int MaximumAttempts = 3;
        private readonly HttpClient httpClient;

        public RefinitivWorldCheckService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RefinitivWorldCheckResponse?> ProcessScreening(RefinitivWorldCheckScreeningDTO screeningData)
        {
            for (int attempt = 1; attempt <= MaximumAttempts; attempt++)
            {
                try
                {
                    // Authorization left out - unknown what kind of authorization vault to get the required keys
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(ProcessingURL, screeningData);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadFromJsonAsync<RefinitivWorldCheckResponse?>();
                }
                catch (HttpRequestException ex)
                    when ((int?)ex.StatusCode >= 500 && attempt < MaximumAttempts)
                {
                    // Retry on internal server errors only. Should place delay here.
                }
            }
            return null;
        }
    }
}
