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
        order.OrderId = DataSource.Config.OrderId;
        DataSource.Orders.Add(order);
        return DataSource.Config.OrderId;
    }
    public Order GetById(int id)
    {
        /* for (int i = 0; i < DataSource.Orders.Count; i++)
         {
             if (((Order)DataSource.Orders[i]).OrderId == id)
                 return (Order)DataSource.Orders[i];
         }*/
        Order? order = (Order)DataSource.Orders.Find(order => ((Order)order).OrderId == id);
        if (order == null)
            throw new EntityNotFoundException();
        return (Order)order;
    }
    /// <summary>
    /// returns all existing orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetAll(Func<Order, bool> func )
    {
        List<Order>? orderList = new List<Order>(DataSource.Orders.Count);
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            orderList.Add((Order)DataSource.Orders[i]);
        }
        return orderList;
    }
    /// <summary>
    /// delete's an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception">if the item does not exist</exception>
    public void Delete(int id)
    {
        int idx = DataSource.Orders.FindIndex(order => ((Order)order).OrderId == id);
        if (idx == -1)
            throw new EntityNotFoundException();
        DataSource.Orders.RemoveAt(idx);
    }
    /// <summary>
    /// finds an order by id and updates it with the order
    /// </summary>
    /// <param name="order"> new order with existing id</param>
    /// <exception cref="Exception"></exception>
    public void Update(Order order)
    {
        int idx = DataSource.Orders.FindIndex(order => ((Order)order).OrderId == ((Order)order).OrderId);
        if (idx == -1)
            throw new EntityNotFoundException();

        DataSource.Orders[idx] = order;
    }
}
