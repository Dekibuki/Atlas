using Atlas.Domain.Enum;

namespace Atlas.API.Requests
{
    /// <summary>
    /// Request body for client's application, containing their personal information and personal documents.
    /// </summary>
    public class CustomerApplicationRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Country Country { get; set; }

        public string NationalId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public List<ApplicationDocumentRequest> Documents { get; set; }

        public bool TermsAccepted { get; set; }
    }
}
