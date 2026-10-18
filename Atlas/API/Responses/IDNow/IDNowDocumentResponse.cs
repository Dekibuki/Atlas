using Atlas.Domain.Enum;

namespace Atlas.API.Responses.IDNow
{
    public class IDNowDocumentResponse
    {
        public string IdentificationId { get; set; } = string.Empty;

        public DocumentResult DocumentResult { get; set; }

        public bool FaceMatch { get; set; }

        public float Confidance { get; set; }
    }
}
