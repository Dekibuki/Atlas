using Atlas.Domain.Models.Card;

namespace Atlas.Repository.CardsRepository
{
    /// <summary>
    /// Interface for managing user cards data.
    /// </summary>
    public interface ICardsRepository
    {
        public Task CreateCard(Card card);

        public Task<bool> CheckIfCardExistsByNationalIdOrNameAndDateOfBirth(string? nationalId, string name, DateTime dateOfBirth);
    }
}
