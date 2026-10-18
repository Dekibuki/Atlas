using Atlas.Domain.Enum;

namespace Atlas.API.Requests.CustomerApplication
{
    /// <summary>
    /// Request body for client's application, containing their personal information and personal documents.
    /// </summary>
    public class CustomerApplicationRequest
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Country Country { get; set; }

        public string? NationalId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public List<ApplicationDocumentRequest> Documents { get; set; } = new List<ApplicationDocumentRequest>();

        public bool TermsAccepted { get; set; }
    }
}
