using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusMarket.Components
{
    public class LanguageSelectorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.Delay(10);
            return View();
        }
    }
}
