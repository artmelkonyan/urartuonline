using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BonusMarket.Components
{
    public class HomePageSaleProductsViewComponent : BaseViewComponent
    {
        protected ProductLayer p_layer;

        public HomePageSaleProductsViewComponent()
        {
            p_layer = new ProductLayer();
        }

        public IViewComponentResult Invoke()
        {
            var sales = p_layer.GetHomePageProducts(RequestLanguage).Where(x => x.Count > 0 && x.OldPrice.GetValueOrDefault() > 0).ToList();
            return View(sales);
        }
    }
}
