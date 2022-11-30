
using BL;
using System.Diagnostics.CodeAnalysis;

namespace BlImplementation;

public class BlOrder : BlApi.IOrder
{
    private DalApi.IDal dal = new Dal.DalList();
    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        List<DO.Order> O = (List<DO.Order>)dal.Order.GetAll();
        for (int i = 0; i < O.Count(); i++)
        {
            double sum = 0;
            List<DO.OrderItem> oi = (List<DO.OrderItem>)dal.OrderItem.GetByOrderId(O[i].OrderId);
            orders[i].CustomerName = O[i].CustomerName;
            orders[i].Id = O[i].OrderId;
            orders[i].NumOfItems = oi.Count();
            for(int j=0;j<oi.Count(); j++)
            {
                sum += oi[j].Price;
            }
            orders[i].Price = sum;
            if (O[i].DateReceived != null)
                orders[i].OrderStatus = EnumOrderStatus.Received;
            if (O[i].DateDelivered != null)
                orders[i].OrderStatus = EnumOrderStatus.Delivered;
            else
                orders[i].OrderStatus = EnumOrderStatus.Ordered;
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
                if (O.DateReceived != null)
                    order.OrderStatus = EnumOrderStatus.Received;
                if (O.DateDelivered != null)
                    order.OrderStatus = EnumOrderStatus.Delivered;
                else
                    order.OrderStatus = EnumOrderStatus.Ordered;
                double finalPrice = 0;
                for (int i = 0; i < oi.Count(); i++)
                {
                    finalPrice += oi[i].Price;
                }
                order.SumOfOrder = finalPrice;

                BO.OrderItem orderI = new BO.OrderItem();
                DO.Item item = new DO.Item();
                for (int i = 0; i < oi.Count(); i++)
                {
                    orderI.ItemId = oi[i].ItemId;
                    orderI.OrderItemId = oi[i].OrderItemId;
                    orderI.Amount = oi[i].Amount;
                    item = dal.Item.GetById(oi[i].ItemId);
                    orderI.ItemName = item.Name;
                    orderI.ItemPrice = item.Price;
                    orderI.PriceOfItems = (orderI.ItemPrice * orderI.Amount);
                    order.Items.Insert(i, orderI);
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
            if (order.DateReceived == null)
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
            ot.TrackingTuples[0] = (order.DateOrdered, EnumOrderStatus.Delivered);
            if (order.DateDelivered != null)
            {
                ot.OrderStatus = EnumOrderStatus.Delivered;
                ot.TrackingTuples[1] = (order.DateDelivered, EnumOrderStatus.Delivered);
            }
            if (order.DateReceived != null)
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
