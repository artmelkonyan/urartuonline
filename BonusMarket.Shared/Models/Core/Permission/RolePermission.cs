using System;

namespace BonusMarket.Shared.Models.Core.Permission
{
    public class RolePermission
    {
        public int ID { get; set; }
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool Status { get; set; } = true;
        
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}