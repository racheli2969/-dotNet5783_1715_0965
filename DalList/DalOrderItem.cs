
using DO;
using DalApi;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// gets a new order item from the main and adds it to the order item array
    /// </summary>
    /// <param name="oi"></param>
    /// <returns>returns the added order item's id</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem oi)
    {
        oi.OrderItemId = DataSource.Config.OrderItemId;
        DataSource.OrderItems.Add(oi);
        return DataSource.Config.OrderItemId;
    }
    /// <summary>
    /// returns existing order items
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem>? GetAll(Func<OrderItem,bool> func)
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem oi)
    {
        int index = DataSource.OrderItems.FindIndex(orderItem => orderItem.HasValue&&((OrderItem)orderItem).OrderItemId == oi.OrderItemId);
        if (index < 0)
            throw new EntityNotFoundException();
        DataSource.OrderItems[index] = oi;
    }
}







