using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;

namespace BonusMarket.Components
{
    public class HomePageNewProductsViewComponent : ViewComponent
    {
        protected ProductLayer p_layer;
        protected string requestLanguage;

        public HomePageNewProductsViewComponent()
        {
            requestLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            p_layer = new ProductLayer();
        }

        public IViewComponentResult Invoke()
        {
            var result = p_layer.GetHomePageProducts(requestLanguage).Where(x => x.Count > 0).ToList();
            return View(result);
        }
    }
}
