using System;
using System.Collections.Generic;
using System.Text;

namespace Models.EntityModels.ViewModels
{
    public class BrandModel :Brand
    {
        public BrandModel()
        {
            Translate = new BrandTranslate();
        }
        public BrandTranslate Translate { get; set; }
    }
}
