using Models.EntityModels;
using Models.EntityModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Categories = new List<CategoryEntity>();
            CategoryList = new List<CategoryEntity>();
            ParentIds = new List<int>();
            Brands = new List<BrandModel>();
            Banners = new List<Banner>();
        }
        public List<Banner> Banners { get; set; }
        public List<CategoryEntity> Categories { get; set; }
        public List<CategoryEntity> CategoryList { get; set; }
        public List<BrandModel> Brands { get; set; }

        public List<int> ParentIds { get; set; }
    }
}
