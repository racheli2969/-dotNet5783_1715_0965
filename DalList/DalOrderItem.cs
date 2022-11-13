
using DO;
namespace Dal;
public static class DalOrderItem
{
    /// <summary>
    /// gets a new order item from the main and adds it to the order item array
    /// </summary>
    /// <param name="oi"></param>
    /// <returns>returns the added order item's id</returns>
    public static int Add(OrderItem oi)
    {
            DataSource.OrderItems[DataSource.Config.LastIndexOrderItem] = oi;
            return DataSource.Config.OrderItemId;
    }
    /// <summary>
    /// finds an order item by id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>returns the order item</returns>
    /// <exception cref="Exception"></exception>
    public static OrderItem ViewById(int Id)
    {
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == Id)
                return DataSource.OrderItems[i];
        }
        throw new Exception("The item is not exist");
    }
    /// <summary>
    /// returns existing order items
    /// </summary>
    public static OrderItem[] ViewAll()
    {
        OrderItem[] oi = new OrderItem[DataSource.Config.IndexOrderItem];
        for(int i = 0; i < DataSource.Config.IndexOrderItem; i++)
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
    public static void Delete(int id)
    {
        bool b = false;
        int index = 0;
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == id)
            {
                index = i;
                b = true;
            }
            if(b==true)
            {
                DataSource.OrderItems[index] = DataSource.OrderItems[DataSource.Config.IndexOrderItem-1];
                DataSource.Config.IndexOrderItem--;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
    }
    /// <summary>
    /// updates order item by id
    /// </summary>
    /// <param name="oi"> updated object</param>
    /// <exception cref="Exception"></exception>
    public static void Update(OrderItem oi)
    {
        bool b = false;
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == oi.OrderItemId)
            {
                DataSource.OrderItems[i] = oi;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
    }
}
       




       

