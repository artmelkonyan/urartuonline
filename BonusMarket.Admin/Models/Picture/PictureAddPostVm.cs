using System.Collections.Generic;

namespace BonusMarket.Admin.Models.Picture
{
    public class PictureAddPostVm : PictureBaseVm
    {
        public IEnumerable<int> NewBooks { get; set; } = new List<int>();
        public PictureAddPostVm()
        {
            
        }
        public PictureAddPostVm(Shared.Models.Core.Picture item)
        {
            this.Picture = item;
        }
        public Shared.Models.Core.Picture GetDbModel()
        {
            return new Shared.Models.Core.Picture()
            {
                Main = this.Picture.Main,
                SeoName = this.Picture.SeoName,
            };
        }
    }
}