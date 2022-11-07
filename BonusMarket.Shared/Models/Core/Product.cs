using System;
using System.Collections.Generic;

namespace BonusMarket.Shared.Models.Core
{
    public class Product
    {
        
        public int Id { get; set; }
        
        public int? Count { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public decimal? OldPrice { get; set; } = null;
        public string Sku { get; set; } = null;
        public bool? ShowOnHomePage { get; set; } = null;
        public bool? Published { get; set; } = null;
        public int? BrandId { get; set; } = null;
        
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Status { get; set; } = true;
        
        public virtual List<ProductPicture> ProductPictures { get; set; } = new List<ProductPicture>();
        public virtual List<ProductTranslation> ProductTranslations { get; set; } = new List<ProductTranslation>();
        public virtual Brand Brand { get; set; }
    }
}