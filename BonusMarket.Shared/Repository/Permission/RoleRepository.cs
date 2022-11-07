using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.Permission;
using BonusMarket.Shared.Models.Core.Role;
using BonusMarket.Shared.Repository.Shared;

namespace BonusMarket.Shared.Repository.Role
{
    public class RoleRepository
    {
        private readonly Context _context;

        public RoleRepository(Context context)
        {
            _context = context;
        }
        
        public Models.Core.Permission.Role Get(int id)
        {
            return _context.Roles.Where(r => (r.ID == id && r.Status)).Include(r => r.RolePermissions).Include(r => r.RolePermissions).Select(r => r).FirstOrDefault();
        }

        public PagedResult<Models.Core.Permission.Role> GetList(RoleFilter filter)
        {
            return _context.Roles.Where(r => (r.Status))
                .Include(r => r.RolePermissions).AsQueryable().GetPaged(filter.CurrentPage, filter.PageSize);
        }

        public Models.Core.Permission.Role Add(Models.Core.Permission.Role model, IEnumerable<int> permissions)
        {
            model.CreationDate = DateTime.UtcNow;
            model.Status = true;
            _context.Roles.Add(model);
            Save();
            this.UpdateRolePermissions(model, permissions);
            Save();
            return model;
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        public bool Update(int id, string name, string systemName, string description, bool? systemRole, bool? active, IEnumerable<int> permissions)
        {
            var item = _context.Roles.Where(
                r => r.Status
                     && r.ID == id).Include(r => r.RolePermissions).FirstOrDefault();
            item.Name = name == null ? item.Name : name;
            item.SystemName = systemName == null ? item.SystemName : systemName;
            item.Description = description == null ? item.Description : description;
            item.SystemRole = systemRole == null ? item.SystemRole : systemRole.Value;
            item.Active = active == null ? item.Active : active.Value;
            _context.Roles.Update(item);

            this.UpdateRolePermissions(item, permissions);

            _context.SaveChanges();
            
            return true;
        }

        public void Delete(int id)
        {
            var item = _context.Roles.Where(
                r => r.Status
                     && r.ID == id).FirstOrDefault();
            item.Status = false;
            _context.Roles.Update(item);
            _context.SaveChanges();
        }

        public void UpdateRolePermissions(Models.Core.Permission.Role role, IEnumerable<int> permissions)
        {
            foreach (var elem in role.RolePermissions)
            {
                _context.RolePermissions.Remove(elem);
            }
            
            foreach (var elem in permissions)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    Role = role,
                    PermissionId = elem
                });
            }
        }
    }
}