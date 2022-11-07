using System;
using System.Collections.Generic;
using System.Text;

namespace Models.EntityModels
{
    public class BrandTranslate : BaseEntity
    {
        public int BrandId { get; set; }
        public string SeoName { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
    }
}
