using System.Collections.Generic;

namespace BonusMarket.Admin.Models.Role
{
    public class RoleEditVm : RoleBaseVm
    {
        public int Id { get; set; }
        public IList<int> NewPermissions { get; set; }
        public IList<Shared.Models.Core.Permission.Permission> AllPermissions { get; set; }
        
        public IList<Shared.Models.Core.Permission.RolePermission> RolePermissions { get; set; }

        public RoleEditVm()
        {
            RolePermissions = new List<Shared.Models.Core.Permission.RolePermission>();
            NewPermissions = new List<int>();
            AllPermissions = new List<Shared.Models.Core.Permission.Permission>();
        }

        public RoleEditVm(Shared.Models.Core.Permission.Role m)
        {
            Id = m.ID;
            Name = m.Name;
            SystemName = m.SystemName;
            SystemRole = m.SystemRole;
            Active = m.Active;
            Description = m.Description;
            RolePermissions = m.RolePermissions;
            AllPermissions = new List<Shared.Models.Core.Permission.Permission>();
            NewPermissions = new List<int>();
        }
    }
}