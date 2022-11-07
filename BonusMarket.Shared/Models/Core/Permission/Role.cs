using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonusMarket.Shared.Models.Core.Permission
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public bool SystemRole { get; set; }
        public bool Active { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool Status { get; set; } = true;

        public virtual List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}