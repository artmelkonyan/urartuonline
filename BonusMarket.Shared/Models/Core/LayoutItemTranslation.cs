using System;

namespace BonusMarket.Shared.Models.Core
{
    public class LayoutItemTranslation
    {
        public int ID { get; set; }
        public int? LayoutItemID { get; set; }
        public string MainTitle { get; set; }
        public string Address { get; set; }
        public string FooterName { get; set; }
        public string LogoImage { get; set; }
        public string LogoShortImage { get; set; }
        public string Language { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Status { get; set; } = true;
        

        public virtual LayoutItem LayoutItem { get; set; }
    }
}