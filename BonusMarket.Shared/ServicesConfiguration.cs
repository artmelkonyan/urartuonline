using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using BonusMarket.Shared.DbProvider;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Shared.Services;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Repository;
using BonusMarket.Shared.Repository.LayoutItem;
using BonusMarket.Shared.Repository.Permission;
using BonusMarket.Shared.Repository;
using BonusMarket.Shared.Repository.Brand;
using BonusMarket.Shared.Repository.Picture;
using BonusMarket.Shared.Repository.Role;
using BonusMarket.Shared.Repository.User;

namespace BonusMarket.Shared
{
    public static class ServicesConfiguration
    {
        public static void AddShared(this IServiceCollection services, DbProviderOptions dbProviderOptions)
        {
            // add db context
            services.AddDbContext<Context>(
                options =>
                {
                    options.UseSqlServer(
                        dbProviderOptions.MssqlConnectionString, b => b.MigrationsAssembly("BonusMarket.Admin"));
                }, ServiceLifetime.Transient);
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            // add repositories
            services.AddScoped<UserRepository>();
            services.AddScoped<FileRepository>();
            services.AddScoped<PermissionRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<LayoutItemRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<PictureRepository>();
            services.AddScoped<BrandRepository>();
            
            // add helper services
            services.AddScoped<EncryptionService>();
            services.AddScoped<AuthorizationService>();
            services.AddScoped<AuthorizeUserHelper>();
            services.AddScoped<FileService>();

        }
    }
}
