using DataLayer;
using Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ShoppingCartLayer
    {

        public List<ShoppingCartItem> GetAll(int clientId, ShoppingCartItemType shoppingCartItemType)
        {
            var shoppingCartItemdb = new ShoppingCartItemDbProxy();
            var productDb = new ProductDbProxy();

            var result = shoppingCartItemdb.GetAll(clientId, shoppingCartItemType);

            foreach (var item in result)
            {
                item.Product = productDb.GetProductById(item.ProductId);
            }

            return result;
        }

        public ShoppingCartItem Insert(ShoppingCartItem shoppingCartItem)
        {
            var shoppingCartItemdb = new ShoppingCartItemDbProxy();
            return shoppingCartItemdb.Insert(shoppingCartItem);
        }
    }
}
