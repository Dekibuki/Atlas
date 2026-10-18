using Atlas.Domain.Enum;

namespace Atlas.Domain.Models.CustomerApplication
{
    /// <summary>
    /// Domain class representing a client application that will be saved in memory/database.
    /// </summary>
    public class CustomerApplication
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Country Country { get; set; }

        public string? NationalId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public bool TermsAccepted { get; set; }

        public ApplicationStatus Status { get; set; }

        public virtual List<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
    }
}
