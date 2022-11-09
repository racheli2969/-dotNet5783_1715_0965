﻿
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
    public OrderItem[] View()
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
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderItemId == id)
            {
                b = true;
            }
            if(b==true&&(i+1 < DataSource.Config.IndexOrderItem))
            {
                DataSource.OrderItems[i] = DataSource.OrderItems[i + 1];
            }
            DataSource.OrderItems[DataSource.Config.IndexOrderItem - 1].OrderItemId = 0;
        }
        if (b == false)
            throw new Exception("The item is not exist");
    }
    public void Update(OrderItem oi)
    {
        bool b = false;
        for (int i = 0; i < DataSource.OrderItems.Length; i++)
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
       




       

