using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BonusMarket.Factories;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.EntityModels;
using Models.EntityModels.ViewModels;

namespace BonusMarket.Controllers
{
    [Authorize()]
    public class AdminController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly AdminLayer _adminLayer;
        public AdminController(IHostingEnvironment hostingEnvironment, AdminLayer adminLayer)
        {
            _hostingEnvironment = hostingEnvironment;
            _adminLayer = adminLayer;
        }
        
        [Authorize()]
        public IActionResult Index(int page = 1, int pageSize = 6, string sku = "", int catPage = 1, int catPageSize = 7, int ordPage = 1, int ordPageSize = 8, string categoryName = "")
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            DashboardViewModel vm = new DashboardViewModel();
            AdminLayer a_layer = new AdminLayer();
            ViewBag.CategorySearch = categoryName;
            ViewBag.search = "notNull";
            if (sku != "")
            {
                vm.Product = a_layer.GetProductBySku(sku);
                vm.DashboardProducts = new List<ProductEntity>();
                vm.DashboardProducts.Add(vm.Product);
                ViewBag.search = null;
            }
            else
            {
                vm.DashboardProducts = a_layer.GetDashboardProducts(page, pageSize);
            }
            vm.DashboardCategories = a_layer.GetDashboardgategories(catPage, catPageSize, categoryName);

            if (vm.DashboardCategories.Count < catPageSize)
            {
                catPage = catPage - 1;
            }
            if (catPage < 1)
            {
                catPage = 1;
            }
            ViewBag.curCatPage = catPage;
            vm.DashboardOrders = a_layer.GetDashboardOrders(ordPage, ordPageSize);
            if (vm.DashboardOrders == null)
                vm.DashboardOrders = new List<OrderEntity>();
            if (vm.DashboardOrders.Count < ordPageSize)
            {
                ordPage = ordPageSize - 1;
            }
            if (ordPage < 1)
            {
                ordPage = 1;
            }
            ViewBag.curOrdPage = ordPage;

            if (vm.DashboardProducts.Count < pageSize)
            {
                page = page - 1;
            }
            if (page < 1)
            {
                page = 1;
            }

