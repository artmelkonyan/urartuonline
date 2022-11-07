using System.Collections.Generic;

namespace BonusMarket.Admin.Models.Picture
{
    public class PictureEditVm : PictureBaseVm
    {
        public IEnumerable<int> NewBooks { get; set; } = new List<int>();
        public int Id { get; set; }

        public PictureEditVm()
        {
            
        }

        public PictureEditVm(Shared.Models.Core.Picture item)
        {
            this.Picture = item;
        }
        public Shared.Models.Core.Picture GetDbModel()
        {
            return new Shared.Models.Core.Picture()
            {
                Id = this.Picture.Id,
                Main = this.Picture.Main,
                SeoName = this.Picture.SeoName,
            };
        }
    }
}