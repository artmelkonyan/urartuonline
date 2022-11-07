using System.Collections.Generic;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Paging;

namespace BonusMarket.Models
{
    public class SearchV2VM
    {
        public PagedResult<Product> Products = new PagedResult<Product>();
        public string HyName { get; set; }
        public string EngName { get; set; }
        public string RuName { get; set; }
        public string HyBrand { get; set; }
        public string EngBrand { get; set; }
        public string RuBrand { get; set; }
        public string OrderBy { get; set; }

        public PagedResult<Brand> brands { get; set; }
        public SearchV2VM()
        {
            
        }

        public SearchV2VM(PagedResult<Product> products, string hyName, string engName, string ruName, string hyBrand, string engBrand, string ruBrand)
        {
            HyName = hyName;
            RuName = ruName;
            EngName = engName;
            HyBrand = hyBrand;
            RuBrand = ruBrand;
            EngBrand = engBrand;
            Products = products;
        }
    }
}