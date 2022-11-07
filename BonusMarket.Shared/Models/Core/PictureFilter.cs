using System;
using System.Collections.Generic;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.Paging;

namespace BonusMarket.Shared.Models.Core
{
    
    [Serializable]
    public class PictureFilter : PagedResultBase
    {
        public bool SkipParent { get; set; } = false;
        public string SearchType { get; set; }
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public int? Type { get; set; }
        public string Title { get; set; }
        public DateTime? CreationDateFrom { get; set; } = null;
        public DateTime? CreationDateTo { get; set; } = null;

        public PictureFilter()
        {
            
        }
        public PictureFilter(int page, int size)
        {
            PageSize = size;
            CurrentPage = page;
        }
    }
}