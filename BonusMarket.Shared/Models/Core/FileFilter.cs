using System;
using System.Collections.Generic;
using BonusMarket.Shared.Models.Core.Paging;

namespace BonusMarket.Shared.Models.Core
{
    
    [Serializable]
    public class FileFilter : PagedResultBase
    {
        public string SearchType { get; set; }
        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreationDateFrom { get; set; } = null;
        public DateTime? CreationDateTo { get; set; } = null;

        public FileFilter()
        {
            
        }
        public FileFilter(int page, int size)
        {
            PageSize = size;
            CurrentPage = page;
        }
    }
}