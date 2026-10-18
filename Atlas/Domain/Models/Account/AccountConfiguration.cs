using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Domain.Models.Account
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.NationalId).IsRequired(false);
            builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
        }
    }
}
