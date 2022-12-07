
using BL;
using System.Diagnostics.CodeAnalysis;

namespace BlImplementation;

public class BlOrder : BlApi.IOrder
{
    private DalApi.IDal dal = new Dal.DalList();
    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        List<DO.Order> ordersFromDal = new List<DO.Order>();
        BO.OrderForList temp;
        ordersFromDal = (List<DO.Order>)dal.Order.GetAll();

        for (int i = 0; i < ordersFromDal.Count; i++)
        {
            temp = new BO.OrderForList();
            temp.Id=ordersFromDal[i].OrderId;
            temp.CustomerName= ordersFromDal[i].CustomerName;
            temp.OrderStatus = ordersFromDal[i].DateReceived != DateTime.MinValue ? BL.EnumOrderStatus.Received : ordersFromDal[i].DateDelivered != DateTime.MinValue ? BL.EnumOrderStatus.Delivered : BL.EnumOrderStatus.Ordered;
            orders.Add(temp);
        }
        return orders;
    }
    public BO.Order GetOrderDetails(int orderId)
    {
        if (orderId < 0)
            throw new BlApi.NegativeIdException();
        else
        {
            try
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
                if (O.DateDelivered != DateTime.MinValue)
                    order.OrderStatus = EnumOrderStatus.Delivered;
                if (O.DateReceived != DateTime.MinValue)
                    order.OrderStatus = EnumOrderStatus.Received;
                else
                    order.OrderStatus = EnumOrderStatus.Ordered;
                double finalPrice = 0;
                for (int i = 0; i < oi.Count; i++)
                {
                    finalPrice += oi[i].Price;
                }
                order.SumOfOrder = finalPrice;
                for (int i = 0; i < oi.Count; i++)
                {
                    BO.OrderItem orderI = new BO.OrderItem();
                    DO.Item item = new DO.Item();
                    orderI.ItemId = oi[i].ItemId;
                    orderI.OrderItemId = oi[i].OrderItemId;
                    orderI.Amount = oi[i].Amount;
                    item = dal.Item.GetById(oi[i].ItemId);
                    orderI.ItemName= item.Name;
                    orderI.ItemPrice = item.Price;
                    orderI.PriceOfItems = (orderI.ItemPrice * orderI.Amount);
                    order.Items.Add(orderI);
                  // order.Items.Insert(i, orderI);
                }
                return order;
            }
            catch (DalApi.EntityNotFoundException)
            {
                throw new BlApi.BlEntityNotFoundException();
            }
        }
    }
    public BO.Order UpdateOrderShipping(int orderId)
    {
        try
        {
            DO.Order order = dal.Order.GetById(orderId);
            if (order.DateDelivered == DateTime.MinValue)
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
            DO.Order order = dal.Order.GetById(orderId);
            if (order.DateReceived == DateTime.MinValue)
            {
                List<DO.OrderItem> oi = (List<DO.OrderItem>)dal.OrderItem.GetByOrderId(orderId);
                order.DateReceived = DateTime.Now;
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
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
        throw new BlApi.SentAlreadyException();
    }
    public BO.OrderTracking OrderTracking(int orderId)
    {
        try
        {
            DO.Order order = dal.Order.GetById(orderId);
            BO.OrderTracking ot = new BO.OrderTracking();
            ot.Id = orderId;
            (DateTime, EnumOrderStatus) myTuple = (order.DateOrdered, EnumOrderStatus.Delivered);
            ot.TrackingTuples.Add(myTuple);
            if (order.DateDelivered != DateTime.MinValue)
            {
                ot.OrderStatus = EnumOrderStatus.Delivered;
                ot.TrackingTuples[1] = (order.DateDelivered, EnumOrderStatus.Delivered);
            }
            if (order.DateReceived != DateTime.MinValue)
            {
                ot.OrderStatus = EnumOrderStatus.Received;
                ot.TrackingTuples[2] = (order.DateReceived, EnumOrderStatus.Delivered);
            }
            else
                ot.OrderStatus = EnumOrderStatus.Ordered;

            return ot;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }

    public BO.Order UpdateOrderDetails(int orderId, int productId, int amount)
    {
        BO.Order order = new BO.Order();
        //do something
        return order;
    }
}
