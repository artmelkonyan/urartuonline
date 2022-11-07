
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.EntityModels.ViewModels
{
    public class BannerModel :Banner
    {
        public IFormFile File { get; set; }
    }
}
