using Atlas.Domain.Enum;

namespace Atlas.API.DTO
{
    public class IDNowDocumentDTO
    {
        public DocumentType DocumentType { get; set; }

        public byte[] DocumentImage { get; set; } = new byte[1];

        public byte[] SelfieImage { get; set; } = new byte[1];
    }
}