            ViewBag.curPage = page;
            return View(vm);
        }
        public IActionResult GetOrder(int id)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            OrderLayer ly = new OrderLayer();
            var result = ly.GetOrderById(id);
            return View("DetailOrder", result);
        }
        [HttpPost]
        public IActionResult EditOrder(OrderEntityViewModel model)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;


            AdminLayer a_layer = new AdminLayer();
            a_layer.EditOrder(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CheckAllDb()
        {
            WebShopFactory wsf = new WebShopFactory(_hostingEnvironment);
            var s = wsf.CheckDbByWCF().Result;
            string res= s ? "Ստուգումը կատարված է":"Error";
            return Content(res);
        }
        public IActionResult AddProductBySku()
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductBySku(string sku)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            WebShopFactory wsf = new WebShopFactory(_hostingEnvironment);
            var id = await wsf.AddProduct(sku);
            if (id == 0)
            {
                return RedirectToAction("AddProductBySku", "Admin");
            }
            //Edit
            return RedirectToAction("Edit", "Admin", new { Id = id });

        }
        [HttpPost]
        public async Task<IActionResult> UploadProductFromXml(IFormFile productXml = null)
        {
            try
            {

                // if (User.Identity.Name != "info@bonusmarket.am")
                // {
                //     return NotFound("Not Found");
                // }
                AdminLayer a_layer = new AdminLayer();
                if (productXml != null)
                {
                    var result = await _adminLayer.AddProductsFromXmlV2(productXml);
                    string res = "";
                    if (result != null)
                    {
                        res = "Ապրաները հաջողությամբ ավելացվել են";
                    }
                    else
                    {
                        res = "Ծրագրային սխալ";
                    }
                    return Content(res);
                }
                return Content("No Xml Uploaded");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? Id, string message = null)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            if (Id == null)
            {
                return NotFound("Not Found");
            }
            ViewBag.Message = message;
            AdminLayer a_layer = new AdminLayer();
            BrandLayer b_layer = new BrandLayer();
            ProductEditViewModel product = a_layer.GetProductById(Id);
            product.BrandList = new List<BrandModel>() {
            new BrandModel()
            {
                Id=0,
                Translate=new BrandTranslate()
                {
                    Name="----"
                }
            }
            };
            product.BrandList.AddRange(b_layer.GetAll());
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model, IFormFile mainImage, IFormFileCollection formFile)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            if (ModelState.IsValid)
            {
                this.ViewData["BaseModel"] = this.BaseModel;
                AdminLayer a_layer = new AdminLayer();

                a_layer.EditProduct(model, mainImage, formFile, _hostingEnvironment.WebRootPath);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", "Admin", new { id = model.Id, message = "Please select category" });

        }
        #region Category
        [HttpGet]
        public IActionResult CreateCateGory()
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            CategoryLayer c_layer = new CategoryLayer();
            CategoryEntityDashboardViewModel category = c_layer.GetCategoryDefultModel();
            return View(category);
        }
        [HttpPost]
        public IActionResult CreateCateGory(CategoryEntityDashboardViewModel model, IFormFile categoryImage)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            AdminLayer a_layer = new AdminLayer();
            var id = a_layer.CreateCateGory(model, categoryImage, _hostingEnvironment.WebRootPath);
            if (id == null)
                return RedirectToAction("Index");
            return RedirectToAction("EditCateGory", new { id = id });
        }
        [HttpGet]
        public IActionResult EditCateGory(int? Id)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            if (Id == null)
            {
                return NotFound("Not Found");
            }

            AdminLayer a_layer = new AdminLayer();
            CategoryEntityDashboardViewModel category = a_layer.GetCategoryById(Id);
            if (category.Picture == null)
                category.Picture = new PictureEntityViewModel();
            return View(category);
        }
        [HttpPost]
        public IActionResult EditCateGory(CategoryEntityDashboardViewModel model, IFormFile categoryImage)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            this.ViewData["BaseModel"] = this.BaseModel;
            AdminLayer a_layer = new AdminLayer();
            a_layer.EditCateGory(model, categoryImage, _hostingEnvironment.WebRootPath);
            return RedirectToAction("Index");
        }
        #endregion
        #region Brand

        public IActionResult EditBrand(int id)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            BrandLayer b_layer = new BrandLayer();
            var model = b_layer.GetByIdForAdmin(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditBrand(BrandAdminModel model)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            BrandLayer b_layer = new BrandLayer();
            b_layer.Update(model, _hostingEnvironment.WebRootPath);
            return RedirectToAction("EditBrand", "Admin", new { id = model.Id });
        }
        public IActionResult AddBrand()
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            BrandLayer b_layer = new BrandLayer();
            var model = b_layer.GetDefultModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddBrand(BrandAdminModel model)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            BrandLayer b_layer = new BrandLayer();
            int id = 0;
            if (ModelState.IsValid)
            {
                id = b_layer.Create(model, _hostingEnvironment.WebRootPath);
            }
            if (id > 0)
                return RedirectToAction("EditBrand", "Admin", new { id = id });
            return RedirectToAction("Brands", "Admin");
        }
        public IActionResult Brands(int page = 1, string searchText = "")
        {
            var pageSize = 6;
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            if (page < 1)
                page = 1;
            BrandLayer b_layer = new BrandLayer();
            var model = b_layer.GetAll(out int total, page, pageSize, searchText);
            ViewBag.SearchText = searchText;
            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = total;
            ViewBag.PageMax = total / pageSize >= 1 ? total / pageSize : 1;
            return View(model);
        }
        #endregion
        #region Banner
        public IActionResult Banners()
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            var b_layer = new BannerLayer();
            var model = b_layer.GetAll();

            return View(model);
        }

        public IActionResult AddBanner()
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            var model = new BannerModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddBanner(BannerModel model)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            if (model == null)
                return RedirectToAction("AddBanner", "Admin");
            var b_layer = new BannerLayer();
            b_layer.Insert(model, _hostingEnvironment.WebRootPath);
            return RedirectToAction("Banners", "Admin");
        }
        public IActionResult EditBanner(int id)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            var b_layer = new BannerLayer();

            var modelEf = b_layer.GetById(id);
            if (modelEf == null)
                return RedirectToAction("Banners", "Admin");

            return View(modelEf);
        }
        [HttpPost]
        public IActionResult EditBanner(BannerModel model)
        {
            if (User.Identity.Name != "info@bonusmarket.am")
            {
                return NotFound("Not Found");
            }
            if (model == null)
                return RedirectToAction("AddBanner", "Admin");
            var b_layer = new BannerLayer();

            b_layer.Update(model, _hostingEnvironment.WebRootPath);
            return RedirectToAction("Banners", "Admin");
        }
        #endregion
    }
}