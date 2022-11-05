
using DO;

namespace Dal;

public class DalOrder
{
    public void AddItem(Order order)
    {
       orders[Config.LastIndexItem] = order;
    }
    public Item ReadById(int Id)
    {
        for (int i = 0; i < orders.Length; i++)
        {
            if (DataSource.orders[i].id == Id)
                return orders[i];
        }
        throw new Exception("The item is not exist");
    }
    public Item[] Read()
    {
       Order order = new Order[Config.LastIndexItem];
        order = orders;
        return item;
    }
    public void Delete(int id)
    {
        bool b = false;
        for (int i = 0; i < orders.Length; i++)
        {
            if (orders[i].id == Id)
            {
                orders[i] = null;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
    }
    public void Update(Order order)
    {
        bool b = false;
        for (int i = 0; i < orders.Length; i++)
        {
            if (orders[i].ID == order.OrderId)
            {
                orders[i] = order;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
    }
}
       


    

}
       

