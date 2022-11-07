using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
            Products = new List<ProductEntity>();
        }
        public List<ProductEntity> Products { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ViewCount { get; set; }
        public int CategoryId { get; set; }
        public int[] BrandIds { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string SearchWord { get; set; }
        public string SearchWordFixed { get; set; }
        public string Order { get; set; }
    }
}
