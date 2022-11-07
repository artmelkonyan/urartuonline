using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.Models.Permission;
using BonusMarket.Admin.ModelValidation.Auth;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Models.Core.Permission;
using BonusMarket.Shared.Repository.Permission;

namespace BonusMarket.Admin.Controllers
{
    [AuthorizeUser((int)Modules.Admin, new int[] { (int)AdminModule.Index })]
    public class PermissionController : Controller
    {
        private readonly PermissionRepository _permissionRepository;

        public PermissionController(PermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        
        [Route("/Permission")]
        [Route("/Permission/Index")]
        [HttpGet]
        public IActionResult Index(PermissionPagingVm paging)
        {
            var model = new PermissionVm();
            model.List = _permissionRepository.GetList(new PermissionFilter() { CurrentPage =  paging.Page, PageSize = paging.PageSize});
            return View(model);
        }

        [HttpGet]
        [Route("/Permission/Add")]
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        [Route("/Permission/Add")]
        public IActionResult Add(PermissionAddPostVm model)
        {
            _permissionRepository.Add(model.GetDbModel());
            _permissionRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Permission/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var permission = _permissionRepository.Get(id);
            PermissionEditVm model = new PermissionEditVm(permission);
            
            return View(model);
        }

        
        [HttpPost]
        [Route("/Permission/Edit")]
        public IActionResult Edit(PermissionEditVm model)
        {
            _permissionRepository.Update(model.Id, model.ModuleNumber, model.ModuleName,model.PermissionNumber,model.PermissionName, model.Description);
            _permissionRepository.Save();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("/Permission/Delete")]
        public IActionResult Delete([FromBody] IEnumerable<int> model)
        {
            
            foreach (var item in model)
            {
                _permissionRepository.Delete(item);
            }
            _permissionRepository.Save();
            return new JsonResult(new {});
        }
        
        [HttpPost]
        [Route("/Permission/Deletes")]
        public IActionResult Deletes(DeleteItemsVm model)
        {
            foreach (var item in model.Items)
            {
                _permissionRepository.Delete(item);
            }
            _permissionRepository.Save();

            return Redirect(model.ReturnUrl);
        }
    }
}