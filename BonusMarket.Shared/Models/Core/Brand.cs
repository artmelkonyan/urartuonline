using System;
using System.Collections.Generic;

namespace BonusMarket.Shared.Models.Core
{
    public class Brand
    {
        public int Id { get; set; }

        public int PictureId { get; set; }
        public bool Status { get; set; } = true;
        
        public virtual Picture Picture { get; set; }
        public virtual List<BrandTranslation> BrandTranslations { get; set; } = new List<BrandTranslation>();
    }
}