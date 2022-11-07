using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CategoryViewModel
    {
        public List<CategoryEntity> CategoriesByParent { get; set; }
        public List<CategoryEntity> CategoryTree { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
