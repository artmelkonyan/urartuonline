using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;

namespace BonusMarket.Components
{
    public class HomePageNewProductsViewComponent : BaseViewComponent
    {
        protected ProductLayer p_layer;
        

        public HomePageNewProductsViewComponent()
        {        
            p_layer = new ProductLayer();
        }

        public IViewComponentResult Invoke()
        {
            var result = p_layer.GetHomePageProducts(RequestLanguage, isNew: true).Where(x => x.Count > 0).ToList();
            return View(result);
        }
    }
}
