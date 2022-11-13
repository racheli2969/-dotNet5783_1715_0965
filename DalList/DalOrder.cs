using DO;
namespace Dal;

public static class DalOrder
{
    /// <summary>
    /// gets a new order from the main and adds it to the order array
    /// </summary>
    /// <param name="order"></param>
    /// <returns>returns the added order's id</returns>
    public static int Add(Order order)
    {
        DataSource.Orders[DataSource.Config.IndexOrder] = order;
        return DataSource.Config.OrderId;
    }
    public static Order ViewById(int id)
    {
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.Orders[i].OrderId == id)
                return DataSource.Orders[i];
        }
        throw new Exception("The item does not exist");
    }
    /// <summary>
    /// returns all existing orders
    /// </summary>
    /// <returns></returns>
    public static Order[] ViewAll()
    {
       Order[] order = new Order[DataSource.Config.IndexOrder];
       for(int i=0;i< DataSource.Config.IndexOrder; i++)
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
    public static void Delete(int id)
    {
        bool b = false;
        int index = 0;
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.Orders[i].OrderId == id)
            {
                b = true;
                index = i;
            }
            if(b==true)
            {
                DataSource.Config.IndexOrder--;
                DataSource.Orders[index] = DataSource.Orders[DataSource.Config.IndexOrder-1];
            }
        }
        if (b == false)
            throw new Exception("The item does not exist");
    }
    /// <summary>
    /// finds an order by id and updates it with the order
    /// </summary>
    /// <param name="order"> new order with existing id</param>
    /// <exception cref="Exception"></exception>
    public static void Update(Order order)
    {
        bool b = false;
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.Orders[i].OrderId == order.OrderId)
            {
                DataSource.Orders[i] = order;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item does not exist");
    }
}

       

