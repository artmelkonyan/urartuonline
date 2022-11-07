using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Admin.Models;
using BonusMarket.Admin.ModelValidation.Auth;
using BonusMarket.Shared.Models.Core.Auth;

namespace BonusMarket.Admin.Controllers
{
    [AuthorizeUser((int)Modules.Admin, new int[] { (int)AdminModule.Index })]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("/Home/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        

        [HttpGet]
        [Route("/setLanguage")]
        
        public IActionResult SetLanguage([FromQuery] string culture, [FromQuery] string returnUrl)
        {
            if (culture == null)
            {
                return NotFound();
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        
        [HttpPost]
        [Route("/Home/GetDeleteItemsModal")]
        public IActionResult GetDeleteItemsModal([FromBody] DeleteItemsModalVm input)
        {
            return View("Table/_deleteListModal", input);
        }
    }
}