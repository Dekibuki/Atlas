using Atlas.Domain.Enum;
using Newtonsoft.Json;

namespace Atlas.API.Responses.IDNow
{
    [Serializable]
    public class IDNowDocumentResponse
    {
        [JsonProperty("identificationId")]
        public string IdentificationId { get; set; } = string.Empty;

        [JsonProperty("documentResult")]
        public DocumentResult DocumentResult { get; set; }

        [JsonProperty("faceMatch")]
        public bool FaceMatch { get; set; }

        [JsonProperty("confidance")]
        public float Confidance { get; set; }
    }
}
