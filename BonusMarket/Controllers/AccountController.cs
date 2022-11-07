using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonusMarket.Managers;
using BonusMarket.Models;
using BusinessLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BonusMarket.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Cart()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        public IActionResult WishList()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(LoginEntity model)
        {
            this.ViewData["BaseModel"] = this.BaseModel;

            LoginLayer l_layer = new LoginLayer();
            LoginEntity loginEntity = new LoginEntity();
            loginEntity= l_layer.SignIn(model);
           
            if (!loginEntity.LoggedIn.Value)
            {
                return RedirectToAction("Login",loginEntity);
            }
            var principal = UserManager.GetPrincipal(model);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal: principal);

            return RedirectToAction("Index","Home");
        }

        public IActionResult SignUp(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 6)
            {
                return View("Error");
            }
            if (string.IsNullOrWhiteSpace(model.Email) || model.Email.Length < 6)
            {
                return View("Error");
            }
            if (string.IsNullOrWhiteSpace(model.FirstName) || model.FirstName.Length < 2)
            {
                return View("Error");
            }
            if (string.IsNullOrWhiteSpace(model.LastName) || model.FirstName.Length < 3)
            {
                return View("Error");
            }
            if (string.IsNullOrWhiteSpace(model.Phone) || model.Phone.Length < 6)
            {
                return View("Error");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
           
            LoginLayer l_layer = new LoginLayer();

            UserEntity user = new UserEntity
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Address = model.Address,
                Role = UserRole.ROLE_GUEST,
                Phone = model.Phone
            };

            int? user_id = l_layer.Register(user);

            if (user_id == null)
            {
                return RedirectToAction("Login");
            }

            LoginEntity loginEntity = new LoginEntity();
            loginEntity.PassWord = model.Password;
            loginEntity.User = user;
            loginEntity.UserName = model.Email;
            loginEntity = l_layer.SignIn(loginEntity);

            if (!loginEntity.LoggedIn.Value)
            {
                return RedirectToAction("Login", loginEntity);
            }

            var principal = UserManager.GetPrincipal(loginEntity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal: principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            HttpContext.SignOutAsync(scheme: CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
            
        }

    }
}