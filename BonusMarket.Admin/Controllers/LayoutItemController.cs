using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.Models.LayoutItem;
using BonusMarket.Admin.ModelValidation.Auth;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Repository.LayoutItem;

namespace BonusMarket.Admin.Controllers
{
    [AuthorizeUser((int)Modules.Auth, new int[] { (int)AuthModule.Login })]
    public class LayoutItemController : Controller
    {
        private readonly LayoutItemRepository _layoutItemRepository;

        public LayoutItemController(LayoutItemRepository categoryRepository)
        {
            _layoutItemRepository = categoryRepository;
        }
        
        [Route("/LayoutItem")]
        [Route("/LayoutItem/Index")]
        [HttpGet]
        public IActionResult Index(LayoutItemPagingVm paging)
        {
            var model = new LayoutItemVm();
            model.ParentId = paging.ParentId;
            model.List = _layoutItemRepository.GetList(new LayoutItemFilter() { CurrentPage =  paging.Page, PageSize = paging.PageSize, ParentId = paging.ParentId});
            return View(model);
        }

        [HttpGet]
        [Route("/LayoutItem/Add")]
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        [Route("/LayoutItem/Add/")]
        public IActionResult Add(LayoutItemAddPostVm model)
        {
            _layoutItemRepository.Add(model.GetDbModel());
            _layoutItemRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/LayoutItem/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = _layoutItemRepository.Get(id);
            LayoutItemEditVm model = new LayoutItemEditVm(category);

            model.LayoutItems = _layoutItemRepository.GetList(new LayoutItemFilter()
            {
                PageSize = 10000,
                SkipParent = true
            }).Results;
            return View(model);
        }

        
        [HttpPost]
        [Route("/LayoutItem/Edit")]
        public IActionResult Edit(LayoutItemEditVm model)
        {
            _layoutItemRepository.Update(model.GetDbModel());
            _layoutItemRepository.Save();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("/LayoutItem/Delete")]
        public IActionResult Delete([FromBody] IEnumerable<int> model)
        {
            foreach (var item in model)
            {
                _layoutItemRepository.Delete(item);
            }
            _layoutItemRepository.Save();
            return new JsonResult(new {});
        }
        
        [HttpPost]
        [Route("/LayoutItem/Deletes")]
        public IActionResult Deletes(DeleteItemsVm model)
        {
            foreach (var item in model.Items)
            {
                _layoutItemRepository.Delete(item);
            }
            _layoutItemRepository.Save();

            return Redirect(model.ReturnUrl);
        }
    }
}