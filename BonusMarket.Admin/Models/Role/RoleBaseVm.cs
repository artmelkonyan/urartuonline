using System.Collections.Generic;

namespace BonusMarket.Admin.Models.Role
{
    public class RoleBaseVm
    {
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public bool SystemRole { get; set; }
        public bool Active { get; set; }
    }
}