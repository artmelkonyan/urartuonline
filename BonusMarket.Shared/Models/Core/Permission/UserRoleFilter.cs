using BonusMarket.Shared.Models.Core.Filter;

namespace BonusMarket.Shared.Models.Core.Permission
{
    public class UserRoleFilter : FilterBase
    {
        public int? UserId { get; set; }
    }
}