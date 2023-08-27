using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BonusMarket.Shared;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Services;
using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Models.config;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BonusMarket
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRole", policy => policy.RequireRole("Administrator"));
            });
            services.AddSession();
            
            // data protection
            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });
            
            var dbProviderOptionsOld = new DbProviderOptionsOld();
            Configuration.Bind("DbProviderOptions", dbProviderOptionsOld);
            services.AddSingleton(dbProviderOptionsOld);
            services.AddTransient<ProductMssqlContext>();
            services.AddTransient<PictureMssqlContext>();
            services.AddTransient<ProductRepository>();
            services.AddTransient<ProductTranslationRepository>();
            services.AddTransient<PictureRepository>();
            services.AddTransient<ProductToPictureRepository>();
            services.AddTransient<AdminLayer>();
            
            
            // file service options
            var fileServiceOptions = new FileServiceOptions();
            Configuration.Bind("FileServiceOptions", fileServiceOptions);
            if (HostEnvironmentEnvExtensions.IsDevelopment(_env))
            {
                var baseDirectory = AppContext.BaseDirectory;
                var currentPath = Path.Join(baseDirectory, "../../../../../data");
                bool exists = Directory.Exists(currentPath);
                if (exists)
                {
                    fileServiceOptions.BasePath = currentPath;
                }  
            }
            services.AddSingleton(fileServiceOptions);
            
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
            services.Configure<RequestLocalizationOptions>(
            opts =>
            {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("hy"),
            };

            opts.DefaultRequestCulture = new RequestCulture("hy");
            // Formatting numbers, dates, etc.
            opts.SupportedCultures = supportedCultures;
            // UI strings that we have localized.
            opts.SupportedUICultures = supportedCultures;
            opts.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
            };
        });

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation().AddSessionStateTempDataProvider()
        .AddViewLocalization(
            LanguageViewLocationExpanderFormat.Suffix,
            opts => { opts.ResourcesPath = "Resources"; })
        .AddDataAnnotationsLocalization();
            
            
            // shared
            var dbProviderOptions = new DbProviderOptions();
            Configuration.Bind("DbProviderOptions", dbProviderOptions);
            services.AddSingleton(dbProviderOptions);
            services.AddShared(dbProviderOptions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error?isTechnical=true");
            }
           // app.UseStatusCodePagesWithRedirects("/Home/Error");
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(name: "AddProductToCart-Catalog",
                pattern: $"addproducttocart/catalog/{{productId:min(0)}}/{{shoppingCartTypeId:min(0)}}/{{quantity:min(0)}}",
                defaults: new { controller = "ShoppingCartItem", action = "AddProductToCart_Catalog" });

                endpoints.MapControllerRoute(name: "Login",
                    pattern: "/Account/Login",
                    defaults: new { controller = "Account", action = "Login" }
                    );
            });
        }
    }
}
