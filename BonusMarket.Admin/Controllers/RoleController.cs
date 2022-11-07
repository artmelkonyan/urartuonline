using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.Models.Role;
using BonusMarket.Admin.ModelValidation.Auth;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Models.Core.Permission;
using BonusMarket.Shared.Models.Core.Role;
using BonusMarket.Shared.Repository.Permission;
using BonusMarket.Shared.Repository.Role;

namespace BonusMarket.Admin.Controllers
{
    [AuthorizeUser((int)Modules.Admin, new int[] { (int)AdminModule.Index })]
    public class RoleController : Controller
    {
        private readonly RoleRepository _roleRepository;
        private readonly PermissionRepository _permissionRepository;

        public RoleController(
            RoleRepository roleRepository,
            PermissionRepository permissionRepository
            )
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }
        [Route("/Role")]
        [Route("/Role/Index")]
        [HttpGet]
        public IActionResult Index(RolePagingVm paging)
        {
            var model = new RoleVm();
            model.List = _roleRepository.GetList(new RoleFilter() { CurrentPage =  paging.Page, PageSize = paging.PageSize});
            return View(model);
        }

        [HttpGet]
        [Route("/Role/Add")]
        public IActionResult Add()
        {
            RoleEditVm model = new RoleEditVm();
            model.AllPermissions = _permissionRepository.GetList(new PermissionFilter(1, 10000)).Results;   
            return View(model);
        }

        
        [HttpPost]
        [Route("/Role/Add")]
        public IActionResult Add(RoleAddPostVm model)
        {
            var inserted = _roleRepository.Add(model.GetDbModel(), model.NewPermissions);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Role/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var role = _roleRepository.Get(id);
            RoleEditVm model = new RoleEditVm(role);
            model.AllPermissions = _permissionRepository.GetList(new PermissionFilter(1, 10000)).Results;   
            return View(model);
        }

        [HttpPost]
        [Route("/Role/Edit")]
        public IActionResult Edit(RoleEditVm model)
        {
            _roleRepository.Update(model.Id, model.Name, model.SystemName, model.Description, model.SystemRole, model.Active, model.NewPermissions);
            _roleRepository.Save();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("/Role/Delete")]
        public IActionResult Delete([FromBody] IEnumerable<int> model)
        {
            
            foreach (var item in model)
            {
                _roleRepository.Delete(item);
            }
            _roleRepository.Save();
            return new JsonResult(new {});
        }
        
        [HttpPost]
        [Route("/Role/Deletes")]
        public IActionResult Deletes(DeleteItemsVm model)
        {
            foreach (var item in model.Items)
            {
                _roleRepository.Delete(item);
            }
            _roleRepository.Save();

            return Redirect(model.ReturnUrl);
        }
    }
}