using Atlas.Domain.Enum;

namespace Atlas.API.Responses
{
    public class IDNowDocumentResponse
    {
        public string IdentificationId { get; set; } = string.Empty;

        public DocumentResult DocumentResult { get; set; }

        public bool FaceMatch { get; set; }

        public float Confidance { get; set; }
    }
}
