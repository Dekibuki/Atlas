namespace Atlas.Domain.Models.Account
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? NationalId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

    }
}
