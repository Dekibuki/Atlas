using Atlas.API.DTO;
using Atlas.API.Responses.IDNow;

namespace Atlas.Services.IDNowService
{
    /// <summary>
    /// Service regarding IDNow functionality - processing document authenticity and face match.
    /// </summary>
    public class IDNowService : IIDNowService
    {
        private const string ProcessingURL = "https://api.idnow.example/v1/identifications";

        private const int MaximumAttempts = 3;

        private readonly HttpClient httpClient;

        public IDNowService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IDNowDocumentResponse?> ProcessIdentifications(IDNowDocumentDTO customerDocumentData)
        {
            for (int attempt = 1; attempt <= MaximumAttempts; attempt++)
            {
                try
                {
                    // Authorization left out - unknown what kind of authorization vault to get the required keys
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                        ProcessingURL, customerDocumentData);

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadFromJsonAsync<IDNowDocumentResponse?>();
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
