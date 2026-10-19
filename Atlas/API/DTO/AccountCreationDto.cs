namespace Atlas.API.DTO
{
    public class AccountCreationDto
    {
        public string Name { get; set; } = string.Empty;

        public string? NationalId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
