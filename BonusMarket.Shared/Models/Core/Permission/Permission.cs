using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonusMarket.Shared.Models.Core.Permission
{
    public class Permission
    {
        public int Id { get; set; }
        public int ModuleNumber { get; set; }
        public string ModuleName { get; set; }
        public int PermissionNumber { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool Status { get; set; } = true;
    }
}