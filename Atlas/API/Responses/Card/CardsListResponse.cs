using Newtonsoft.Json;

namespace Atlas.API.Responses.Card
{
    [Serializable]
    public class CardsListResponse
    {
        [JsonProperty("cards")]
        public List<Domain.Models.Card.Card> Cards { get; set; } = new List<Domain.Models.Card.Card>();

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
