﻿using BO;
using DO;

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
        ordersFromDal = dal?.Order?.GetAll()?.ToList();
        //get for each order orderItems
        for (int i = 0; i < ordersFromDal?.Count; i++)
        {
            List<DO.OrderItem>? oi = new List<DO.OrderItem>();
            oi = dal?.OrderItem?.GetAll(p => p.OrderID == ordersFromDal[i].OrderId)?.ToList();
            double finalPrice = 0;
            int amountOfProducts = 0;
            for (int j = 0; j < oi?.Count; j++)
            {
                finalPrice = finalPrice + (oi[j].Price * oi[j].Amount);
                amountOfProducts += oi[j].Amount;
            }
            temp = new BO.OrderForList();
            temp.Id = ordersFromDal[i].OrderId;
            temp.CustomerName = ordersFromDal[i].CustomerName;
            if (ordersFromDal[i].DateReceived != DateTime.MinValue)
                temp.OrderStatus = BO.EnumOrderStatus.Delivered;
            else if (ordersFromDal[i].DateDelivered != DateTime.MinValue)
                temp.OrderStatus = BO.EnumOrderStatus.Shipped;
            else temp.OrderStatus = BO.EnumOrderStatus.Ordered;

            temp.NumOfItems = amountOfProducts;
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
            //gets the order from dal
            DO.Order O = new DO.Order();
            O = dal?.Order?.GetAll(o => o.OrderId == orderId)?.ToList().FirstOrDefault() ?? throw new DalApi.EntityNotFoundException();
            //move info from dal order to bo order
            BO.Order order = new BO.Order();
            order.Email = O.Email;
            order.CustomerName = O.CustomerName;
            order.OrderId = O.OrderId;
            order.DateDelivered = O.DateDelivered;
            order.DateOrdered = O.DateOrdered;
            order.DateReceived = O.DateReceived;
            //checks the staus of the order
            if (O.DateReceived != DateTime.MinValue)
                order.OrderStatus = BO.EnumOrderStatus.Delivered;
            else if (O.DateDelivered != DateTime.MinValue)
                order.OrderStatus = BO.EnumOrderStatus.Shipped;
            else
                order.OrderStatus = BO.EnumOrderStatus.Ordered;

            //gets the order items from dal
            List<DO.OrderItem>? oi = dal?.OrderItem?.GetAll(p => p.OrderID == orderId)?.ToList();

            //get the order items details from dal and puts then in bl layer
            List<BO.OrderItem> listOfOrderItems = (List<BO.OrderItem>)(IEnumerable<BO.OrderItem>)(
                from item in oi
                let tempItemFromDal = dal?.Item?.GetAll(i => i.ID == item.ItemId)?.ToList().SingleOrDefault()
                let tempBOorderItem = new BO.OrderItem()
                {
                    OrderItemId = item.OrderItemId,
                    ItemId = item.ItemId,
                    ItemName = tempItemFromDal.Value.Name,
                    ItemPrice = tempItemFromDal.Value.Price,
                    Amount = item.Amount,
                    PriceOfItems = item.Amount * tempItemFromDal.Value.Price
                }
                select tempBOorderItem).ToList();
            //calculates the final price of the order
            if (oi != null)
                order.SumOfOrder = oi.Select(o => o.Price * o.Amount).Sum();
            order.Items = listOfOrderItems;
            return order;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }

    }
    //updates date of delivery
    public BO.Order UpdateOrderShipping(int orderId)
    {
        try
        {
            //get the order
            DO.Order orderfromDalToUpdate = dal?.Order.GetAll(o => o.OrderId == orderId)?.ToList().SingleOrDefault() ?? throw new DalApi.EntityNotFoundException();
            if (orderfromDalToUpdate.DateDelivered != DateTime.MinValue)
                throw new BlApi.SentAlreadyException();
            // update the date of shipping
            orderfromDalToUpdate.DateDelivered = DateTime.Now;
            dal?.Order.Update(orderfromDalToUpdate);
            BO.Order order = GetOrderDetails(orderId);
            return order;

        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }
    //updates the date of delivery (receiving)
    public BO.Order UpdateOrderDelivery(int orderId)
    {
        try
        {
            //get the order
            DO.Order orderfromDalToUpdate = dal?.Order.GetAll(o => o.OrderId == orderId)?.ToList().First() ?? throw new Exception();
            if (orderfromDalToUpdate.DateReceived != DateTime.MinValue)
                throw new BlApi.deliveredAlreadyException();
            // update the date of shipping
            orderfromDalToUpdate.DateReceived = DateTime.Now;
            dal?.Order.Update(orderfromDalToUpdate);
            BO.Order order = GetOrderDetails(orderId);
            return order;

        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }

    public BO.OrderTracking OrderTracking(int orderId)
    {
        try
        {
            //gets the order from dal
            DO.Order? order = dal?.Order.GetAll(o => o.OrderId == orderId)?.ToList().First();
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.Id = orderId;
            List<(DateTime?, EnumOrderStatus)> tempTrackingtUples = new();
            // orderTracking.TrackingTuples[0] = ((order ?? throw new DalApi.EntityNotFoundException()).DateOrdered, EnumOrderStatus.Ordered);
            tempTrackingtUples.Add((order.HasValue?(order).Value.DateOrdered:throw new DalApi.EntityNotFoundException(), EnumOrderStatus.Ordered));
            (orderTracking ?? throw new Exception("an unexpected error occured")).OrderStatus = EnumOrderStatus.Ordered;
            if ((order ?? throw new DalApi.EntityNotFoundException()).DateDelivered != DateTime.MinValue)
            {
                tempTrackingtUples.Add(((order ?? throw new DalApi.EntityNotFoundException()).DateDelivered, EnumOrderStatus.Ordered));
                (orderTracking ?? throw new Exception("an unexpected error occured")).OrderStatus = EnumOrderStatus.Shipped;
            }
            if ((order ?? throw new DalApi.EntityNotFoundException()).DateReceived != DateTime.MinValue)
            {
                tempTrackingtUples.Add(((order ?? throw new DalApi.EntityNotFoundException()).DateReceived, EnumOrderStatus.Ordered));
                (orderTracking ?? throw new Exception("an unexpected error occured")).OrderStatus = EnumOrderStatus.Delivered;
            }
            orderTracking.TrackingTuples = tempTrackingtUples;
            return orderTracking ?? throw new Exception("an unexpected error occured");

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
