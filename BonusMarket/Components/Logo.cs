using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Components
{
    public class LogoViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
