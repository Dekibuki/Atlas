using Atlas.Domain.Models.CustomerApplication;

namespace Atlas.Services.CardsService
{
    /// <summary>
    /// Interface for functions regarding manipulating user cards.
    /// </summary>
    public interface ICardsService
    {
        public Task CreateCardFromCustomerApplication(CustomerApplication customerApplication);
    }
}
