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
        DataSource.Orders[DataSource.Orders.Count] = order;
        return DataSource.Config.OrderId;
    }
    public Order GetById(int id)
    {
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i].OrderId == id)
                return DataSource.Orders[i];
        }
        throw new EntityNotFoundException();
    }
    /// <summary>
    /// returns all existing orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetAll()
    {
        Order[] order = new Order[DataSource.Orders.Count];
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            order[i] = DataSource.Orders[i];
        }
        return order;
    }
    /// <summary>
    /// delete's an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception">if the item does not exist</exception>
    public void Delete(int id)
    {
        bool b = false;
        int index = 0;
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i].OrderId == id)
            {
                b = true;
                index = i;
            }
            if (b == true)
            {
                DataSource.Orders.RemoveAt(index);
            }
        }
        if (b == false)
            throw new EntityNotFoundException();
    }
    /// <summary>
    /// finds an order by id and updates it with the order
    /// </summary>
    /// <param name="order"> new order with existing id</param>
    /// <exception cref="Exception"></exception>
    public void Update(Order order)
    {
        bool b = false;
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i].OrderId == order.OrderId)
            {
                DataSource.Orders[i] = order;
                b = true;
            }
        }
        if (b == false)
            throw new EntityNotFoundException();
    }


}
