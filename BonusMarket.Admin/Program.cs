using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.Utils;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Repository.Picture;
using BonusMarket.Shared.Services;

namespace BonusMarket.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            // PicturesNewPath(host); // one time only

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<Context>();
                    var encryptionService = services.GetRequiredService<EncryptionService>();
                    // context.Database.EnsureCreated();
                    ContextInitializer.Initialize(context, encryptionService);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        
        /// <summary>
        /// PicturesNewPath
        /// </summary>
        /// <param name="host"></param>
        private static void PicturesNewPath(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var fileServiceOptions = services.GetRequiredService<FileServiceOptions>();
                    var fileService = services.GetRequiredService<FileService>();
                    var context = services.GetRequiredService<Context>();
                    var pictures = context.Pictures.Where(r => String.IsNullOrEmpty(r.NewPath)).ToList();
                    int i = 1;
                    foreach (var picture in pictures)
                    {
                        string newPath = Path.Combine(fileService.GeneratePath(picture.CreationDate ?? DateTime.Now));
                        picture.NewPath = newPath;
                        //copy file
                        string oldFullPath = Path.Combine(fileServiceOptions.BasePath, "www", picture.FullPath);
                        string newFullPath = Path.Combine(fileServiceOptions.BasePath, newPath);
                        if (File.Exists(oldFullPath))
                        {
                            File.Copy(oldFullPath, newFullPath);
                            context.Pictures.Update(picture);
                        }
                        else
                        {
                            logger.LogWarning($"Picture not found {picture.FullPath}");
                        }

                        Console.WriteLine($"Picture done {i++}");
                    }
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error Copying pictures new paths");
                }
            }
            
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var fileServiceOptions = services.GetRequiredService<FileServiceOptions>();
                    var fileService = services.GetRequiredService<FileService>();
                    var context = services.GetRequiredService<Context>();
                    var pictures = context.Pictures.Where(r => r.FileId == null).ToList();
                    int i = 1;
                    foreach (var picture in pictures)
                    {
                        // string newPath = Path.Combine(fileService.GeneratePath(picture.CreationDate ?? DateTime.Now));
                        // picture.NewPath = newPath;
                        // //copy file
                        // string oldFullPath = Path.Combine(fileServiceOptions.BasePath, "www", picture.FullPath);
                        // string newFullPath = Path.Combine(fileServiceOptions.BasePath, newPath);
                        // if (File.Exists(oldFullPath))
                        // {
                        //     File.Copy(oldFullPath, newFullPath);
                        //     context.Pictures.Update(picture);
                        // }
                        // else
                        // {
                        //     logger.LogWarning($"Picture not found {picture.FullPath}");
                        // }
                        //
                        // Console.WriteLine($"Picture done {i++}");
                    }
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error Copying pictures new paths");
                }
            }
            
            
            
        }
        
    }
}
