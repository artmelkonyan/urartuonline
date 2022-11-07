using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Models
{
    public class ImageHelper
    {
        private static ImageHelper instance;

        private ImageHelper() { }

        public static ImageHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageHelper();
                }
                return Instance;
            }
        }

        public static string GenImageLink(string FullPath)
        {
            if (String.IsNullOrEmpty(FullPath))
            {
                return "/img/products/noimg.jpg";
            }
            string newPath = Path.Combine("https://urartuonline.am","uploads", "images", FullPath);
            return newPath;
        }
    }
}
