using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusMarket.Components
{
    public class LoginRegisterLinkViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(bool isMobile = false)
        {
            ViewBag.IsMobile = isMobile;
            return View();
        }
    }
}
