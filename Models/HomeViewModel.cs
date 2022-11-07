using Models.EntityModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            HomeBrands = new List<BrandModel>();
        }
        public List<CategoryEntity> HomePageCategories { get; set; }
        public List<ProductEntity> HomePageProducts { get; set; }
        public List<BrandModel> HomeBrands { get; set; }

    }
}
