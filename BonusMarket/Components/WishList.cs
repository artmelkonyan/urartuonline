using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models.EntityModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BonusMarket.Components
{
    public class WishListViewComponent : ViewComponent
    {
        ShoppingCartLayer shoppingCartLayer;

        public WishListViewComponent()
        {
            shoppingCartLayer = new ShoppingCartLayer();
        }

        public IViewComponentResult Invoke()
        {
         
            var quantity = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = User as ClaimsPrincipal;

                var userId = int.Parse(user.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                quantity = shoppingCartLayer.GetAll(userId, ShoppingCartItemType.WishListItem).Sum(x => x.Quantity);
            }

            ViewBag.Quantity = quantity;
            return View();
        }
    }
}
