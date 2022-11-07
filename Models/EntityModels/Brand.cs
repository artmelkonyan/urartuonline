using System;
using System.Collections.Generic;
using System.Text;

namespace Models.EntityModels
{
    public class Brand : BaseEntity
    {
        
        public bool Show { get; set; }
        public int OrderId { get; set; }
        public int PictureId { get; set; }
        public PictureEntityViewModel Picture { get; set; }
        public bool Status { get; set; }
    }
}
