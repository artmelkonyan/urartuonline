using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class BaseEntity
    {
        public int? Id { get; set; } = null;
        public DateTime? CreationDate { get; set; } = null;
        public DateTime? ModificationDate { get; set; } = null;
        public bool? Status { get; set; } = null;
    }
    [Serializable]
    public class BaseEntityViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool Status { get; set; }
    }
    
}
