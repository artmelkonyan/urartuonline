using Microsoft.AspNetCore.Mvc;

namespace BonusMarket.Components
{
    public class SearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() { return View(); }
    }
}
