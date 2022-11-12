
using DO;
namespace Dal;
 public class DalItem
 {
     public int Add(Item item)
     {
        DataSource.Items[DataSource.Config.LastIndexItem] = item;
        return DataSource.Config.IndexItem;
     }
     public Item ViewById(int Id)
     {
        for (int i = 0; i < DataSource.Config.IndexItem; i++)
        {
            if (DataSource.Items[i].ID == Id)
                return DataSource.Items[i];
        }
        throw new Exception("The item does not exist");
     }
     public Item[] ViewAll()
     {
        Item[] item = new Item[DataSource.Config.IndexItem];
       for(int i = 0; i < DataSource.Config.IndexItem; i++)
        {
            item[i] = DataSource.Items[i];
        }
        return item;
     }
     public void Delete(int id)
     {
        bool b = false;
        int index = 0;
        for (int i = 0; i < DataSource.Config.IndexItem; i++)
        {
            if (DataSource.Items[i].ID == id)
            { 
                b = true;
                index = i;
            }
            if (b == true)
            {
            DataSource.Items[index] = DataSource.Items[DataSource.Config.IndexItem];
                DataSource.Config.IndexItem--;
            }
        }
        if (b == false)
            throw new Exception("The item does not exist");
     }
     public void Update(Item item)
     {
        bool b = false;
        for(int i = 0; i < DataSource.Config.IndexItem; i++)
        {
            if (DataSource.Items[i].ID == item.ID)
            {
                DataSource.Items[i] = item;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item does not exist");
     }
}
