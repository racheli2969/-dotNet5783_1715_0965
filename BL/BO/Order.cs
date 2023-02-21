using System.Text;
namespace BO;
/// <summary>
/// class for order 
/// </summary>
public class Order
{
    /// <summary>
    /// order id
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// customer name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customer email
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public EnumOrderStatus? OrderStatus { get; set; }
    /// <summary>
    /// date of order
    /// </summary>
    public DateTime? DateOrdered { get; set; }
    /// <summary>
    /// date of delivery
    /// </summary>
    public DateTime? DateShipped { get; set; }
    /// <summary>
    /// date received
    /// </summary>
    public DateTime? DateDelivered { get; set; }
    /// <summary>
    /// list of items of type orderItem
    /// </summary>
    public List<OrderItem>? Items { get; set; }
    /// <summary>
    /// sum of order type double
    /// </summary>
    public double SumOfOrder { get; set; }
    /// <summary>
    /// override of the class to string
    /// </summary>
    /// <returns> Order ID , Customer Name, Email,Order Status,	Date Ordered,Date Delivered, Date Received, items ordered,Sum of order</returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Order ID:{0}\n Customer Name:{1}\n Email:{2}\n Order Status:{3}\nDate Ordered:{4}\n Date Delivered{5}\nDate Received: {6}\nfinal price:{7};", OrderId, CustomerName, Email, OrderStatus, DateOrdered,DateShipped, DateDelivered, SumOfOrder);
        if (Items != null)
            foreach (BO.OrderItem item in Items)
            {
                sb.AppendFormat(" Order Item: {0}\n", item);
            }
        return sb.ToString();
    }
}
