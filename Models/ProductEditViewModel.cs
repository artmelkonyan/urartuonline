using Models.EntityModels;
using Models.EntityModels.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{

    public class ProductEditViewModel : BaseEntityViewModel
    {
        public ProductEditViewModel()
        {
            BrandList = new List<BrandModel>();
        }
        public int Count { get; set; } 
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public string Sku { get; set; } = null;
        public bool ShowOnHomePage { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select category")]
        public int CategoryId { get; set; }
        public List<CategoryEntityDashboardViewModel> ProductsCategoryList { get; set; }
        public List<BrandModel> BrandList { get; set; }
        public int? BrandId { get; set; }
        public PictureEntityViewModel MainImage { get; set; }
        public List<PictureEntityViewModel> PictureList { get; set; } 
        public ProductTranslationEntityViewModel Translation { get; set; }
        public List<ProductTranslationEntityViewModel> TranslationList { get; set; }
        public bool Published { get; set; }
        public ProductToCategory ProductCategory { get; set; }
    }
}
