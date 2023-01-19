
using DO;
using DalApi;
using System.Linq;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// gets a new order item from the main and adds it to the order item array
    /// </summary>
    /// <param name="oi"></param>
    /// <returns>returns the added order item's id</returns>
    public int Add(OrderItem oi)
    {
        oi.OrderItemId = DataSource.Config.OrderItemId;
        DataSource.OrderItems.Add(oi);
        return DataSource.Config.OrderItemId;
    }
    /// <summary>
    /// returns existing order items
    /// </summary>
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public IEnumerable<OrderItem>? GetAll(Func<OrderItem,bool> func)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    {
        List<OrderItem?> orderItems;
        if (func == null)
            orderItems = DataSource.OrderItems;
        else orderItems = DataSource.OrderItems.Where(x => x.HasValue && func((OrderItem)x)).ToList();
        if (orderItems == null)
            throw new EntityNotFoundException();
        return orderItems.Cast<OrderItem>();
    }
    /// <summary>
    /// deletes order item by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int index = DataSource.OrderItems.FindIndex(orderItem => orderItem.HasValue&&((OrderItem)orderItem).OrderItemId == id);
        if (index < 0)
            throw new EntityNotFoundException();
        DataSource.OrderItems.RemoveAt(index);

    }
    /// <summary>
    /// updates order item by id
    /// </summary>
    /// <param name="oi"> updated object</param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem oi)
    {
        int index = DataSource.OrderItems.FindIndex(orderItem => orderItem.HasValue&&((OrderItem)orderItem).OrderItemId == oi.OrderItemId);
        if (index < 0)
            throw new EntityNotFoundException();
        DataSource.OrderItems[index] = oi;
    }
}







