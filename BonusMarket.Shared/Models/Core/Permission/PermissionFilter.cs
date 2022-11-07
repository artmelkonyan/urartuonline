using System;
using System.Collections.Generic;
using BonusMarket.Shared.Models.Core.Paging;

namespace BonusMarket.Shared.Models.Core.Permission
{
    
    [Serializable]
    public class PermissionFilter : PagedResultBase
    {
        public string SearchType { get; set; }
        public int? Id { get; set; }
        public int? Module { get; set; }
        public string ModuleName { get; set; }
        public int? Permission { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDateFrom { get; set; } = null;
        public DateTime? CreationDateTo { get; set; } = null;

        public PermissionFilter()
        {
            
        }
        public PermissionFilter(int page, int size)
        {
            PageSize = size;
            CurrentPage = page;
        }
    }
}