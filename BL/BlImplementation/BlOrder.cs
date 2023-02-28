using BO;
using DO;
using System.Runtime.CompilerServices;

namespace BlImplementation;
public class BlOrder : BlApi.IOrder
{
    private DalApi.IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        List<DO.Order>? ordersFromDal = new List<DO.Order>();
        BO.OrderForList temp;
        //gets all orders from dal
        lock (dal)
        {
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
                if (ordersFromDal[i].DateDelivered != DateTime.MinValue)
                    temp.OrderStatus = BO.EnumOrderStatus.Delivered;
                else if (ordersFromDal[i].DateShipped != DateTime.MinValue)
                    temp.OrderStatus = BO.EnumOrderStatus.Shipped;
                else temp.OrderStatus = BO.EnumOrderStatus.Ordered;

                temp.NumOfItems = amountOfProducts;
                temp.Price = finalPrice;
                orders.Add(temp);
            }
        }
        return orders;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order GetOrderDetails(int orderId)
    {
        if (orderId < 0)
            throw new BlApi.NegativeIdException();
        try
        {
            //gets the order from dal
            DO.Order O = new DO.Order();
            lock (dal)
            {
                O = dal?.Order?.GetAll(o => o.OrderId == orderId)?.ToList().FirstOrDefault() ?? throw new DalApi.EntityNotFoundException();
            }
            //move info from dal order to bo order
            BO.Order order = new BO.Order();
            order.Email = O.Email;
            order.CustomerName = O.CustomerName;
            order.OrderId = O.OrderId;
            order.DateDelivered = O.DateDelivered;
            order.DateOrdered = O.DateOrdered;
            order.DateShipped = O.DateShipped;
            //checks the staus of the order
            if (O.DateDelivered != DateTime.MinValue)
                order.OrderStatus = BO.EnumOrderStatus.Delivered;
            else if (O.DateShipped != DateTime.MinValue)
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
    //updates date of shipping
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderShipping(int orderId)
    {
        try
        {
            //get the order
            DO.Order orderfromDalToUpdate = dal?.Order.GetAll(o => o.OrderId == orderId)?.ToList().FirstOrDefault() ?? throw new DalApi.EntityNotFoundException();
            if (orderfromDalToUpdate.DateShipped != DateTime.MinValue)
                throw new BlApi.SentAlreadyException();
            // update the date of shipping
            orderfromDalToUpdate.DateShipped = DateTime.Now;
            lock (dal)
            {
                dal?.Order.Update(orderfromDalToUpdate);
            }
            BO.Order order = GetOrderDetails(orderId);
            return order;

        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }
    //updates the date of delivery (receiving)

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderDelivery(int orderId)
    {
        try
        {
            //get the order
            DO.Order orderfromDalToUpdate = dal?.Order.GetAll(o => o.OrderId == orderId)?.ToList().First() ?? throw new Exception();
            if (orderfromDalToUpdate.DateDelivered != DateTime.MinValue)
                throw new BlApi.deliveredAlreadyException();
            // update the date of shipping
            orderfromDalToUpdate.DateDelivered = DateTime.Now;
            lock (dal)
            {
                dal?.Order.Update(orderfromDalToUpdate);
            }
            BO.Order order = GetOrderDetails(orderId);
            return order;

        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
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
            tempTrackingtUples.Add((order.HasValue ? (order).Value.DateOrdered : throw new DalApi.EntityNotFoundException(), EnumOrderStatus.Ordered));
            (orderTracking ?? throw new Exception("an unexpected error occured")).OrderStatus = EnumOrderStatus.Ordered;
            if ((order ?? throw new DalApi.EntityNotFoundException()).DateShipped != DateTime.MinValue)
            {
                tempTrackingtUples.Add(((order ?? throw new DalApi.EntityNotFoundException()).DateShipped, EnumOrderStatus.Delivered));
                (orderTracking ?? throw new Exception("an unexpected error occured")).OrderStatus = EnumOrderStatus.Shipped;
            }
            if ((order ?? throw new DalApi.EntityNotFoundException()).DateDelivered != DateTime.MinValue)
            {
                tempTrackingtUples.Add(((order ?? throw new DalApi.EntityNotFoundException()).DateDelivered, EnumOrderStatus.Shipped));
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderDetails(int orderId, int productId, int amount)
    {
        BO.Order order = new BO.Order();
        //do something
        return order;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? GetOldestOrderNumber()
    {
        List<DO.Order>? orders1 = new List<DO.Order>();
        orders1 = dal?.Order?.GetAll(o => o.DateDelivered == DateTime.MinValue)?.ToList();
        //if all the orders dates are updated then we can return null to show we are done
        if (orders1 == null)
            return null;
        orders1.ForEach(i => Console.WriteLine(i.ToString()));
        DO.Order? smallestDateOrdered = new();
        smallestDateOrdered = (from order in orders1
                               orderby order.DateOrdered ascending
                               select order).ToList().FirstOrDefault();

        DO.Order? smallestDateShipped = new();
        smallestDateShipped = (from order in orders1
                               orderby order.DateShipped ascending
                               where order.DateShipped != DateTime.MinValue
                               select order).ToList().FirstOrDefault();
        if (smallestDateShipped == null)
            return smallestDateOrdered.Value.OrderId;
        int res = DateTime.Compare(smallestDateOrdered.Value.DateOrdered, smallestDateShipped.Value.DateShipped);
        int? id = res > 0 ? smallestDateShipped.Value.OrderId : smallestDateOrdered.Value.OrderId;
       id=id==0?null: id;   
        return id;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int TimeToUpdateAllDatesForSimulation()
    {
        try
        {
            List<DO.Order>? orders1 = new List<DO.Order>();
            orders1 = dal?.Order?.GetAll(o => o.DateDelivered == DateTime.MinValue)?.ToList();
            int num = (orders1 ?? throw new Exception("no elements")).Count;
            num += (from order in orders1
                    where order.DateShipped == DateTime.MinValue
                    select order).Count();
            return num;
        }
        catch
        {
            return 0;
        }
    }
}
