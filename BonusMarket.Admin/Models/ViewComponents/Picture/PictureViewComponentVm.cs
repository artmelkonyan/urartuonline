using System.Collections.Generic;

namespace BonusMarket.Admin.Models.ViewComponents.Picture
{
    public class PictureViewComponentVm
    {
        public string Name { get; set; }
        public List<Shared.Models.Core.Picture> Pictures { get; set; } = new List<Shared.Models.Core.Picture>();
        public bool Single { get; set; } = true;
    }
}