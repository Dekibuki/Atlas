using Atlas.Domain.Models.Card;
using Atlas.Repository.AtlastDb;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Repository.CardsRepository
{
    /// <summary>
    /// Repository for managing user cards data.
    /// </summary>
    public class CardsRepository : ICardsRepository
    {
        private readonly AtlasDbContext atlasDbContext;

        public CardsRepository(AtlasDbContext atlasDbContext)
        {
            this.atlasDbContext = atlasDbContext;
        }

        public async Task CreateCard(Card card)
        {
            await atlasDbContext.Cards.AddAsync(card);
            await atlasDbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfCardExistsByNationalIdOrNameAndDateOfBirth(string? nationalId, string name, DateTime dateOfBirth)
        {
            return await atlasDbContext.Cards
                .AnyAsync(x => (nationalId != null && nationalId == x.NationalId) || (x.DateOfBirth == dateOfBirth && x.Name.Equals(name)));
        }

        public async Task<List<Card>> GetAllCards()
        {
            return await atlasDbContext.Cards.ToListAsync();
        }
    }
}
