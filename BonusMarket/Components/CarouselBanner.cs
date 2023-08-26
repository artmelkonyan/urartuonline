using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BonusMarket.Components
{
    public class CarouselBannerViewComponent : ViewComponent
    {
        PictureLayer p_layer;
        BannerLayer b_layer;
        public CarouselBannerViewComponent()
        {
            p_layer = new PictureLayer();
            b_layer = new BannerLayer();
        }

        public IViewComponentResult Invoke()
        {
            var banners = b_layer.GetAll().Where(x => x.Show).OrderBy(x => x.OrderId).ToList();
            foreach (var item in banners)
            {
                item.Picture = p_layer.GetPictureById(item.PictureId);
            }

            return View(banners);
        }
    }
}
