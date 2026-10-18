using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Domain.Models.Card
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd();
            builder.Property(x => x.NationalId).IsRequired(false);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
        }
    }
}
