namespace BonusMarket.Admin.Models.Permission
{
    public class PermissionAddPostVm : PermissionBaseVm
    {
        public Shared.Models.Core.Permission.Permission GetDbModel()
        {
            return new Shared.Models.Core.Permission.Permission()
            {
                ModuleName = this.ModuleName,
                ModuleNumber = this.ModuleNumber,
                PermissionName = this.PermissionName,
                PermissionNumber = this.PermissionNumber,
                Description = this.Description
            };
        }
    }
}