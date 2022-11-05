
using DO;

namespace Dal;

public class DalOrderItem
{

    public void AddItem(Item item)
    {
        orderItems[Config.LastIndexItem] = item;
    }
    public Item ReadById(int Id)
    {
        for (int i = 0; i < orderItems.Length; i++)
        {
            if (DataSource.orderItems[i].id == Id)
                return orderItems[i];
        }
        throw new Exception("The item is not exist");
    }
    public Item[] Read()
    {
         = new Item[Config.LastIndexItem];
        item = orderItems;
        return item;
    }
    public void Delete(int id)
    {
        bool b = false;
        for (int i = 0; i < orderItems.Length; i++)
        {
            if (orderItems[i].id == Id)
            {
                orderItems[i] = null;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
    }
    public void Update(Item item)
    {
        bool b = false;
        for (int i = 0; i < orderItems.Length; i++)
        {
            if (orderItems[i].ID == item.ID)
            {
                orderItems[i] = item;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
    }
}
       



}
       

