namespace Atlas.API.Requests.CustomerApplication
{
    public class CustomerApplicationsListRequest
    {
        public string? NationalId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
