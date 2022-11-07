using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ProductPictureEntity
    {
        public string PicId;
        public string PicName;
    }

    public class ProductCategoryMappingEntity
    {
        public int? ProductOldid { get; set; } = null;
        public int? Productid { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public int? CategoryOldId { get; set; } = null;
        public string Sku { get; set; } = null;
    }
}