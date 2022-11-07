using System;
using System.Collections.Generic;
using BonusMarket.Shared.Models.Core.Paging;

namespace BonusMarket.Shared.Models.Core.Role
{
    
    [Serializable]
    public class RoleFilter : PagedResultBase
    {
        public string SearchType { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public bool? SystemRole { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreationDateFrom { get; set; } = null;
        public DateTime? CreationDateTo { get; set; } = null;

        public RoleFilter()
        {
            
        }
        public RoleFilter(int page, int size)
        {
            PageSize = size;
            CurrentPage = page;
        }
    }
}