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
        public IEnumerable<ProductForList> GetOrderList();

        public Order GetOrderDetails(int orderId);

        public Order UpdateOrderShipping(int orderId);

        public Order UpdateOrderDelivery(int orderId);

        public OrderTracking OrderTracking(int orderId);
       
    }
}
