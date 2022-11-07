using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CategoryLeftMenuViewModel
    {
        public CategoryLeftMenuViewModel()
        {
            ParentIds = new List<int>();
        }
        public int[] BrandsId { get; set; }
        public CategoryEntity Category { get; set; }
        public bool IsSeleced { get; set; }
        public List<int> ParentIds { get; set; }
    }
}