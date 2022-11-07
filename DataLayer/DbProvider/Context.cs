using Microsoft.EntityFrameworkCore;
using Models;
using Models.config;

namespace DataLayer.DbProvider
{
    public class Context : DbContext
    {
        private readonly DbProviderOptionsOld _options;
        
        public Context(DbProviderOptionsOld options)
        {
            _options = options;
        }
        public DbSet<PictureDbModel> Pictures { get; set; }
        public DbSet<ProductToPictureDbModel> Product_To_Picture { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_options.MssqlConnectionString);
    }
}