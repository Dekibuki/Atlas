using Atlas.Domain.Models.Account;
using Atlas.Domain.Models.Card;
using Atlas.Domain.Models.CustomerApplication;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Repository.AtlastDb
{
    public class AtlasDbContext : DbContext
    {
        public AtlasDbContext(DbContextOptions<AtlasDbContext> options) :
            base(options)
        {
        }

        public DbSet<ApplicationDocument> ApplicationDocuments { get; set; }

        public DbSet<CustomerApplication> CustomerApplications { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
