namespace Atlas.API.DTO
{
    public class CardCreationDto
    {
        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? NationalId { get; set; }
    }
}
