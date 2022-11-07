using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonusMarket.Models;
using BusinessLayer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BonusMarket.Controllers
{
    public class CategoryController : BaseController
    {
        public IActionResult GetCategoriesByParentId(int parentId)
        {
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            //Todo for hayko 
            this.ViewData["BaseModel"] = this.BaseModel;
            CategoryLayer c_layer = new CategoryLayer();
            //c_layer.GetCategoriesByParentId(parentId);
            var model = new CategoryViewModel();
            Tuple<List<CategoryEntity>,List<CategoryEntity>> tuple = c_layer.GetCategoriesTreeByParentId(parentId, RequestLanguage);
            model.CategoriesByParent = tuple.Item1;
            model.CategoryTree = c_layer.GetCategoryTree();
            model.CategoryName = c_layer.GetCategoryById(parentId, RequestLanguage).Translation.Name;
            model.CategoryId = parentId;

            return View("Categories", model);
        }
    }
}