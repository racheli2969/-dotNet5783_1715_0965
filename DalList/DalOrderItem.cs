
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
    /// finds an order item by id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>returns the order item</returns>
    /// <exception cref="Exception"></exception>
   /* public OrderItem GetById(int Id)
    {
        OrderItem? orderItem = DataSource.OrderItems.Find(o => ((OrderItem)o).OrderItemId == Id);
        if(orderItem==null)
            throw new EntityNotFoundException();
        return (OrderItem)orderItem;
       
    }*/
    /// <summary>
    /// returns existing order items
    /// </summary>
    public IEnumerable<OrderItem>? GetAll(Func<OrderItem,bool> func)
    {
        return func == null ? DataSource.OrderItems : DataSource.OrderItems.Where(func).ToList();
    }
    /// <summary>
    /// deletes order item by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int index = DataSource.OrderItems.FindIndex(orderItem => ((OrderItem)orderItem).OrderItemId == id);
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
        int index = DataSource.OrderItems.FindIndex(orderItem => ((OrderItem)orderItem).OrderItemId == oi.OrderItemId);
        if (index < 0)
            throw new EntityNotFoundException();
        DataSource.OrderItems[index] = oi;
    }
    /// <summary>
    /// in dal layer receives an order id and productid and searches for them in the order item list
    /// </summary>
    /// <param name="orderId">order id</param>
    /// <param name="productId">product id</param>
    /// <returns>the order item found or exception</returns>
    /// <exception cref="EntityNotFoundException"></exception>
   /* public OrderItem GetById(int orderId, int productId)
    {
        OrderItem? orderItem = DataSource.OrderItems.Find(o => ((OrderItem)o).OrderID == orderId && ((OrderItem)o).ItemId==productId);
        if (orderItem == null)
            throw new EntityNotFoundException();
        return (OrderItem)orderItem;
    }*/
    /// <summary>
    ///in dal searches for all items in a certain order
    /// </summary>
    /// <param name="orderId">order id</param>
    /// <returns>the items</returns>
   /* public IEnumerable<OrderItem>? GetByOrderId(int orderId)
    {
        List<OrderItem> product = new(DataSource.OrderItems.Count);
        product = DataSource.OrderItems.FindAll(p => ((OrderItem)p).OrderItemId == orderId);
        return product;
    }*/
}







