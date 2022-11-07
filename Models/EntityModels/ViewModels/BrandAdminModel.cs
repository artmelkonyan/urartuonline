using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.EntityModels.ViewModels
{
    public class BrandAdminModel:Brand
    {
        public BrandAdminModel()
        {
            BrandTranslates = new List<BrandTranslate>();
        }
        public List<BrandTranslate> BrandTranslates { get; set; }
        public IFormFile File { get; set; }
    }
}
