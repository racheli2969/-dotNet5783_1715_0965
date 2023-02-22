using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrder : IOrder
{
    /// <summary>
    /// gets a new order from the main and adds it to the order array
    /// </summary>
    /// <param name="order"></param>
    /// <returns>returns the added order's id</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order order)
    {
        order.OrderId = DataSource.Config.LastOrderId;
        DataSource.Orders?.Add(order);
        return DataSource.Config.OrderId;
    }
    /// <summary>
    /// returns all existing orders
    /// </summary>
    /// <returns></returns>
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order>? GetAll(Func<Order, bool> func)
    {
        List<Order?> orders = new();
        if (func == null)
            orders = DataSource.Orders;
        else orders = DataSource.Orders.Where(x => x.HasValue && func((Order)x)).ToList();
        return orders.Cast<Order>() ?? throw new EntityNotFoundException(); ;
    }
    /// <summary>
    /// delete's an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception">if the item does not exist</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        int? idx = DataSource.Orders?.FindIndex(order => (order ?? throw new NullObject()).OrderId == id);
        if (idx == -1 || idx == null)
            throw new EntityNotFoundException();
        DataSource.Orders?.RemoveAt((int)idx);
    }
    /// <summary>
    /// finds an order by id and updates it with the order
    /// </summary>
    /// <param name="order"> new order with existing id</param>
    /// <exception cref="Exception"></exception>

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order order)
    {
        int? idx = DataSource.Orders?.FindIndex(o => (o ?? throw new NullObject()).OrderId == order.OrderId);
        if (idx == -1 || idx == null || DataSource.Orders == null)
            throw new EntityNotFoundException();
        DataSource.Orders[(int)idx] = order;
    }
}
