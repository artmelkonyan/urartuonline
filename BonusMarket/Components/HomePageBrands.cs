using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BonusMarket.Components
{
    public class HomePageBrandsViewComponent : BaseViewComponent
    {
        BrandLayer brandLayer;


        public HomePageBrandsViewComponent()
        {
            brandLayer = new BrandLayer();
        }

        public IViewComponentResult Invoke()
        {
            var brands = brandLayer.GetOnHomePage(RequestLanguage).ToList();
            return View(brands);
        }
    }
}
