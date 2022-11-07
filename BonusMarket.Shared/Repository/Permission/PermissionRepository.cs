using System;
using System.Linq;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.Permission;
using BonusMarket.Shared.Repository.Shared;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.Permission;

namespace BonusMarket.Shared.Repository.Permission
{
    public class PermissionRepository
    {
        private readonly Context _context;

        public PermissionRepository(Context context)
        {
            _context = context;
        }
        
        public Models.Core.Permission.Permission Get(int id)
        {
            return _context.Permissions.Where(r => (r.Id == id && r.Status)).Select(r => r).FirstOrDefault();
        }

        public PagedResult<Models.Core.Permission.Permission> GetList(PermissionFilter filter)
        {
            return _context.Permissions.Where(r => (r.Status))
                .Select(customerInformation => customerInformation).AsQueryable().GetPaged(filter.CurrentPage, filter.PageSize);
        }

        public Models.Core.Permission.Permission Add(Models.Core.Permission.Permission model)
        {
            model.CreationDate = DateTime.UtcNow;
            model.Status = true;
            _context.Permissions.Add(model);
            Save();
            return model;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(int id, int? module, string moduleName, int? permission, string permissionName, string description)
        {

            var item = _context.Permissions.Where(
                r => r.Status
                     && r.Id == id).FirstOrDefault();
            item.ModuleNumber = module == null ? item.ModuleNumber : module.Value;
            item.ModuleName = moduleName == null ? item.ModuleName : moduleName;
            item.PermissionNumber = permission == null ? item.PermissionNumber : permission.Value;
            item.PermissionName = permissionName == null ? item.PermissionName : permissionName;
            item.Description = description == null ? item.Description : description;
            _context.Permissions.Update(item);
            _context.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            var item = _context.Permissions.Where(
                r => r.Status
                     && r.Id == id).FirstOrDefault();
            item.Status = false;
            _context.Permissions.Update(item);
            _context.SaveChanges();
        }
    }
}