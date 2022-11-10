
using DO;

namespace Dal;
public class DalOrderItem
{
    public int Add(OrderItem oi)
    {
        DataSource.OrderItems[DataSource.Config.LastIndexOrderItem] = oi;
        return DataSource.Config.IndexOrderItem;
    }
    public OrderItem ViewById(int Id)
    {
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == Id)
                return DataSource.OrderItems[i];
        }
        throw new Exception("The item is not exist");
    }
    public OrderItem[] ViewAll()
    {
        OrderItem[] oi = new OrderItem[DataSource.Config.IndexOrderItem];
        for(int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            oi[i] = DataSource.OrderItems[i];
        }
        return oi;
    }
    public void Delete(int id)
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
    public void Update(OrderItem oi)
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
       




       

