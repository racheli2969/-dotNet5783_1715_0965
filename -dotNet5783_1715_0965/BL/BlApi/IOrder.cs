
using BO;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// returns all the orders in the list
    /// </summary>
    /// <returns>orders</returns>
    public IEnumerable<OrderForList> GetOrderList();
    /// <summary>
    /// searches for the order details
    /// </summary>
    /// <param name="orderId">order id</param>
    /// <returns>the order</returns>
    public Order GetOrderDetails(int orderId);
    /// <summary>
    /// update the date of shipping for manager
    /// </summary>
    /// <param name="orderId">order id</param>
    /// <returns>the updated order</returns>
    public Order UpdateOrderShipping(int orderId);
    /// <summary>
    /// update the date of delivery for manager
    /// </summary>
    /// <param name="orderId">order id</param>
    /// <returns>the updated order</returns>
    public Order UpdateOrderDelivery(int orderId);
    public OrderTracking OrderTracking(int orderId);
    /// <summary>
    /// update the amount of a product in order for manager
    /// </summary>
    /// <param name="orderId">order id</param>
    /// <param name="productId">product to look for in order</param>
    /// <param name="amount">amount to change to</param>
    /// <returns>the updated order</returns>
    public Order UpdateOrderDetails(int orderId, int productId, int amount);


}
