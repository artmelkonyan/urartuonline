using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
