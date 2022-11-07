using DataLayer;
using Models;

namespace BusinessLayer
{
    public class IdramLayer
    {
        public IdramPay GetByOrderId(int orderId)
        {
           var idramDbProxy = new IdramDbProxy();
            return idramDbProxy.GetidramPayByOrderId(orderId);
        }

        public IdramPay GetByBuildId(string id)
        {
            var idramDbProxy = new IdramDbProxy();
            return idramDbProxy.GetidramPayByBillId(id);
        }

        public bool Update(IdramPay model)
        {
            var idramDbProxy = new IdramDbProxy();
            return idramDbProxy.UpdateIdramPay(model);
        }
        public int Create(IdramPay model)
        {
            var idramDbProxy = new IdramDbProxy();
            return idramDbProxy.Insert(model)??0;
        }

    }
}
