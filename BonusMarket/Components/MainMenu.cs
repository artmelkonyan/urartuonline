using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System;
using System.Threading;

namespace BonusMarket.Components
{
    public class MainMenuViewComponent : ViewComponent
    {
        public string RequestLanguage { get; set; }
        CategoryLayer _categoryLayer;
        public MainMenuViewComponent()
        {
            RequestLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            _categoryLayer = new CategoryLayer();    
        }

        public IViewComponentResult Invoke(bool isMobile = false)
        {
            ViewBag.IsMobile = isMobile;
            Tuple<List<CategoryEntity>, List<CategoryEntity>> tuple = _categoryLayer.GetLayoutCategories(RequestLanguage);
            return View(tuple.Item1);
        }
    }
}
