using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.Models.Brand;
using BonusMarket.Admin.ModelValidation.Auth;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Repository.Brand;

namespace BonusMarket.Admin.Controllers
{
    [AuthorizeUser((int)Modules.Auth, new int[] { (int)AuthModule.Login })]
    public class BrandController : Controller
    {
        private readonly BrandRepository _brandRepository;

        public BrandController(BrandRepository categoryRepository)
        {
            _brandRepository = categoryRepository;
        }
        
        [Route("/Brand")]
        [Route("/Brand/Index")]
        [HttpGet]
        public IActionResult Index(BrandPagingVm paging)
        {
            var model = new BrandVm();
            model.ParentId = paging.ParentId;
            model.List = _brandRepository.GetList(new BrandFilter() { CurrentPage =  paging.Page, PageSize = paging.PageSize, ParentId = paging.ParentId});
            return View(model);
        }

        [HttpGet]
        [Route("/Brand/Add")]
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        [Route("/Brand/Add/")]
        public IActionResult Add(BrandAddPostVm model)
        {
            _brandRepository.Add(model.GetDbModel());
            _brandRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Brand/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = _brandRepository.Get(id);
            BrandEditVm model = new BrandEditVm(category);

            model.Brands = _brandRepository.GetList(new BrandFilter()
            {
                PageSize = 10000,
                SkipParent = true
            }).Results;
            return View(model);
        }

        
        [HttpPost]
        [Route("/Brand/Edit")]
        public IActionResult Edit(BrandEditVm model)
        {
            _brandRepository.Update(model.GetDbModel());
            _brandRepository.Save();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("/Brand/Delete")]
        public IActionResult Delete([FromBody] IEnumerable<int> model)
        {
            foreach (var item in model)
            {
                _brandRepository.Delete(item);
            }
            _brandRepository.Save();
            return new JsonResult(new {});
        }
        
        [HttpPost]
        [Route("/Brand/Deletes")]
        public IActionResult Deletes(DeleteItemsVm model)
        {
            foreach (var item in model.Items)
            {
                _brandRepository.Delete(item);
            }
            _brandRepository.Save();

            return Redirect(model.ReturnUrl);
        }
    }
}