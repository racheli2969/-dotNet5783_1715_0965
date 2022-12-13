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
    public int Add(Item item)
    {
        //if item exists already
        if (DataSource.Items.FindIndex(x => ((Item)x).ID == item.ID) > 0)
            throw new EntityDuplicateException();
        DataSource.Items.Add(item);
        return DataSource.Config.ItemId;

    }
    /// <summary>
    /// finds an item by id 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>returns the item</returns>
    /// <exception cref="Exception">not found</exception>
    public Item GetById(int Id)
    {
        Item? item = DataSource.Items.Find(x => ((Item)x).ID == Id);
        if (((Item)item).ID != 0)
            return ((Item)item);
        throw new EntityNotFoundException();
    }
    /// <summary>
    /// returns all the existing items
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Item>? GetAll(Func<Item,bool>?func=null)
    {
        List<Item>? item = new();

        for (int i = 0; i < DataSource.Items.Count; i++)
        {
            item.Add((Item)DataSource.Items[i]);
        }
        return item ;
    }
    /// <summary>
    /// gets an id and deletes that item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception">if id does not exist in the product list</exception>
    public void Delete(int id)
    {
        //check if exists
        int index = DataSource.Items.FindIndex(x => ((Item)x).ID == id);
        if (index < 0)
            throw new EntityNotFoundException();
        DataSource.Items.RemoveAt(index);
    }
    /// <summary>
    /// gets an object and searches for it's id in the data array and then replaces the updated object
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="Exception">if there is no object with that id an exception is thrown</exception>
    public void Update(Item item)
    {
        int index = DataSource.Items.FindIndex(x => ((Item)x).ID == item.ID);
        if (index < 0)
            throw new EntityNotFoundException();
        DataSource.Items[index] = item;
    }
    /// <summary>
    /// overload for the update gets an id and updates that product's amount
    /// </summary>
    /// <param name="id">product id</param>
    /// <param name="amount">amount to update to</param>
    public void Update(int id, int amount)
    {
        Item item = new Item();
        item = GetById(id);
        if (item.AmountInStock - amount >= 0)
        {
            item.AmountInStock -= amount;
            Update(item);
        }
        else throw new NegativeAmount();
    }
    /// <summary>
    /// checks if there's enough to order one
    /// </summary>
    /// <param name="id">id of product</param>
    /// <returns>true if available</returns>
    public bool Available(int id)
    {
        Item item = GetById(id);
        return item.AmountInStock - 1 >= 0;
    }
    /// <summary>
    /// checks if there's enough to order the amount
    /// </summary>
    /// <param name="id">id of product</param>
    ///  /// <param name="amount">amount wanted</param>
    /// <returns>true if available</returns>
    public bool Available(int id, int amount)
    {
        Item item = GetById(id);
        return item.AmountInStock - amount >= 0;
    }
}
