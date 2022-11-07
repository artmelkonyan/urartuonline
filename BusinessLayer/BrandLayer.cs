using DataLayer;
using Models.EntityModels;
using Models.EntityModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class BrandLayer
    {
        public BrandAdminModel GetByIdForAdmin(int id)
        {
            var brandDb = new BrandDbProxy();
            return brandDb.GetBrandDashboardById(id);
        }
        public List<BrandModel> GetBrandsBySearch(string name, string lang)
        {
            var brandDb = new BrandDbProxy();
            return brandDb.GetBrandsBySearch(name, lang);
        }
        public BrandModel GetById(int id, string lang = "hy")
        {
            var brandDb = new BrandDbProxy();
            return brandDb.GetBrandById(id, lang);
        }
        public List<BrandModel> GetAll(string lang="hy")
        {
            var brandDb = new BrandDbProxy();
            return brandDb.GetBrands(lang);
        }
        public List<BrandModel> GetBrandsByCategoryId(int categoyId,string lang)
        {
            var brandDb = new BrandDbProxy();
            return brandDb.GetBrandsByCategoryId(categoyId, lang);
        }
        public int GetBrandCount()
        {
            var brandDb = new BrandDbProxy();
            return brandDb.GetBrandsCount();
        }
        public List<BrandModel> GetOnHomePage(string lang = "hy")
        {
            var brandDb = new BrandDbProxy();
            return brandDb.GetOnHomePage(lang);
        }
        public List<BrandModel> GetAll(out int total, int page, int pageSize = 10, string searchText = "", string lang = "hy")
        {
            var brandDb = new BrandDbProxy();
            if (string.IsNullOrEmpty(searchText))
            {
                total = brandDb.GetBrandsCount();
                return brandDb.GetAll(page, pageSize, lang);
            }
            total = brandDb.GetBrandCountBySearch(searchText, lang);
            return brandDb.GetAll(page, pageSize, searchText, lang);
        }
        public BrandAdminModel GetDefultModel()
        {
            var model = new BrandAdminModel()
            {
                Status = true,
                OrderId = 0
            };
            List<BrandTranslate> list = new List<BrandTranslate>()
            { new BrandTranslate() { Language = "hy" }, new BrandTranslate() { Language = "en" },new BrandTranslate() { Language = "ru" }};
            model.BrandTranslates = list;
            return model;
        }
        public void Update(BrandAdminModel model, string webRoot)
        {
            var brandDb = new BrandDbProxy();
            if (model.File != null)
            {
                PictureLayer p_l = new PictureLayer();
                var picId = p_l.AddImage(model.File, true, webRoot);
                if (picId.HasValue)
                {
                    p_l.RemoveImage(model.PictureId);
                    model.PictureId = picId.Value;
                }
            }
            brandDb.EditBanner(model);
            foreach (var item in model.BrandTranslates)
            {
                brandDb.EditBrandTranslate(item);
            }
        }
        public int Create(BrandAdminModel model, string webRoot)
        {
            var brandDb = new BrandDbProxy();
            if (model.File != null)
            {
                PictureLayer p_l = new PictureLayer();
                var picId = p_l.AddImage(model.File, true, webRoot);
                if (picId.HasValue)
                {
                    model.PictureId = picId.Value;
                }
            }
            var bId = brandDb.AddBanner(model);
            if (bId.HasValue || model.BrandTranslates != null || model.BrandTranslates.Any())
            {
                foreach (var item in model.BrandTranslates)
                {
                    item.BrandId = bId.Value;
                    item.SeoName = "";
                    brandDb.AddBrandTranslate(item);
                }
            }
            if (bId.HasValue)
            {
                return bId.Value;
            }
            return 0;
        }
    }
}
