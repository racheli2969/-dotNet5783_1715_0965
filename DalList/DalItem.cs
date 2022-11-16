using DalApi;
using DO;
namespace Dal;
/// <summary>
/// item data layer
/// </summary>

internal class DalItem : IItem
{
    /// <summary>
    /// gets a new item from the main and adds it to the item array
    /// </summary>
    /// <param name="item"></param>
    /// <returns>returns the added item's id</returns>
     public static int Add(Item item)
     {
        DataSource.Items[DataSource.Items.Count] = item;
        return DataSource.Config.ItemId;
     }
    /// <summary>
    /// finds an item by id 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>returns the item</returns>
    /// <exception cref="Exception"></exception>
     public static Item ViewById(int Id)
     {
        for (int i = 0; i < DataSource.Items.Count; i++)
        {
            if (DataSource.Items[i].ID == Id)
                return DataSource.Items[i];
        }
        throw new Exception("The item does not exist");
     }
    /// <summary>
    /// returns all the existing items
    /// </summary>
    /// <returns></returns>
     public IEnumerable<Item> ViewAll()
     {
        Item[] item = new Item[DataSource.Items.Count];
       for(int i = 0; i < DataSource.Items.Count; i++)
        {
            item[i] = DataSource.Items[i];
        }
        return item;
     }
    /// <summary>
    /// gets an id and deletes that item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
     public static void Delete(int id)
     {
        bool b = false;
        int index = 0;
        for (int i = 0; i < DataSource.Items.Count; i++)
        {
            if (DataSource.Items[i].ID == id)
            { 
                b = true;
                index = i;
            }
            if (b == true)
            {
                DataSource.Items.RemoveAt(index);
            }
        }
        if (b == false)
            throw new Exception("The item does not exist");
     }
    /// <summary>
    /// gets an object and searches for it's id in the data array and then replaces the updated object
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="Exception">if there is no object with that id an exception is thrown</exception>
     public static void Update(Item item)
     {
        bool b = false;
        for(int i = 0; i < DataSource.Items.Count; i++)
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
