using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DashboardViewModel
    {
        public List<ProductEntity> DashboardProducts { get; set; }
        public ProductEntity Product { get; set; }
        public List<CategoryEntityDashboardViewModel> DashboardCategories{get;set;}
        public List<OrderEntity> DashboardOrders{get;set; }
    }
}
