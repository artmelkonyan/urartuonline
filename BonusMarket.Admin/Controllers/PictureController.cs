using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.Models.Picture;
using BonusMarket.Admin.ModelValidation.Auth;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Repository.Picture;

namespace BonusMarket.Admin.Controllers
{
    [AuthorizeUser((int)Modules.Auth, new int[] { (int)AuthModule.Login })]
    public class PictureController : Controller
    {
        private readonly PictureRepository _pictureRepository;

        public PictureController(PictureRepository categoryRepository)
        {
            _pictureRepository = categoryRepository;
        }
        
        [Route("/Picture")]
        [Route("/Picture/Index")]
        [HttpGet]
        public IActionResult Index(PicturePagingVm paging)
        {
            var model = new PictureVm();
            model.List = _pictureRepository.GetList(new PictureFilter() { CurrentPage =  paging.Page, PageSize = paging.PageSize});
            return View(model);
        }

        [HttpGet]
        [Route("/Picture/Add")]
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        [Route("/Picture/Add/")]
        public IActionResult Add(PictureAddPostVm model)
        {
            _pictureRepository.Add(model.GetDbModel());
            _pictureRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Picture/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = _pictureRepository.Get(id);
            PictureEditVm model = new PictureEditVm(category);

            return View(model);
        }

        
        [HttpPost]
        [Route("/Picture/Edit")]
        public IActionResult Edit(PictureEditVm model)
        {
            _pictureRepository.Update(model.GetDbModel());
            _pictureRepository.Save();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("/Picture/Delete")]
        public IActionResult Delete([FromBody] IEnumerable<int> model)
        {
            foreach (var item in model)
            {
                _pictureRepository.Delete(item);
            }
            _pictureRepository.Save();
            return new JsonResult(new {});
        }
        
        [HttpPost]
        [Route("/Picture/Deletes")]
        public IActionResult Deletes(DeleteItemsVm model)
        {
            foreach (var item in model.Items)
            {
                _pictureRepository.Delete(item);
            }
            _pictureRepository.Save();

            return Redirect(model.ReturnUrl);
        }
    }
}