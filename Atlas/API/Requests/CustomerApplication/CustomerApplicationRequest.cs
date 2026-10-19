using Atlas.Domain.Enum;
using Newtonsoft.Json;

namespace Atlas.API.Requests.CustomerApplication
{
    /// <summary>
    /// Request body for client's application, containing their personal information and personal documents.
    /// </summary>
    [Serializable]
    public class CustomerApplicationRequest
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("nationalId")]
        public string? NationalId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonProperty("documents")]
        public List<ApplicationDocumentRequest> Documents { get; set; } = new List<ApplicationDocumentRequest>();

        [JsonProperty("termsAccepted")]
        public bool TermsAccepted { get; set; }
    }
}
