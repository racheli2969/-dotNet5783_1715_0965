using BL.BO;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
   public interface IOrder
    {
        public IEnumerable< ProductForList> GetOrderList()
        {
            return
        }
        public Order GetOrderDetails(int orderId)
        {
            return 
        }
        public Order UpdateOrderShipping(int orderId)
        {
            return
        }
        public Order UpdateOrderDelivery(int orderId)
        {
            return
        }
        public OrderTracking OrderTracking(int orderId)
        {
            return
        }
    }
}
