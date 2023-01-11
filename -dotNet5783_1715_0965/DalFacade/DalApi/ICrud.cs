
namespace DalApi;

public interface ICrud<T>
{
    /// <summary>
    /// gets an item and adds it to the data
    /// </summary>
    /// <param name="item">an object with a type of:item/order/orderitem</param>
    /// <returns>the id of the added obj</returns>
    public int Add(T item);
    /// <summary>
    /// deletes the item from the data 
    /// </summary>
    /// <param name="id">id of the item to delete</param>
    public void Delete(int id);
    /// <summary>
    /// updates the item to the received object
    /// </summary>
    /// <param name="item"> updated item</param>
    public void Update(T item);
    /// <summary>
    /// gets all items that match the requested paremeter
    /// </summary>
    /// <param name="func">required function condition</param>
    /// <returns>requested elements</returns>
    public IEnumerable<T?>? GetAll(Func<T,bool>? func=null);

}

