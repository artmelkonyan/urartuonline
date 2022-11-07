using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Models.Core.User;

namespace BonusMarket.Admin.Models.User
{
    public class UserVm
    {
        public string SearchType { get; set; }
        public int Id { get; set; }
        
        public List<Shared.Models.Core.User.User> Users { get; set; }
        public string FullName { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public PagedResult<Shared.Models.Core.User.User> List { get; set; }
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(SearchType);
        }
        
        public Shared.Models.Core.User.User ParentUser { get; set; } 

        public void ClearFilters()
        {
            if (SearchType != "FullName")
                FullName = null;

            if (SearchType != "CreationDate")
            {
                CreationDateFrom = null;
                CreationDateTo = null;   
            }
        }
        public UserVm()
        {
            List = new PagedResult<Shared.Models.Core.User.User>();
        }
        public UserVm(PagedResult<Shared.Models.Core.User.User> list)
        {
            List = list;
        }

        public UserFilter GetFilter()
        {
            return new UserFilter()
            {
                SearchType = SearchType,
                FullName = FullName,
            };
            
        }

        public void FromFilter(UserFilter filter)
        {
            SearchType = filter.SearchType;
            FullName = filter.FullName;
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