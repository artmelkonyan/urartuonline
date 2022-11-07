using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusMarket.Shared.Models.Core.Auth
{
    public enum UserRoleState : short
    {
        Inactive = 0,
        Active = 1
    }

    public enum ActivationCodeState : short
    {
        Pending = 0,
        Activated = 1,
        Outdated = 2
    }

    public enum RoleType : short
    {
        System = 1,
        Normal = 2
    }

    public enum AuthorizationState : byte
    {
        UnAuthorized = 1,
        HasPermission = 2,
        BadRequest = 3,
        Internal = 4,
        Forbidden = 5,
    }
}
