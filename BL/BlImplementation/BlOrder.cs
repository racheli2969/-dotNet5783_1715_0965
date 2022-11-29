using BlApi;
using BO;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlImplementation
{
    public class BlOrder : IOrder
    {
        private DalApi.IDal dal = new Dal.DalList();
        public IEnumerable<OrderForList> GetOrderList()
        {
            List<OrderForList> orders = new List<OrderForList>();
            orders.AddRange((IEnumerable<BO.OrderForList>)dal.Order.GetAll());
            return orders;
        }
        public Order GetOrderDetails(int orderId)
        {
            if (orderId < 0)
                throw new NegativeIdException();

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
        public Order UpdateOrderDetails(int orderId, int productId, int amount)
        {
            return
        }
    }
}
