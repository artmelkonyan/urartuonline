using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.Role;

namespace BonusMarket.Admin.Models.Role
{
    public class RoleVm
    {
        public string SearchType { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public bool? SystemRole { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public PagedResult<Shared.Models.Core.Permission.Role> List { get; set; }
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(SearchType);
        }
        
        public Shared.Models.Core.Permission.Role ParentRole { get; set; } 

        public void ClearFilters()
        {
            if (SearchType != "Name")
                Name = null;
            if (SearchType != "SystemName")
                SystemName = null;
            if (SearchType != "Description")
                Description = null;
            if (SearchType != "SystemRole")
                SystemRole = null;
            if (SearchType != "Active")
                Active = null;
            

            if (SearchType != "CreationDate")
            {
                CreationDateFrom = null;
                CreationDateTo = null;   
            }
        }
        public RoleVm()
        {
            List = new PagedResult<Shared.Models.Core.Permission.Role>();
        }
        public RoleVm(PagedResult<Shared.Models.Core.Permission.Role> list)
        {
            List = list;
        }

        public RoleFilter GetFilter()
        {
            return new RoleFilter()
            {
                SearchType = SearchType,
                Name = Name,
                SystemName = SystemName,
                Description = Description,
                SystemRole = SystemRole,
                Active= Active,
                CreationDateFrom = CreationDateFrom,
                CreationDateTo = CreationDateTo,
            };
            
        }

        public void FromFilter(RoleFilter filter)
        {
            SearchType = filter.SearchType;
            Name = filter.Name;
            SystemName = filter.SystemName;
            Description = filter.Description;
            SystemRole = filter.SystemRole;
            Active = filter.Active;
            CreationDateFrom = filter.CreationDateFrom;
            CreationDateTo = filter.CreationDateTo;
        }
        
        public static List<string> ReadAsList(IFormFile file)
        {
//            var result = new StringBuilder();
            List<string> result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(reader.ReadLine()); 
            }
            return result;
        }
    }
}