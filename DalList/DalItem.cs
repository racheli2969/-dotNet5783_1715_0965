
using Dal.;

namespace Dal.DO;
public class DalItem
 {
     public void Add(Item item)
     {
       DataSource.Items[DataSource.Config.LastIndexItem] = item;
     }
     public Item ViewById(int Id)
     {
        for (int i = 0; i < DataSource.Items.Length; i++)
        {
            if (DataSource.Items[i].id == Id)
                return DataSource.Items[i];
        }
        throw new Exception("The item does not exist");
     }
     public Item[] ViewAll()
     {
        Item item = new Item[DataSource.Config.LastIndexItem];
        item = DataSource.Items;
        return item;
     }
     public void Delete(int id)
     {
        bool b = false;
        for (int i = 0; i < DataSource.Items.Length; i++)
        {
            if (DataSource.Items[i].id == id)
            {
                DataSource.Items[i] = null;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item does not exist");
     }
     public void Update(Item item)
     {
        bool b = false;
        for(int i = 0; i < DataSource.Items.Length; i++)
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
       

