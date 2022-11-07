using Microsoft.AspNetCore.Authentication.Cookies;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BonusMarket.Managers
{
    public class UserManager
    {
        public static ClaimsPrincipal GetPrincipal(LoginEntity model)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));

            // Authenticate using the identity
            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
