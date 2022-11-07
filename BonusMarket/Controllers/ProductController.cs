using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace BonusMarket.Controllers
{
    public class ProductController : BaseController
    {

        //[Authorize]
        public IActionResult OneProductById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            this.ViewData["BaseModel"] = this.BaseModel;
            ProductLayer p_layer = new ProductLayer();
            OneProductViewModel Model = new OneProductViewModel();
            Model.OneProduct = p_layer.GetOneProductById(id, RequestLanguage);

            if (Model.OneProduct.Count < 1)
                return StatusCode(404);

            return View("OneProduct", Model);
        }

        public IActionResult ProductsList(int[] brandIds, int categoryId = 0, int currentPage = 1, string orderBy = null)
        {
            var viewCount = 12;
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            this.ViewData["BaseModel"] = this.BaseModel;
            ProductLayer p_layer = new ProductLayer();
            CategoryLayer categoryLayer = new CategoryLayer();
            BrandLayer brandLayer = new BrandLayer();
            ProductsViewModel Model = new ProductsViewModel();
            Tuple<int, List<ProductEntity>> tuple;
            if (!brandIds.Any() && categoryId == 0)
            {
                ViewData["Brands"] = brandLayer.GetAll(RequestLanguage);
                return View("Products", Model);
            }
            if (categoryId != 0 && !brandIds.Any())
            {
                Model.CategoryName = new CategoryLayer().GetCategoryById(categoryId, RequestLanguage).Translation.Name;
                tuple = p_layer.GetProductListForUser(categoryId, currentPage, viewCount, orderBy, RequestLanguage);
                Model.Products = tuple.Item2;
                Model.TotalCount = tuple.Item1;
                Model.TotalPages = (Model.TotalCount / viewCount) + 1;
                Model.ViewCount = viewCount;
                Model.CurrentPage = currentPage;
                Model.CategoryId = categoryId;
                ViewData["Brands"] = brandLayer.GetBrandsByCategoryId(categoryId, RequestLanguage);
                Model.BrandIds = brandIds;
                Model.Order = orderBy;
                return View("Products", Model);
            }
            var cats = new List<CategoryEntity>();
            var resCats = new List<CategoryEntity>();

            if (brandIds.Any() && categoryId == 0)
            {
                tuple = p_layer.GetProductsByBrandListId(brandIds, currentPage, viewCount, RequestLanguage, orderBy);
                Model.Products = tuple.Item2;
                foreach (var item in brandIds)
                {
                    cats.AddRange(categoryLayer.GetCategoriesByBrandId(item, RequestLanguage));
                }
                cats = cats.Distinct().ToList();
                foreach (var item in cats)
                {
                    if (categoryLayer.GetCategoryByBrandCheck(item.Id.Value)&&!resCats.Any(x => x.Id == item.Id))
                        resCats.Add(item);
                }
                ViewData["Categories"] = resCats;
                Model.TotalCount = tuple.Item1;
                Model.TotalPages = (Model.TotalCount / viewCount) + 1;
                Model.ViewCount = viewCount;
                Model.CurrentPage = currentPage;
                ViewData["Brands"] = brandLayer.GetAll(RequestLanguage);
                Model.BrandIds = brandIds;
                Model.CategoryId = 0;
                return View("Products", Model);
            }
            foreach (var item in brandIds)
            {
                cats.AddRange(categoryLayer.GetCategoriesByBrandId(item, RequestLanguage));
            }
            cats = cats.Distinct().ToList();
            foreach (var item in cats)
            {
                if (categoryLayer.GetCategoryByBrandCheck(item.Id.Value) && !resCats.Any(x => x.Id == item.Id))
                    resCats.Add(item);
            }
            ViewData["Categories"] = resCats;
            Model.CategoryName = new CategoryLayer().GetCategoryById(categoryId, RequestLanguage).Translation.Name;
            tuple = p_layer.GetProductsByBrandListId(brandIds, categoryId, currentPage, viewCount, RequestLanguage, orderBy );
            Model.Products = tuple.Item2;
            Model.TotalCount = tuple.Item1;
            Model.TotalPages = (Model.TotalCount / viewCount) + 1;
            Model.ViewCount = viewCount;
            ViewData["Brands"] = brandLayer.GetBrandsByCategoryId(categoryId, RequestLanguage);
            Model.CurrentPage = currentPage;
            Model.CategoryId = categoryId;
            Model.BrandIds = brandIds;
            Model.Order = orderBy;
            return View("Products", Model);
        }
        public IActionResult ProductsByCategoryId(int CategoryId, int CurrentPage = 1, int ViewCount = 12, string OrderBy = null)
        {
            RequestLanguage = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            this.ViewData["BaseModel"] = this.BaseModel;
            ViewBag.CategoryName = "nameeeee";
            ProductLayer p_layer = new ProductLayer();
            ProductsViewModel Model = new ProductsViewModel();
            Model.CategoryName = new CategoryLayer().GetCategoryById(CategoryId, RequestLanguage).Translation.Name;
            Tuple<int, List<ProductEntity>> tuple = p_layer.GetProductListForUser(CategoryId, CurrentPage, ViewCount, OrderBy, RequestLanguage);
            Model.Products = tuple.Item2;
            Model.TotalCount = tuple.Item1;
            Model.TotalPages = (Model.TotalCount / ViewCount) + 1;
            Model.ViewCount = ViewCount;
            Model.CurrentPage = CurrentPage;
            Model.CategoryId = CategoryId;
            Model.Order = OrderBy;
            return View("Products", Model);
        }

        public IActionResult ProductsByBrandId(int id, int currentPage = 1, string OrderBy = null)
        {
            var viewCount = 12;
            this.ViewData["BaseModel"] = this.BaseModel;
            ProductLayer p_layer = new ProductLayer();
            ProductsViewModel Model = new ProductsViewModel();
            Tuple<int, List<ProductEntity>> tuple = p_layer.GetProductsByBrandId(id, currentPage, viewCount, RequestLanguage, OrderBy);
            Model.Products = tuple.Item2;
            Model.TotalCount = tuple.Item1;
            Model.TotalPages = (Model.TotalCount / viewCount) + 1;
            Model.ViewCount = viewCount;
            Model.CurrentPage = currentPage;
            Model.BrandIds = new int[] { id };
            return View("Products", Model);
        }
        
        [HttpPost]
        public JsonResult GetProductsByIdList([FromBody] WishListEntity entity)
        {
            string joined = string.Join(",", entity.idList);
            List<ProductEntity> list = new ProductLayer().GetProductByIdList(joined);
            list = list.Where(x =>x.Price > 0).ToList();
            foreach (var ent in list)
            {
                ent.MainImage.FullPath = ImageHelper.GenImageLink(ent.MainImage.FullPath);
            }
            return new JsonResult(list);
        }
    }
}