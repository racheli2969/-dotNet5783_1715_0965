
namespace DalApi;

public interface ICrud<T>
{
    /// <summary>
    /// gets an item and adds it to the data
    /// </summary>
    /// <param name="item">an object with a type of:item/order/orderitem</param>
    /// <returns>the id of the added obj</returns>
    public int Add(T item);
    public void Delete(int id);
    public void Update(T item);
    /// <summary>
    /// finds a specified type by id 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>returns the item</returns>
    /// <exception cref="Exception">not found</exception>
    public T GetById(int id);
    public IEnumerable<T> GetAll();

}

