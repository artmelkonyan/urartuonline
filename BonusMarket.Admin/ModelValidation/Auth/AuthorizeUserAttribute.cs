using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BonusMarket.Admin.ModelValidation.Auth
{
    /// <summary>
    /// Overridern Authorize user attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed partial class AuthorizeUserAttribute : ActionFilterAttribute
    {
        private short _module;
        private List<int> _permissionNumberList;
        public bool OnlyHttpStatus = false;
        private readonly bool _checkActivation;
        private readonly bool _checkVerification;
        
        
        /// <summary>
        /// Constructor which accepts permissions
        /// </summary>
        /// <param name="module"></param>
        /// <param name="permissionList"></param>
        /// <param name="checkActivation"></param>
        /// <param name="checkVerification"></param>
        public AuthorizeUserAttribute(short module, int[] permissionList, bool checkActivation = true, bool checkVerification = true)
        {
            _module = module;
            _permissionNumberList = new List<int>(permissionList);
            _checkActivation = checkActivation;
            _checkVerification = checkVerification;
        }

        /// <summary>
        /// Overriden function for validation
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // check if authenticated or not;
            base.OnActionExecuting(context);
            bool authenticated = context.HttpContext.User.Identity.IsAuthenticated;

            // redirect to url
            if (!authenticated)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        controller = "Auth",
                        action = "Login",
                        returnUrl = context.HttpContext.Request.Path
                    }));
                return;
            }

            /*
            // get user
            //var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            var userGetService = (IUserDetail)context.HttpContext.RequestServices.GetService(typeof(IUserDetail));

            string email = context.HttpContext.User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Email).Value;
            UserEntity user = userGetService.GetCachedUserByEmail(email);

            if (user == null)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        controller = "Auth",
                        action = "Logout",
                        returnUrl = context.HttpContext.Request.Path
                    }));
                return;
            }

            // check block
            if (user.Blocked)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        controller = "Auth",
                        action = "Blocked"
                    }));
                return;
            }

            // check activation
            if (_checkActivation)
            {
                switch (user.State)
                {
                    case UserState.Active:
                        break;
                    case UserState.Inactive:
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                            new
                            {
                                controller = "Auth",
                                action = "Logout",
                                returnUrl = context.HttpContext.Request.Path
                            }));
                        return;
                    case UserState.Pending:
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                            new
                            {
                                controller = "Auth",
                                action = "AccountActivation",
                                returnUrl = context.HttpContext.Request.Path
                            }));
                        return;
                }
            }

            if (_checkVerification)
            {
                switch (user.VerificationState) {
                    case UserVerificationState.Approved:
                        break;
                    case UserVerificationState.Pending:
                    case UserVerificationState.Rejected:
                        break;
                        // allow user to use balance while rejected
                        //context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        //    new
                        //    {
                        //        controller = "Auth",
                        //        action = "AccountVerification",
                        //        returnUrl = context.HttpContext.Request.Path
                        //    }));
                        //return;
                }
            }

            // check permission
            AuthorizationState state = Authorization.HasPermission(user.Roles, _module, _permissionNumberList);

            switch (state)
            {
                case AuthorizationState.HasPermission:
                    return;
                case AuthorizationState.UnAuthorized:
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new StatusCodeResult(401);
                    return;
                case AuthorizationState.Internal:
                    context.HttpContext.Response.StatusCode = 503;
                    context.Result = new StatusCodeResult(503);
                    return;
                case AuthorizationState.Forbidden:
                default:
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new StatusCodeResult(403);
                    return;
            } */
        }
    }
}
