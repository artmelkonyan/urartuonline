using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Models;
using Models;
using BusinessLayer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using UnidecodeSharpFork;
using Models.EntityModels.ViewModels;
using WebShop;
using System.ServiceModel;
using BonusMarket.Factories;
using System.Net.Http;

namespace BonusMarket.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            ProductLayer p_layer = new ProductLayer();
            CategoryLayer c_layer = new CategoryLayer();
            BrandLayer brandLayer = new BrandLayer();
            HomeViewModel Model = new HomeViewModel();
            Model.HomePageCategories = c_layer.GetHomePageCategories(RequestLanguage);
            Model.HomePageProducts = p_layer.GetHomePageProducts(RequestLanguage).Where(x => x.Count > 0).ToList();
            Model.HomeBrands = brandLayer.GetOnHomePage(RequestLanguage).Take(6).ToList();
            return View(Model);
        }
        public IActionResult PrivacyPolicy()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        public IActionResult Search(string brandSearchWord, string searchword, string searchwordFixed, int CurrentPage = 1, int ViewCount = 12, string Order = "")
        {
            if (string.IsNullOrEmpty(searchword) && string.IsNullOrEmpty(brandSearchWord))
                return RedirectToAction("Index");
            this.ViewData["BaseModel"] = this.BaseModel;
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            ProductLayer p_layer = new ProductLayer();
            SearchLear searchLear = new SearchLear();

            searchwordFixed = searchLear.GetArm(searchword).FirstOrDefault();
            ProductsViewModel Model = new ProductsViewModel();
            Tuple<int, List<ProductEntity>> tuple;
            if (string.IsNullOrEmpty(brandSearchWord) && !string.IsNullOrEmpty(searchword))
            {
                tuple = p_layer.Search(searchword, searchwordFixed, CurrentPage, ViewCount, RequestLanguage, Order);
                Model.Products = tuple.Item2;
                Model.SearchWordFixed = searchwordFixed;
                Model.TotalCount = tuple.Item1;
                Model.BrandName = "";
                Model.TotalPages = (Model.TotalCount / ViewCount) + 1;
                Model.ViewCount = ViewCount;
                Model.SearchWord = searchword;
                Model.Order = Order;
                Model.CurrentPage = CurrentPage;
                return View(Model);
            }
            if (!string.IsNullOrEmpty(brandSearchWord) && string.IsNullOrEmpty(searchword))
            {
                searchwordFixed = searchLear.GetArm(brandSearchWord).FirstOrDefault();
                tuple = p_layer.SearchByBrandName(brandSearchWord, searchwordFixed, CurrentPage, ViewCount, RequestLanguage, Order);
                Model.Products = tuple.Item2;
                Model.SearchWordFixed = searchwordFixed;
                Model.TotalCount = tuple.Item1;
                Model.BrandName = brandSearchWord;
                Model.TotalPages = (Model.TotalCount / ViewCount) + 1;
                Model.ViewCount = ViewCount;
                Model.SearchWord = searchword;
                Model.Order = Order;
                Model.CurrentPage = CurrentPage;
                return View(Model);
            }
            tuple = p_layer.SearchByBrandNameAndProductName(brandSearchWord, searchword, searchwordFixed, CurrentPage, ViewCount, RequestLanguage, Order);
            Model.Products = tuple.Item2;
            Model.SearchWordFixed = searchwordFixed;
            Model.TotalCount = tuple.Item1;
            Model.BrandName = brandSearchWord;
            Model.TotalPages = (Model.TotalCount / ViewCount) + 1;
            Model.ViewCount = ViewCount;
            Model.SearchWord = searchword;
            Model.Order = Order;
            Model.CurrentPage = CurrentPage;
            return View(Model);

        }
        [HttpPost]
        public IActionResult SearchByBrandResult(string brandSearchword, string searchword, string searchwordFixed)
        {
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            ProductLayer p_layer = new ProductLayer();
            BrandLayer brandLayer = new BrandLayer();
            SearchLear s_layer = new SearchLear();
            var searchwordFixeds = s_layer.GetArm(searchword);
            var res = new List<ProductEntity>();
            foreach (var item in searchwordFixeds)
            {
                var test = p_layer.SearchByBrandName(searchword, item, 1, 15, RequestLanguage, "");
                if (test.Item2.Any())
                    res.AddRange(test.Item2);
            }
            var model = res.Distinct();
            return Json(model);
        }
        [HttpPost]
        public IActionResult SearchResult(string searchword, string searchwordFixed)
        {
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            ProductLayer p_layer = new ProductLayer();
            SearchLear s_layer = new SearchLear();
            var searchwordFixeds = s_layer.GetArm(searchword);
            searchwordFixeds.Add(searchword);
            var res = new List<ProductEntity>();
            foreach (var item in searchwordFixeds)
            {
                var test = p_layer.Search(searchword, item, 1, 15, RequestLanguage, "");
                if (test.Item2.Any())
                    res.AddRange(test.Item2);
            }
            var model = res.Distinct();
            return Json(model);
        }
        [HttpPost]
        public IActionResult BrandSearchResult(string searchword, string searchwordFixed)
        {
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            SearchLear s_layer = new SearchLear();
            BrandLayer brandLayer = new BrandLayer();
            var searchwordFixeds = s_layer.GetArm(searchword);
            var res = new List<BrandModel>();
            foreach (var item in searchwordFixeds)
            {
                var test = brandLayer.GetBrandsBySearch(searchword, RequestLanguage);
                if (test.Any())
                {
                    res.AddRange(test);
                }
            }
            var model = res.GroupBy(x => x.Id).SelectMany(x => x.Take(1));
            return Json(model);
        }
        public IActionResult ReturnPolicy()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        public IActionResult About()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        public IActionResult Contacts()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        public IActionResult WishList()
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            return View();
        }
        public IActionResult Basket(bool haveError = false)
        {
            this.ViewData["BaseModel"] = this.BaseModel;
            ViewBag.ShowErrorMessage = haveError;
            ViewBag.buyerr = HttpContext.Session.GetString("buyerr");
            HttpContext.Session.SetString("buyerr", "");
            return View();
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public IActionResult Error(bool isTechnical = false)
        {
            return View("Error");
        }
    }
}