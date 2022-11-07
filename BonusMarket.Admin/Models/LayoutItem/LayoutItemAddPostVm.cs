namespace BonusMarket.Admin.Models.LayoutItem
{
    public class LayoutItemAddPostVm : LayoutItemBaseVm
    {
        public LayoutItemAddPostVm()
        {
            
        }
        public LayoutItemAddPostVm(Shared.Models.Core.LayoutItem item)
        {
            this.LayoutItem = item;
        }
        public Shared.Models.Core.LayoutItem GetDbModel()
        {
            return new Shared.Models.Core.LayoutItem()
            {
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