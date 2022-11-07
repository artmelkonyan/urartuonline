using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.Models.File;
using BonusMarket.Admin.ModelValidation.Auth;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Repository;
using BonusMarket.Shared.Repository.Picture;
using BonusMarket.Shared.Services;

namespace BonusMarket.Admin.Controllers
{
    [AuthorizeUser((int)Modules.Admin, new int[] { (int)AdminModule.Index })]
    public class FileController : Controller
    {
        private readonly FileRepository _fileRepository;
        private readonly FileService _fileService;
        private readonly PictureRepository _pictureRepository;

        public FileController(
            FileRepository fileRepository,
            PictureRepository pictureRepository,
            FileService fileService)
        {
            _fileRepository = fileRepository;
            _pictureRepository = pictureRepository;
            _fileService = fileService;
        }
        
        [HttpPost]
        [Route("/File/Upload")]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            if (files != null)
            {
                var result = await _fileService.Upload(files);
                return await Task.Run(() => Ok(result));
            }
            return await Task.Run( () => BadRequest());
        }
        
        [HttpPost]
        [Route("/File/UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile files)
        {
            if (files != null)
            {
                var result = await _fileService.Upload(files);
                var file = _fileRepository.Get(result);
                Picture picture = new Picture(file);
                picture.Status = true;
                picture.CreationDate = DateTime.Now;
                var pictureId = _pictureRepository.Add(picture);
                return await Task.Run(() => Ok(pictureId));
            }
            return await Task.Run( () => BadRequest());
        }

        [HttpGet]
        [Route("/File/Image/{id}")]
        public async Task<IActionResult> Images(int id)
        {
            var image = await _fileService.Download(id);
            if (image == null)
                return NotFound();
            var result = File(image.fileStream, image.Type);
            return await Task.Run(() => result);
        }

        [HttpPost]
        [Route("/File/Delete")]
        public IActionResult Delete([FromBody] IEnumerable<int> model)
        {
            foreach (var item in model)
            {
                _fileRepository.Delete(item);
            }
            _fileRepository.Save();
            return new JsonResult(new {});
        }
        
        [HttpPost]
//        [Route("/File/Deletes")]
        public IActionResult Deletes(DeleteItemsVm model)
        {
            foreach (var item in model.Items)
            {
                _fileRepository.Delete(item);
            }
            _fileRepository.Save();

            return Redirect(model.ReturnUrl);
        }
    }
}