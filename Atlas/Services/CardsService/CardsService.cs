using Atlas.Domain.Models.Card;
using Atlas.Domain.Models.CustomerApplication;
using Atlas.Repository.AtlastDb;
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

        public async Task CreateCardFromCustomerApplication(CustomerApplication customerApplication)
        {
            string fullCustomerName = String.Join(" ", customerApplication.FirstName, customerApplication.LastName);

            // One user should have one card - should be discussed
            if (!await cardsRepository.CheckIfCardExistsByNationalIdOrNameAndDateOfBirth(
                customerApplication.NationalId,
                fullCustomerName,
                customerApplication.DateOfBirth))
            {
                Card card = new Card
                {
                    DateOfBirth = customerApplication.DateOfBirth,
                    Name = fullCustomerName,
                    NationalId = customerApplication.NationalId
                };

                await cardsRepository.CreateCard(card);
            }
        }
    }
}
