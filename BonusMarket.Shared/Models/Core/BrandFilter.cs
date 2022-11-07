using System;
using System.Collections.Generic;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.Paging;

namespace BonusMarket.Shared.Models.Core
{
    
    [Serializable]
    public class BrandFilter : PagedResultBase
    {
        public bool SkipParent { get; set; } = false;
        public string SearchType { get; set; }
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public int? Type { get; set; }
        public string Title { get; set; }
        public int? BrandId { get; set; }
        public string HyName { get; set; }
        public string EngName { get; set; }
        public string RuName { get; set; }
        public string HyBrand { get; set; }
        public string EngBrand { get; set; }
        public string RuBrand { get; set; }
        public string OrderBy { get; set; }
        public string LanguageCode { get; set; }

        public DateTime? CreationDateFrom { get; set; } = null;
        public DateTime? CreationDateTo { get; set; } = null;

        public BrandFilter()
        {
            
        }
        public BrandFilter(int page, int size)
        {
            PageSize = size;
            CurrentPage = page;
        }
    }
}