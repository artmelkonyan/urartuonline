using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.Permission;

namespace BonusMarket.Admin.Models.Permission
{
    public class PermissionVm
    {
        public string SearchType { get; set; }
        public int? Module { get; set; }
        public string ModuleName { get; set; }
        public int? Permission { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public PagedResult<Shared.Models.Core.Permission.Permission> List { get; set; }
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(SearchType);
        }
        
        public Shared.Models.Core.Permission.Permission ParentPermission { get; set; } 

        public void ClearFilters()
        {
            if (SearchType != "Module")
                Module = null;
            if (SearchType != "ModuleName")
                ModuleName = null;
            if (SearchType != "Permission")
                Permission = null;
            if (SearchType != "PermissionName")
                PermissionName = null;
            if (SearchType != "Description")
                Description = null;
            

            if (SearchType != "CreationDate")
            {
                CreationDateFrom = null;
                CreationDateTo = null;   
            }
        }
        public PermissionVm()
        {
            List = new PagedResult<Shared.Models.Core.Permission.Permission>();
        }
        public PermissionVm(PagedResult<Shared.Models.Core.Permission.Permission> list)
        {
            List = list;
        }

        public PermissionFilter GetFilter()
        {
            return new PermissionFilter()
            {
                SearchType = SearchType,
                Module = Module,
                ModuleName= ModuleName,
                Permission= Permission,
                PermissionName= PermissionName,
                Description= Description,
                CreationDateFrom = CreationDateFrom,
                CreationDateTo = CreationDateTo,
            };
            
        }

        public void FromFilter(PermissionFilter filter)
        {
            SearchType = filter.SearchType;
            Module = filter.Module;
            ModuleName = filter.ModuleName;
            Permission = filter.Permission;
            PermissionName = filter.PermissionName;
            Description = filter.Description;
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