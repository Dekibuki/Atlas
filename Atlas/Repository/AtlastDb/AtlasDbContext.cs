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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerApplicationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
