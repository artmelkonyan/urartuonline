using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class PictureEntity : BaseEntity
    {
        public string RealPath { get; set; } = null;
        public string RealName { get; set; } = null;
        public string SeoName { get; set; } = null;
        public bool? Main { get; set; } = null;
        public string FullPath { get; set; } = null;
    }
    public class PictureEntityViewModel : BaseEntityViewModel
    {
        public string RealPath { get; set; }
        public string RealName { get; set; }
        public string SeoName { get; set; } 
        public bool Main { get; set; }
        public string FullPath { get; set; }
    }
}
