using DalApi;
using DO;
using System.Linq;

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
        item.ID = DataSource.Config.LastItemId;
        DataSource.Items?.Add(item);
        return DataSource.Config.ItemId - 1;

    }
    /// <summary>
    /// returns all the existing items
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Item>? GetAll(Func<Item, bool>? func)
    {
        List<Item?> items;
        if (func == null)
            items = DataSource.Items;
        else items = DataSource.Items.Where(x => x.HasValue && func((Item)x)).ToList();
        if (items == null)
            throw new EntityNotFoundException();
        return items.Cast<Item>(); 
    } 
    /// <summary>
    /// gets an id and deletes that item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception">if id does not exist in the product list</exception>
    public void Delete(int id)
    {
        //check if exists
        int index = DataSource.Items.FindIndex(x => x.HasValue && x.Value.ID == id);
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
        int? index = DataSource.Items?.FindIndex(x => x.HasValue && x.Value.ID == item.ID);
        if (index < 0 || index == null)
            throw new EntityNotFoundException();
        DataSource.Items[(int)index] = item;
    }
    /// <summary>
    /// overload for the update gets an id and updates that product's amount
    /// </summary>
    /// <param name="id">product id</param>
    /// <param name="amount">amount to update to</param>
    public void Update(int id, int amount)
    {
        Item item = new();
        int? index = DataSource.Items?.FindIndex(x => x.HasValue && x.Value.ID == id);
        if (index == null || index < 0)
            throw new EntityNotFoundException();
        item = (Item)DataSource.Items[(int)index];
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
        Item? item = new Item();
        item = DataSource.Items?[DataSource.Items.FindIndex(x => x.HasValue && x.Value.ID == id)];
        return item?.AmountInStock - 1 >= 0;
    }
    /// <summary>
    /// checks if there's enough to order the amount
    /// </summary>
    /// <param name="id">id of product</param>
    ///  /// <param name="amount">amount wanted</param>
    /// <returns>true if available</returns>
    public bool Available(int id, int amount)
    {
        Item? item = new Item();
        item = DataSource.Items?[DataSource.Items.FindIndex(x => x.HasValue && x.Value.ID == id)];
        return item?.AmountInStock - amount >= 0;
    }
}
