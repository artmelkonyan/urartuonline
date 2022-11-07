using System.Linq;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Models.Core.User;
using BonusMarket.Shared.Models.Core.Role;
using BonusMarket.Shared.Models.Core.Permission;

namespace BonusMarket.Shared.DbProvider
{
    public class Context : DbContext
    {
        // user and role part
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslation { get; set; }
        public DbSet<ProductPicture> Product_To_Picture { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<LayoutItem> LayoutItems { get; set; }
        public DbSet<LayoutItemTranslation> LayoutItemTranslations { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<BrandTranslation> BrandTranslate { get; set; }
        
        public DbSet<File> Files { get; set; }
      
        
        public Context(DbContextOptions<Context> options)
        : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelbuilder);
        }
    }
}