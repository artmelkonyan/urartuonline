using BusinessLayer;
using Models.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace BonusMarket.Components
{
    public class ShoppingCartMiniViewComponent : ViewComponent
    {
        ShoppingCartLayer shoppingCartLayer;

        public ShoppingCartMiniViewComponent()
        {
            shoppingCartLayer = new ShoppingCartLayer();
        }

        public IViewComponentResult Invoke(bool isMobile = false)
        {
    
            var quantity = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = User as ClaimsPrincipal;

                var userId = int.Parse(user.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                quantity = shoppingCartLayer.GetAll(userId, ShoppingCartItemType.ShoppingCartItem).Sum(x => x.Quantity);
            }

            ViewBag.Quantity = quantity;
            ViewBag.IsMobile = isMobile;
            return View();
        }
    }
}
