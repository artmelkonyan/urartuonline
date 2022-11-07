using System;
using System.Linq;
using System.Security.Claims;
using System.Net;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Repository.User;
using BonusMarket.Shared.Services;

namespace BonusMarket.Shared.Services
{
    public class AuthorizeUserHelper
    {
        private readonly Context _context;
        private readonly UserRepository _userRepository;
        private readonly AuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Perihelion.Helpers.AuthorizeUserHelper"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public AuthorizeUserHelper(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Hases the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was hased, <c>false</c> otherwise.</returns>
        /// <param name="User">User.</param>
        /// <param name="module">Module.</param>
        /// <param name="permissionList">Permission list.</param>
        public bool HasPermission(System.Security.Claims.ClaimsPrincipal User, short module, params int[] permissionList) {
            try
            {
                bool isAuthenticated = User.Identity.IsAuthenticated;

                if (!isAuthenticated)
                    return false;

                // check the permission
                int userId = Int32.Parse(User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Email).Value);
                var user = _userRepository.getById(userId);

                if (user == null)
                    return false;

                AuthorizationState state = AuthorizationService.HasPermission(user.UserRoles, module, permissionList);

                switch (state)
                {
                    case AuthorizationState.HasPermission:
                        return true;                            
                    default:
                        return false;

                }
            } catch(Exception e) {
                throw e;
            }
        }


        /// <summary>
        /// Hases the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was hased, <c>false</c> otherwise.</returns>
        /// <param name="User">User.</param>
        /// <param name="module">Module.</param>
        /// <param name="permissionList">Permission list.</param>
        public bool HasPermissionAndEmailVerified(System.Security.Claims.ClaimsPrincipal User)
        {
            try
            {
                bool isAuthenticated = User.Identity.IsAuthenticated;

                if (!isAuthenticated)
                    return false;

                // check the permission
                string email = User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Email).Value;

                // if check for state
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Hases the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was hased, <c>false</c> otherwise.</returns>
        /// <param name="User">User.</param>
        public bool HasPermission(System.Security.Claims.ClaimsPrincipal User) {
            try
            {
                return User.Identity.IsAuthenticated;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}