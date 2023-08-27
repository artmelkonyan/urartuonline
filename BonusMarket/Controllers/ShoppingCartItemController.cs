using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.EntityModels;
using System;
using System.Linq;

namespace BonusMarket.Controllers
{
    //[Authorize]
    public class ShoppingCartItemController : BaseController
    {

        ShoppingCartLayer shoppingCartLayer;

        public ShoppingCartItemController()
        {
            shoppingCartLayer = new ShoppingCartLayer();
        }

        [HttpPost]
        public IActionResult AddProductToCart_Catalog(int productId, int shoppingCartTypeId,
            int quantity)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    success = false,
                    redirectUrl = Url.RouteUrl("Login")
                });
            }
            var userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var res = shoppingCartLayer.Insert(new ShoppingCartItem
            {
                ProductId = productId,
                ClientId = userId,
                ShoppingCartItemTypeId = shoppingCartTypeId,
                CreatedOn = DateTime.Now,
                Quantity = quantity
            });

            var count = shoppingCartLayer.GetAll(userId, (ShoppingCartItemType)shoppingCartTypeId).Sum(x=>x.Quantity);

            if ((ShoppingCartItemType)shoppingCartTypeId==ShoppingCartItemType.ShoppingCartItem)
            {
                return Json(new
                {
                    success = true,
                    updatetopcartsectionhtml = count
                });
            }
            else
            {
                return Json(new
                {
                    success = true,
                    updatetopwishlistsectionhtml = count
                });
            }            
        }

        public IActionResult GetShoppingCartCount(ShoppingCartItemType type)
        {
            var shci = shoppingCartLayer.GetAll(0, type);
            return Ok(shci.Count);
        }
    }
}
