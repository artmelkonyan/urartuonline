using DataLayer;
using Models;

namespace BusinessLayer
{
    public class OrderLayer
    {
        public OrderEntity GetOrderById(int id)
        {
            OrderEntity order = null;
            OrderDbProxy db = new OrderDbProxy();
            order = db.GetOrderById(id);
           
            return order;
        }
        public void Delete(int id)
        {
            OrderDbProxy db = new OrderDbProxy();
            db.DeleteOrderById(id);
        }
    }
}
