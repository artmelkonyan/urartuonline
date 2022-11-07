using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Admin.Models.LayoutItem
{
    public class LayoutItemPagingVm
    {
        [FromQuery]
        public int PageSize { get; set; } = 10;
        [FromQuery]
        public int Page { get; set; } = 1;
        
        [FromQuery]
        public int? ParentId { get; set; }
    }
}