
using DO;
using System.ComponentModel;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
namespace Dal;

 public class DalItem
 {
     public void Add(Item item)
     {
        Items[Config.LastIndexItem] = item;
     }
     public Item ViewById(int Id)
     {
        for (int i = 0; i <indexItem; i++)
        {
            if (DataSource.Items[i].id == Id)
                return Items[i];
        }
        throw new Exception("The item is not exist");
     }
     public Item[] ViewAll()
     {
        Item item = new Item[Config.LastIndexItem];
        item = Items;
        return item;
     }
     public void Delete(int id)
     {
        bool b = false;
        for (int i = 0; i < Items.Length; i++)
        {
            if (DataSource.Items[i].id == Id)
            {
                Items[i] = null;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
     }
     public void Update(Item item)
     {
        bool b = false;
        for(int i = 0; i < Items.Length; i++)
        {
            if (Items[i].ID == item.ID)
            {
                Items[i] = item;
                b = true;
            }
        }
        if (b == false)
            throw new Exception("The item is not exist");
     }
}
       

