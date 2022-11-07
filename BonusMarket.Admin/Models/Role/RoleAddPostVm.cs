using System.Collections.Generic;

namespace BonusMarket.Admin.Models.Role
{
    public class RoleAddPostVm : RoleBaseVm
    {
        public IList<int> NewPermissions { get; set; }

        public RoleAddPostVm()
        {
            NewPermissions = new List<int>();
        }
        public Shared.Models.Core.Permission.Role GetDbModel()
        {
            return new Shared.Models.Core.Permission.Role()
            {
                Name = this.Name,
                SystemName = this.SystemName,
                SystemRole = this.SystemRole,
                Active = this.Active,
                Description = this.Description
            };
        }
    }
}