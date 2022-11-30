
namespace BlImplementation
{
    public class BlOrder : BlApi.IOrder
    {
        private DalApi.IDal dal = new Dal.DalList();
        public IEnumerable<BO.OrderForList> GetOrderList()
        {
            List<BO.OrderForList> orders = new List<BO.OrderForList>();
            orders.AddRange((IEnumerable<BO.OrderForList>)dal.Order.GetAll());
            return orders;
        }
        public BO.Order GetOrderDetails(int orderId)
        {
            if (orderId < 0)
                throw new BlApi.NegativeIdException();
            else
            {
                List<DO.OrderItem> oi = (List<DO.OrderItem>)dal.OrderItem.GetByOrderId(orderId);
                DO.Order O = dal.Order.GetById(orderId);
                BO.Order order = new BO.Order();
                order.Email = O.Email;
                order.CustomerName = O.CustomerName;
                order.OrderId = O.OrderId;
                order.DateDelivered = O.DateDelivered;
                order.DateOrdered = O.DateOrdered;
                order.DateReceived = O.DateReceived;
                if (O.DateDelivered != null)
                    order.OrderStatus = EnumOrderStatus.Delivered;
                if (O.DateReceived != null)
                    order.OrderStatus = EnumOrderStatus.Received;
                else
                    order.OrderStatus = EnumOrderStatus.Ordered;
                double finalPrice = 0;
                for (int i = 0; i < oi.Count(); i++)
                {
                    finalPrice += oi[i].Price;
                }
                order.SumOfOrder = finalPrice;
                order.Items = oi;
                return order;
            }
        }
        public BO.Order UpdateOrderShipping(int orderId)
        {
            //לתפוס את השגיאה שחוזרת מהדאל
            DO.Order order = dal.Order.GetById(orderId);
            if (order.DateDelivered == null)
            {
                List<DO.OrderItem> oi = (List<DO.OrderItem>)dal.OrderItem.GetByOrderId(orderId);
                order.DateDelivered = DateTime.Now;
                dal.Order.Update(order);
                BO.Order o = new BO.Order();
                o.CustomerName = order.CustomerName;
                o.Email = order.Email;
                o.DateDelivered = order.DateDelivered;
                o.DateOrdered = order.DateOrdered;
                o.OrderStatus = EnumOrderStatus.Delivered;
                double finalPrice = 0;
                for (int i = 0; i < oi.Count(); i++)
                {
                    finalPrice += oi[i].Price;
                }
                o.SumOfOrder = finalPrice;
                return o;
            }
            throw new BlApi.SentAlreadyException();
        }
        public BO.Order UpdateOrderDelivery(int orderId)
        {
            return
        }
        public BO.Order UpdateOrderDetails(int orderId, int productId, int amount)
        {
            return
        }
    }
}
