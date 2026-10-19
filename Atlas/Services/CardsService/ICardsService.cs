using Atlas.API.DTO;
using Atlas.API.Responses.Card;

namespace Atlas.Services.CardsService
{
    /// <summary>
    /// Interface for functions regarding manipulating user cards.
    /// </summary>
    public interface ICardsService
    {
        public Task CreateCard(CardCreationDto cardDto);

        public Task<CardsListResponse> GetAllCards();
    }
}
