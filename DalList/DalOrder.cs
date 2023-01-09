using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder
{
    /// <summary>
    /// gets a new order from the main and adds it to the order array
    /// </summary>
    /// <param name="order"></param>
    /// <returns>returns the added order's id</returns>
    public int Add(Order order)
    {
        order.OrderId = DataSource.Config.LastOrderId;
        DataSource.Orders?.Add(order);
        return DataSource.Config.OrderId;
    }
    /* public Order GetById(int id)
     {
         for (int i = 0; i < DataSource.Orders.Count; i++)
         {
             if (((Order)DataSource.Orders[i]).OrderId == id)
                 return (Order)DataSource.Orders[i];
         }
         Order? order = (Order)DataSource.Orders.Find(order => ((Order)order).OrderId == id);
         if (order == null)
             throw new EntityNotFoundException();
         return (Order)order;
     }*/
    /// <summary>
    /// returns all existing orders
    /// </summary>
    /// <returns></returns>
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public IEnumerable<Order>? GetAll(Func<Order, bool> func)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    {
        List<Order?> orders;
        if (func == null)
            orders = DataSource.Orders;
        else orders = DataSource.Orders.Where(x => x.HasValue && func((Order)x)).ToList();
        if (orders == null)
            throw new EntityNotFoundException();
        return orders.Cast<Order>();
    }
    /// <summary>
    /// delete's an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception">if the item does not exist</exception>
    public void Delete(int id)
    {
        int? idx = DataSource.Orders?.FindIndex(order => order.Value.OrderId == id);
        if (idx == -1 || idx == null)
            throw new EntityNotFoundException();
        DataSource.Orders?.RemoveAt((int)idx);
    }
    /// <summary>
    /// finds an order by id and updates it with the order
    /// </summary>
    /// <param name="order"> new order with existing id</param>
    /// <exception cref="Exception"></exception>
    public void Update(Order order)
    {
        int? idx = DataSource.Orders?.FindIndex(order => order.Value.OrderId == order.Value.OrderId);
        if (idx == -1 || idx == null || DataSource.Orders==null)
            throw new EntityNotFoundException();
        DataSource.Orders[(int)idx] = order;
    }
}
