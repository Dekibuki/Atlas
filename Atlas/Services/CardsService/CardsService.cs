using Atlas.API.DTO;
using Atlas.API.Responses.Card;
using Atlas.Domain.Models.Card;
using Atlas.Repository.CardsRepository;

namespace Atlas.Services.CardsService
{
    /// <summary>
    /// Service for functions regarding manipulating user accounts.
    /// </summary>
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository cardsRepository;

        public CardsService(ICardsRepository cardsRepository)
        {
            this.cardsRepository = cardsRepository;
        }

        public async Task CreateCard(CardCreationDto cardDto)
        {
            // One user should have one card - should be discussed
            if (!await cardsRepository.CheckIfCardExistsByNationalIdOrNameAndDateOfBirth(
                cardDto.NationalId,
                cardDto.Name,
                cardDto.DateOfBirth))
            {
                Card card = new Card
                {
                    CreatedOn = DateTime.Now,
                    DateOfBirth = cardDto.DateOfBirth,
                    Name = cardDto.Name,
                    NationalId = cardDto.NationalId
                };

                await cardsRepository.CreateCard(card);
            }
        }

        public async Task<CardsListResponse> GetAllCards()
        {
            List<Card> cards = await cardsRepository.GetAllCards() ?? new List<Card>();
            return new CardsListResponse { Cards = cards, TotalCount = cards.Count };
        }
    }
}
