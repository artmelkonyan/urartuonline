using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class ProductTranslationEntity : TranslationEntity
    {
        public int? ProductId { get; set; } = null;
        public string NameTranslation { get; set; } = null;
        public string ShortDescriptionTranslation { get; set; } = null;
        public string FullDescriptionTranslation { get; set; } = null;
        public string SeoName { get; set; } = null;
    }
    public class ProductTranslationEntityViewModel : TranslationEntityViewModel
    {
        public int ProductId { get; set; }
        public string NameTranslation { get; set; }
        public string ShortDescriptionTranslation { get; set; }
        public string FullDescriptionTranslation { get; set; }
        public string SeoName { get; set; }
    }
    [Serializable]
    public class ProductEntity : BaseEntity
    {
        public int? Count { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public decimal? OldPrice { get; set; } = null;
        public string Sku { get; set; } = null;
        public bool? ShowOnHomePage { get; set; } = null;
        public PictureEntity MainImage { get; set; } = null;
        public List<PictureEntity> PictureList { get; set; } = null;
        public ProductTranslationEntity Translation { get; set; } = null;
        public List<ProductTranslationEntity> TranslationList { get; set; } = null;
        public bool? Published { get; set; } = null;
        public int? BrandId { get; set; } = null;
        public ProductToCategory ProductCategory { get; set; } = null;
        public bool? IsNew { get; set; } = null;
    }
}
