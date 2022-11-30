
using DO;
namespace DalApi;

public interface IItem : ICrud<Item>
{
<<<<<<< HEAD
    public interface IItem : ICrud<Item>
    {

    }
=======
    /// <summary>
    /// checks if there is enough in stock 
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <returns>true if the product is availabe</returns>
    public bool Available(int id);
    /// <summary>
    /// checks if there is a bigger or equal amount of the product in stock
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <param name="amount">the amount</param>
    /// <returns>true if available</returns>
    public bool Available(int id, int amount);
    /// <summary>
    /// overload for update gets an id and amount and takes off that amount from the stock
    /// </summary>
    /// <param name="id">product id</param>
    /// <param name="amount">amount to subtract</param>
    public void Update(int id, int amount);
}
