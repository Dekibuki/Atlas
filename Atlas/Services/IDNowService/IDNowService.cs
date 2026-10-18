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
        private readonly HttpClient httpClient;

        public IDNowService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IDNowDocumentResponse?> ProcessIdentifications(IDNowDocumentDTO customerDocumentData)
        {
            // Authorization left out - unknown what kind of authorization this third-party API uses
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(ProcessingURL, customerDocumentData);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IDNowDocumentResponse?>();
        }
    }
}
