using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Shared.Models.Core.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BonusMarket.Admin.Models.Auth;
using BonusMarket.Shared.Models.Core.User;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Models.Core.User;
using BonusMarket.Shared.Services;

namespace BonusMarket.Admin.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthorizationService _authorizationService;

        public AuthController(
            AuthorizationService authorizationService,
            ILogger<AuthController> logger
            )
        {
            _logger = logger;
            _authorizationService = authorizationService;
           
        }

        #region Login

        /// <summary>
        /// Login View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                    return await Task.Run(() => RedirectToAction("Index", "Home"));

                return await Task.Run(() => View(new LoginVM { ReturnUrl = returnUrl }));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message + " Date:" + DateTime.Now.ToString());
                return await Task.Run(() => StatusCode(503));
            }
        }

        /// <summary>
        /// Login Processing Unit after post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _authorizationService.Login(model.Email, model.Password);

                    if (user == null)
                    {
                        ModelState.AddModelError("ModelStateNotValid", AuthLoginErrMsg.ModelStateNotValidErrMsg);
                        return await Task.Run(() => View(model));
                    }
                        
                    await SignInUser(Request.HttpContext, user);

                        // register account activity
                        //get ip
                        HttpContext context = HttpContext.Request.HttpContext;
                        string ip = context.Request.HttpContext.Features.ToString();
                        var ipr = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                        //_userActivityLog.AddAccountActivity(user.Id, UserAccountActivityLogEnum.Login, _userActivityLogHelper.GetDetails(Request));

                        return await Task.Run(() =>
                            Redirect(String.IsNullOrEmpty(model.ReturnUrl) ? "/" : model.ReturnUrl));
                }

                ModelState.AddModelError("ModelStateNotValid", AuthLoginErrMsg.ModelStateNotValidErrMsg);
                return await Task.Run(() => View(model));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message + " Date:" + DateTime.Now.ToString());
                return await Task.Run(() => StatusCode(503));
            }
        }
        
        private async Task SignInUser(HttpContext context, User user)
        {
            try
            {
                var claims = new List<Claim>();

                if (!string.IsNullOrEmpty(user.Email))
                    claims.Add(new Claim(ClaimTypes.Email, user.Id.ToString(), ClaimValueTypes.Email, BonusMarketAdminAuthenticationDefaults.ClaimsIssuer));

                //create principal for the current authentication scheme
                var userIdentity = new ClaimsIdentity(claims, BonusMarketAdminAuthenticationDefaults.AuthenticationScheme);
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                //set value indicating whether session is persisted and the time at which the authentication was issued
                var authenticationProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                };

                // sign in
                await context.SignInAsync(BonusMarketAdminAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion login

        #region Logout

        /// <summary>
        /// Logout action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                // TODO normal logout
                //and sign out from the current authentication scheme
                await Request.HttpContext.SignOutAsync(BonusMarketAdminAuthenticationDefaults.AuthenticationScheme);

                return await Task.Run(() => Redirect("Login"));
            }
            catch (Exception ex)
            {
                // TODO Exception Handling
                Console.WriteLine(ex.Message);
                return await Task.Run(() => StatusCode(503));
            }
        }

        #endregion

    }
}
