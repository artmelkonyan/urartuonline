using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Components
{
    public class PhoneNumbersViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke() { return View(); }
    }
}
