using Atlas.Domain.Enum;

namespace Atlas.Domain.Models.ClientApplication
{
    public class ApplicationDocument
    {
        public int Id { get; set; }

        public int ClientApplicationId { get; set; }

        public DocumentType Type { get; set; }

        public byte[] Image { get; set; }
    }
}
