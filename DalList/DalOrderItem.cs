
using DO;
using DalApi;
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
    public OrderItem GetById(int Id)
    {

        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == Id)
                return DataSource.OrderItems[i];
        }
        throw new EntityNotFoundException();
    }
    /// <summary>
    /// returns existing order items
    /// </summary>
    public IEnumerable<OrderItem> GetAll()
    {
        OrderItem[] oi = new OrderItem[DataSource.OrderItems.Count];
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            oi[i] = DataSource.OrderItems[i];
        }
        return oi;
    }
    /// <summary>
    /// deletes order item by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        bool b = false;
        int index = 0;
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == id)
            {
                index = i;
                b = true;
            }
            if (b == true)
            {
                DataSource.OrderItems.RemoveAt(index);
            }
        }
        if (b == false)
            throw new EntityNotFoundException();
    }
    /// <summary>
    /// updates order item by id
    /// </summary>
    /// <param name="oi"> updated object</param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem oi)
    {
        bool b = false;
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == oi.OrderItemId)
            {
                DataSource.OrderItems[i] = oi;
                b = true;
            }
        }
        if (b == false)
            throw new EntityNotFoundException();
    }

    public OrderItem GetById(int orderId, int productId)
    {
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].OrderID == orderId && DataSource.OrderItems[i].ItemId == productId)
                return DataSource.OrderItems[i];
        }
        throw new EntityNotFoundException();
    }
    public IEnumerable<OrderItem> GetByOrderId(int orderId)
    {
        int index = 0;
        OrderItem[] product = new OrderItem[DataSource.OrderItems.Count];
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].OrderID == orderId)
            {
                product[index++] = DataSource.OrderItems[i];
            }
        }
        return product;
    }
}







