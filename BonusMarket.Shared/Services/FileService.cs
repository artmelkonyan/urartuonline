using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Repository;
using File = System.IO.File;

namespace BonusMarket.Shared.Services
{
    public class FileDownloadResult
    {
        public FileStream fileStream { get; set; }
        public string Type { get; set; }
    }
    public class FileService
    {
        private readonly FileServiceOptions _options;
        private readonly FileRepository _fileRepository;

        public FileService(FileServiceOptions options,
            FileRepository fileRepository)
        {
            _options = options;
            _fileRepository = fileRepository;
        }
        
        public string GeneratePath(DateTime dateTime)
        {
            string datePath = Path.Join(dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString());
            if (!Directory.Exists(Path.Combine(_options.BasePath, datePath)))
            {
                Directory.CreateDirectory(Path.Combine(_options.BasePath, datePath));
            }

            return Path.Join(dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(),
                Path.GetRandomFileName());
        }

        public async Task<int> Upload(IFormFile file)
        {
            var fileName = GeneratePath(DateTime.Now);
                var filePath = Path.Combine(_options.BasePath, fileName
                    );

                ;
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                var model = _fileRepository.Add(new BonusMarket.Shared.Models.Core.File()
                {
                    Path = fileName,
                    FileName = file.ContentType
                });
                return await Task.Run(() => model.ID);
        }

        public async Task<FileDownloadResult> Download(int id)
        {
            var model = _fileRepository.Get(id);
            if (model == null)
                return null;

            var filePath = Path.Combine(_options.BasePath, model.Path
            );

            if (!File.Exists(filePath))
                return null;

            var image = System.IO.File.OpenRead(filePath);
            return new FileDownloadResult()
            {
                fileStream = image,
                Type = model.FileName
            };
        }
    }
}