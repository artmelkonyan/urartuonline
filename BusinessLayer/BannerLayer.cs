using DataLayer;
using Models.EntityModels;
using Models.EntityModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class BannerLayer
    {
        public BannerModel GetById(int id)
        {
            var bannerdb = new BannerDbProxy();
            return bannerdb.GetById(id);
        }
        public List<Banner> GetAll()
        {
            var bannerdb = new BannerDbProxy();
            return bannerdb.GetAllBanners();
        }
        public bool Update(BannerModel model, string webroot = "")
        {
            var picL = new PictureLayer();
            var bannerdb = new BannerDbProxy();
            if (model.File != null)
            {
                var picId = picL.AddImage(model.File, true, webroot);
                if (picId.HasValue)
                {
                    picL.RemoveImage(model.PictureId);
                    model.PictureId = picId.Value;
                }
            }
            return bannerdb.UpdateBanner(model);
        }
        public int? Insert(BannerModel model, string webroot = "")
        {
            var picL = new PictureLayer();
            var bannerdb = new BannerDbProxy();
            if (model.File != null)
            {
                var picId = picL.AddImage(model.File, true, webroot);
                if (picId.HasValue)
                    model.PictureId = picId.Value;
            }
            return bannerdb.Insert(model);
        }
    }
}
