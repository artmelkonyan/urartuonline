using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BonusMarket.Components
{
    public class WishListViewComponent : ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.Delay(10);
            return View();
        }
    }
}
