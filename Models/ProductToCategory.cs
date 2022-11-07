using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class ProductToCategory
    {
        public int? Id { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public int? ProductId { get; set; } = null;
        public DateTime? CreationDate { get; set; } = null;
        public bool? Status { get; set; } = null;
    }
}
