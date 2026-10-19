using Atlas.Domain.Enum;
using Newtonsoft.Json;

namespace Atlas.API.Requests.CustomerApplication
{
    /// <summary>
    /// Represent's a user's personal indicator - such as an ID or a passport, or a selfie.
    /// </summary>
    [Serializable]
    public class ApplicationDocumentRequest
    {
        [JsonProperty("type")]
        public DocumentType Type { get; set; }

        [JsonProperty("image")]
        public byte[] Image { get; set; } = new byte[1];
    }
}
