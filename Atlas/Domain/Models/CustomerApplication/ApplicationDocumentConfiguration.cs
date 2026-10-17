using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Domain.Models.CustomerApplication
{
    public class ApplicationDocumentConfiguration : IEntityTypeConfiguration<ApplicationDocument>
    {
        public void Configure(EntityTypeBuilder<ApplicationDocument> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerApplicationId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Image).IsRequired();
        }
    }
}
