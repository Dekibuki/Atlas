namespace Atlas.API.Responses.CustomerApplication
{
    public class CustomerApplicationListResponse
    {
        public List<Domain.Models.CustomerApplication.CustomerApplication> Applications { get; set; } = new List<Domain.Models.CustomerApplication.CustomerApplication>();
    }
}
