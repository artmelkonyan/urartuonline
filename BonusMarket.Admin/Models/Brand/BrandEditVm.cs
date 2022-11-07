using System.Collections.Generic;

namespace BonusMarket.Admin.Models.Brand
{
    public class BrandEditVm : BrandBaseVm
    {
        public int Id { get; set; }
        public IEnumerable<Shared.Models.Core.Brand> Brands { get; set; } = new List<Shared.Models.Core.Brand>();

        public BrandEditVm()
        {
            
        }

        public BrandEditVm(Shared.Models.Core.Brand item)
        {
            this.Brand = item;
        }
        public Shared.Models.Core.Brand GetDbModel()
        {
            return new Shared.Models.Core.Brand()
            {
                Id = this.Brand.Id,
                BrandTranslations = this.Brand.BrandTranslations,
            };
        }
    }
}