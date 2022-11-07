using System.Collections.Generic;
using System.Threading.Tasks;
using BonusMarket.Admin.Models.ViewComponents.Picture;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Repository;
using BonusMarket.Shared.Repository.Picture;
using BonusMarket.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Admin.ViewComponents
{
    [ViewComponent(Name = "Picture")]
    public class PictureViewComponent : ViewComponent
    {
        private readonly FileRepository _fileRepository;
        private readonly FileService _fileService;
        private readonly PictureRepository _pictureRepository;

        public PictureViewComponent(FileRepository fileRepository, FileService fileService, PictureRepository pictureRepository)
        {
            _fileRepository = fileRepository;
            _fileService = fileService;
            _pictureRepository = pictureRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(PictureViewComponentVm model)
        {
            return View(model);
        }
    }
}