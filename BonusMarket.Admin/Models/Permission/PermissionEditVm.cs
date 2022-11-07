namespace BonusMarket.Admin.Models.Permission
{
    public class PermissionEditVm : PermissionBaseVm
    {
        public int Id { get; set; }

        public PermissionEditVm()
        {
            
        }
        public PermissionEditVm(Shared.Models.Core.Permission.Permission permission)
        {
            this.Id = permission.Id;
            this.ModuleNumber = permission.ModuleNumber;
            this.ModuleName = permission.ModuleName;
            this.PermissionNumber = permission.PermissionNumber;
            this.PermissionName = permission.PermissionName;
            this.Description = permission.Description;
        }
    }
}