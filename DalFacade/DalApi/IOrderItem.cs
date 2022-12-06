
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    /// <summary>
    /// gets a order id 
    /// </summary>
    /// <param name="orderId"> order id</param>
    /// <returns>products with the same order id</returns>
    public IEnumerable<OrderItem> GetByOrderId(int orderId);
    /// <summary>
    /// gets by order id and the product id
    /// </summary>
    /// <param name="orderId">order id</param>
    /// <param name="productId">product id</param>
    /// <returns>the order item in the requested order with the specific id</returns>
    public OrderItem GetById(int orderId, int productId);
}

