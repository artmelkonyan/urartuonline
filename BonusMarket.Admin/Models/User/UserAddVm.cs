using System.Collections.Generic;
using BonusMarket.Admin.Models.Role;

namespace BonusMarket.Admin.Models.User
{
    public class UserAddVm : UserBaseVm
    {
        public IList<Shared.Models.Core.Permission.Role> Roles { get; set; }
        public UserAddVm()
        {
            Roles = new List<Shared.Models.Core.Permission.Role>();
        }

    }
}