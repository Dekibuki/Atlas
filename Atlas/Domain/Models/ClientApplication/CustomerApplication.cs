using Atlas.Domain.Enum;

namespace Atlas.Domain.Models.ClientApplication
{
    /// <summary>
    /// Domain class representing a client application that will be saved in memory/database.
    /// </summary>
    public class CustomerApplication
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Country Country { get; set; }

        public string NationalId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool TermsAccepted { get; set; }

        public ApplicationStatus Status { get; set; }
    }
}
