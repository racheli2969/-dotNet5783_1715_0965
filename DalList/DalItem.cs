
using DO;
using System.ComponentModel;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
namespace Dal;

public class DalItem
{
   public void AddItem()
    {
    }
public Item ReadById(int Id)
    {
        Item
        for (int i = 0; i < Items.Length; i++)
        {
            if ( DataSource.Items[i].id == Id)
                return Items[i];
        }  
    }
  public Item[] read()
    {
        return Items;
    }

    public void Delete(int id)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].id == Id)
               Items[i] = null;         
        }
    }
    public void Update()
    {

    }
}
       

