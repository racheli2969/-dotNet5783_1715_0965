using DO;
namespace Dal;

public static class DalOrder
{
    public static int Add(Order order)
    {
        DataSource.Orders[DataSource.Config.IndexOrder] = order;
        return DataSource.Config.IndexOrder;
    }
    public static Order ViewById(int Id)
    {
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.Orders[i].OrderId == Id)
                return DataSource.Orders[i];
        }
        throw new Exception("The item is not exist");
    }
    public static Order[] ViewAll()
    {
       Order[] order = new Order[DataSource.Config.IndexOrder];
       for(int i=0;i< DataSource.Config.IndexOrder; i++)
       {
            order[i] = DataSource.Orders[i];
       }
        return order;
    }
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
            throw new Exception("The item is not exist");
    }
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
            throw new Exception("The item is not exist");
    }
}

       

