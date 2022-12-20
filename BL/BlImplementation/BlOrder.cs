namespace BlImplementation;
public class BlOrder : BlApi.IOrder
{
    private DalApi.IDal? dal = DalApi.Factory.Get();
    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        List<DO.Order>? ordersFromDal = new List<DO.Order>();
        BO.OrderForList temp;
        //gets all orders from dal
        ordersFromDal = dal?.Order?.GetAll().ToList();
        //get for each order orderItems
        for (int i = 0; i < ordersFromDal?.Count; i++)
        {
            List<DO.OrderItem>? oi = new List<DO.OrderItem>();
            oi = dal?.OrderItem?.GetAll(p => p.OrderItemId == ordersFromDal[i].OrderId).ToList();
            double finalPrice = 0;
            for (int j = 0; j < oi?.Count; j++)
            {
                finalPrice = finalPrice + (oi[j].Price * oi[j].Amount);
            }
            temp = new BO.OrderForList();
            temp.Id = ordersFromDal[i].OrderId;
            temp.CustomerName = ordersFromDal[i].CustomerName;
            temp.OrderStatus = ordersFromDal[i].DateReceived != DateTime.MinValue ? BO.EnumOrderStatus.Received : ordersFromDal[i].DateDelivered != DateTime.MinValue ? BO.EnumOrderStatus.Delivered : BO.EnumOrderStatus.Ordered;
            temp.Price = finalPrice;
            orders.Add(temp);
        }
        return orders;
    }
    public BO.Order GetOrderDetails(int orderId)
    {
        if (orderId < 0)
            throw new BlApi.NegativeIdException();
        try
        {
            //gets the order items from dal
            List<DO.OrderItem>? oi = dal?.OrderItem?.GetAll(p => p.OrderItemId == orderId)?.ToList();
            List<DO.Order>? O = dal?.Order?.GetAll(o => o.OrderId == orderId)?.ToList();
            BO.Order order = new BO.Order();
            //move info from dal order to bo order
            order.Email = O[0].Email;
            order.CustomerName = O[0].CustomerName;
            order.OrderId = O[0].OrderId;
            order.DateDelivered = O[0].DateDelivered;
            order.DateOrdered = O[0].DateOrdered;
            order.DateReceived = O[0].DateReceived;
            if (O[0].DateDelivered != DateTime.MinValue)
                order.OrderStatus = BO.EnumOrderStatus.Delivered;
            if (O[0].DateReceived != DateTime.MinValue)
                order.OrderStatus = BO.EnumOrderStatus.Received;
            else
                order.OrderStatus = BO.EnumOrderStatus.Ordered;
            double finalPrice = 0;
            for (int i = 0; i < oi.Count; i++)
            {
                finalPrice += (oi[i].Price * oi[i].Amount);
            }
            order.SumOfOrder = finalPrice;
            BO.OrderItem orderI;
            List<BO.OrderItem> listOfOrderItems = new List<BO.OrderItem>();
            //put the order items in the order
            for (int i = 0; i < oi.Count; i++)
            {
                orderI = new BO.OrderItem();
                List<DO.Item>? item = new List<DO.Item>();
                orderI.ItemId = oi[i].ItemId;
                orderI.OrderItemId = oi[i].OrderItemId;
                orderI.Amount = oi[i].Amount;
                //gets the item from dal
                item = dal?.Item?.GetAll(i => i.ID == orderI.ItemId)?.ToList();
                orderI.ItemName = item[0].Name;
                orderI.ItemPrice = item[0].Price;
                orderI.PriceOfItems = (orderI.ItemPrice * orderI.Amount);
                listOfOrderItems.Add(orderI);
            }
            order.Items = listOfOrderItems;
            return order;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }

    }
    public BO.Order UpdateOrderShipping(int orderId)
    {
        try
        {
            //get the order
            List<DO.Order>? order = dal?.Order.GetAll(o => o.OrderId == orderId)?.ToList();
            if (order[0].DateDelivered == DateTime.MinValue)
            {
                List<DO.OrderItem>? oi = dal?.OrderItem.GetAll(p => p.OrderItemId == orderId)?.ToList();
                DO.Order order_ = new();
                order_ = order[0];
                order_.DateDelivered = DateTime.Now;
                dal.Order.Update(order[0]);
                BO.Order o = new BO.Order();
                o.OrderId = orderId;
                o.CustomerName = order_.CustomerName;
                o.Email = order_.Email;
                o.DateDelivered = order_.DateDelivered;
                o.DateOrdered = order_.DateOrdered;
                o.OrderStatus = BO.EnumOrderStatus.Delivered;
                double finalPrice = 0;
                for (int i = 0; i < oi.Count; i++)
                {
                    finalPrice = finalPrice + (oi[i].Price * oi[i].Amount);
                }
                o.SumOfOrder = finalPrice;
                return o;
            }
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
        throw new BlApi.SentAlreadyException();
    }
    public BO.Order UpdateOrderDelivery(int orderId)
    {
        try
        {
            //get the order
            List<DO.Order>? order = dal?.Order?.GetAll(o => o.OrderId == orderId)?.ToList();
            if (order[0].DateReceived == DateTime.MinValue && order[0].DateDelivered != DateTime.MinValue)
            {
                List<DO.OrderItem>? oi = dal?.OrderItem?.GetAll(p => p.OrderItemId == orderId)?.ToList();
                DO.Order order_ = new();
                order_ = order[0];
                order_.DateReceived = DateTime.Now;
                dal?.Order.Update(order[0]);
                BO.Order o = new BO.Order();
                o.CustomerName = order_.CustomerName;
                o.Email = order_.Email;
                o.DateDelivered = order_.DateDelivered;
                o.DateOrdered = order_.DateOrdered;
                o.OrderStatus = BO.EnumOrderStatus.Received;
                o.OrderId = orderId;
                double finalPrice = 0;
                for (int i = 0; i < oi.Count; i++)
                {
                    finalPrice = finalPrice + (oi[i].Price * oi[i].Amount);
                }
                o.SumOfOrder = finalPrice;
                return o;
            }
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
        throw new BlApi.deliveredAlreadyException();
    }
    public BO.OrderTracking OrderTracking(int orderId)
    {
        try
        {

            //get the order
            List<DO.Order>? order = dal?.Order.GetAll(o => o.OrderId == orderId)?.ToList();
            BO.OrderTracking ot = new BO.OrderTracking();
            ot.Id = orderId;
            (DateTime, BO.EnumOrderStatus) myTuple = ((DateTime)(order)[0].DateOrdered, BO.EnumOrderStatus.Delivered);
            ot?.TrackingTuples?.Add(myTuple);
            if (order[0].DateDelivered != DateTime.MinValue)
            {
                ot.OrderStatus = BO.EnumOrderStatus.Delivered;
                ot.TrackingTuples[1] = ((DateTime)(order[0]).DateDelivered, BO.EnumOrderStatus.Delivered);
            }
            if (order[0].DateReceived != DateTime.MinValue)
            {
                ot.OrderStatus = BO.EnumOrderStatus.Received;
                ot.TrackingTuples[2] = ((DateTime)(order[0]).DateReceived, BO.EnumOrderStatus.Delivered);
            }
            else
                ot.OrderStatus = BO.EnumOrderStatus.Ordered;

            return ot;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }

    //for bonus
    public BO.Order UpdateOrderDetails(int orderId, int productId, int amount)
    {
        BO.Order order = new BO.Order();
        //do something
        return order;
    }
}
