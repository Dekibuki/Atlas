using Atlas.Domain.Enum;

namespace Atlas.Domain.Models.CustomerApplication
{
    /// <summary>
    /// Domain class representing a document attached to a customer's application.
    /// </summary>
    public class ApplicationDocument
    {
        public int Id { get; set; }

        public int CustomerApplicationId { get; set; }

        public DocumentType Type { get; set; }

        public byte[] Image { get; set; } = new byte[1];
    }
}
