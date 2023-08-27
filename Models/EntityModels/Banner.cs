using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.EntityModels
{
    public class Banner:BaseEntity
    {
        public bool Show { get; set; }
        public int OrderId { get; set; }
        public int PictureId { get; set; }
        public PictureEntityViewModel Picture { get; set; }
        public string Link { get; set; }
    }

    public class AdvertismentBanner : Banner
    {
        public bool Show { get; set; }
        public int OrderId { get; set; }
        public int PictureId { get; set; }
        public PictureEntityViewModel Picture { get; set; }
        public string Link { get; set; }
    }
}
