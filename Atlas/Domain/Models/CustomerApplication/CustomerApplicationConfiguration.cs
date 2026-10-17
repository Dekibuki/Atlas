using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Domain.Models.CustomerApplication
{
    public class CustomerApplicationConfiguration : IEntityTypeConfiguration<CustomerApplication>
    {
        public void Configure(EntityTypeBuilder<CustomerApplication> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Country).IsRequired();
            builder.Property(x => x.NationalId).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.TermsAccepted).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
