namespace BonusMarket.Admin.Models.Permission
{
    public class PermissionBaseVm
    {
        public int ModuleNumber { get; set; }
        public string ModuleName { get; set; }
        public int PermissionNumber { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
    }
}