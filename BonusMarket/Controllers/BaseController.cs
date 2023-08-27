using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using BusinessLayer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Managers;
using BonusMarket.Shared.Models.Core.User;

namespace BonusMarket.Controllers
{
    public class BaseController : Controller
    {

        public string RequestLanguage { get; set; }
        public BaseViewModel BaseModel { get; set; }

        public BaseController()
        {
            RequestLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            CategoryLayer c_layer = new CategoryLayer();
            PictureLayer p_layer = new PictureLayer();
            BannerLayer b_layer = new BannerLayer();
            BrandLayer br_layer = new BrandLayer();
            this.BaseModel = new BaseViewModel();
            foreach (var item in this.BaseModel.Banners)
            {
                item.Picture = p_layer.GetPictureById(item.PictureId);
            }
            this.BaseModel.Banners = b_layer.GetAll().Where(x => x.Show).OrderBy(x => x.OrderId).ToList();
            Tuple<List<CategoryEntity>, List<CategoryEntity>> tuple = c_layer.GetLayoutCategories(RequestLanguage);
            this.BaseModel.Categories = tuple.Item1;
            this.BaseModel.CategoryList = tuple.Item2;
        }
    }
}
