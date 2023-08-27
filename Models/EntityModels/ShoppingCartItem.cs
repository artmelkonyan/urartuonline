using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Models.EntityModels
{
    public enum ShoppingCartItemType
    {
        ShoppingCartItem = 1,
        WishListItem = 2
    }

    public class ShoppingCartItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartItemTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ProductEntity Product { get; set; }        
    }
}
