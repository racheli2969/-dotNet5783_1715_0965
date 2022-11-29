
namespace DalApi;

public interface ICrud<T>
{
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

