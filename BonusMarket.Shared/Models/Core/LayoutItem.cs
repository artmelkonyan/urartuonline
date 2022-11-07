using System;
using System.Collections.Generic;

namespace BonusMarket.Shared.Models.Core
{
    public class LayoutItem 
    {
        public int ID { get; set; }
        public string DomainName { get; set; }
        public string Twitter { get; set; }
        public string BookShopUrl { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string CategoryImage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Status { get; set; } = true;

        public virtual List<LayoutItemTranslation> LayoutItemTranslations { get; set; } = new List<LayoutItemTranslation>();
    }
}