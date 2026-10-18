namespace Atlas.Domain.Models.Card
{
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? NationalId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
