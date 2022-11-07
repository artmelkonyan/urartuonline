namespace BonusMarket.Admin.Models.Brand
{
    public class BrandAddPostVm : BrandBaseVm
    {
        public BrandAddPostVm()
        {
            
        }
        public BrandAddPostVm(Shared.Models.Core.Brand item)
        {
            this.Brand = item;
        }
        public Shared.Models.Core.Brand GetDbModel()
        {
            return new Shared.Models.Core.Brand()
            {
                BrandTranslations = this.Brand.BrandTranslations,
            };
        }
    }
}