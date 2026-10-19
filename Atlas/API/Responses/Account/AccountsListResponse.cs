using Newtonsoft.Json;

namespace Atlas.API.Responses.Account
{
    [Serializable]
    public class AccountsListResponse
    {
        [JsonProperty("accounts")]
        public List<Domain.Models.Account.Account> Accounts { get; set; } = new List<Domain.Models.Account.Account>();

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
