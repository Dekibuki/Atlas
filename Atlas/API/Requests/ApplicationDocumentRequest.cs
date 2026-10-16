using Atlas.Domain.Enum;

namespace Atlas.API.Requests
{
    /// <summary>
    /// Represent's a user's personal indicator - such as an ID or a passport, or a selfie.
    /// </summary>
    public class ApplicationDocumentRequest
    {
        public DocumentType Type { get; set; }

        public byte[] Image { get; set; }
    }
}
