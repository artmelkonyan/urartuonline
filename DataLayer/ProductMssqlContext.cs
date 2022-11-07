using Microsoft.EntityFrameworkCore;
using Models;
using Models.config;

namespace DataLayer
{
    public class ProductMssqlContext : DbContext
    {
        private readonly DbProviderOptionsOld _options;
        public ProductMssqlContext(DbProviderOptionsOld options)
        {
            _options = options;
        }
        public DbSet<ProductDbModel> Products { get; set; }
        public DbSet<ProductTranslationDbModel> ProductTranslation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_options.MssqlConnectionString);
    }
}