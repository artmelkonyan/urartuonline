using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusMarket.Components
{
    public class ShoppingCartMiniViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(bool isMobile = false)
        {
            await Task.Delay(10);
            ViewBag.IsMobile = isMobile;
            return View();
        }
    }
}
