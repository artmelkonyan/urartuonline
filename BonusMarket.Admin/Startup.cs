using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using BonusMarket.Shared;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Resources;
using BonusMarket.Shared.Services;
using BonusMarket.SharedAdmin.Resources;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace BonusMarket.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }

        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


//             services.Configure<RequestLocalizationOptions>(
//                 options =>
//                 {
//                     var en = new CultureInfo("en-US");
//                     en.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm:ss";
//                     en.DateTimeFormat.LongTimePattern = "HH:mm:ss";
//                     en.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
//                     var hy = new CultureInfo("hy-HY");
//                     hy.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm:ss";
//                     hy.DateTimeFormat.LongTimePattern = "HH:mm:ss";
//                     hy.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
//                     var ru = new CultureInfo("ru-RU");
//                     ru.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm:ss";
//                     ru.DateTimeFormat.LongTimePattern = "HH:mm:ss";
//                     ru.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
//                     var supportedCultures = new List<CultureInfo>
//                     {
//                         en,
//                         hy,
//                         ru
//                     };
//
//                     options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
//                     options.SupportedCultures = supportedCultures;
//                     options.SupportedUICultures = supportedCultures;
//
// //                    options.RequestCultureProviders.Insert(0, new RouteCultureProvider(options.DefaultRequestCulture));
// //                    options.RequestCultureProviders.Clear();
//                 });
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

            services.AddSingleton<LocalizationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // data protection
            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });
            
            // add mvc
            services.AddControllersWithViews()
            .AddRazorRuntimeCompilation()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            

            // data protection
            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });
            
            // url helper
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            
            // routing
            services.AddRouting(options => options.LowercaseUrls = true);
            
            // cache
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            
            // antiforgery token options
            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

            services.AddSession(options =>
            {
                // // Set a short timeout for easy testing.
                // options.IdleTimeout = TimeSpan.FromMinutes(60);
                // // You might want to only set the application cookies over a secure connection:
                // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                // options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            
            // authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => {
                    o.LoginPath = "/auth/login";
                    o.LogoutPath = "/auth/logout";
                    o.ExpireTimeSpan = TimeSpan.FromDays(7);
                });
            
            // database and repositories
            var dbProviderOptions = new DbProviderOptions();
            Configuration.Bind("DbProviderOptions", dbProviderOptions);
            services.AddSingleton(dbProviderOptions);
            
            
            // file service options
            var fileServiceOptions = new FileServiceOptions();
            Configuration.Bind("FileServiceOptions", fileServiceOptions);
            if (_env.IsDevelopment())
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
            
            services.AddShared(dbProviderOptions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IWebHostEnvironment env,
            IAntiforgery antiforgery)
        {

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            // for nginx proxy pass
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // authentication
            app.UseAuthentication();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/page{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // cache statics
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            
            app.UseCookiePolicy();
        }
    }
}
