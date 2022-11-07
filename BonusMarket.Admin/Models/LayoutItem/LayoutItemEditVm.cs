using System.Collections.Generic;

namespace BonusMarket.Admin.Models.LayoutItem
{
    public class LayoutItemEditVm : LayoutItemBaseVm
    {
        public int Id { get; set; }
        public IEnumerable<Shared.Models.Core.LayoutItem> LayoutItems { get; set; } = new List<Shared.Models.Core.LayoutItem>();

        public LayoutItemEditVm()
        {
            
        }

        public LayoutItemEditVm(Shared.Models.Core.LayoutItem item)
        {
            this.LayoutItem = item;
        }
        public Shared.Models.Core.LayoutItem GetDbModel()
        {
            return new Shared.Models.Core.LayoutItem()
            {
                ID = this.LayoutItem.ID,
                LayoutItemTranslations = this.LayoutItem.LayoutItemTranslations,
                IsActive = this.LayoutItem.IsActive,
                DomainName = this.LayoutItem.DomainName,
                Twitter = this.LayoutItem.Twitter,
                BookShopUrl = this.LayoutItem.BookShopUrl,
                Instagram = this.LayoutItem.Instagram,
                Facebook = this.LayoutItem.Facebook,
                CategoryImage = this.LayoutItem.CategoryImage,
            };
        }
    }
}