using BL;
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
    public EnumOrderStatus OrderStatus { get; set; }
    /// <summary>
    /// date of order
    /// </summary>
    public DateTime DateOrdered { get; set; }
    /// <summary>
    /// date of delivery
    /// </summary>
    public DateTime DateDelivered { get; set; }
    /// <summary>
    /// date received
    /// </summary>
    public DateTime DateReceived { get; set; }
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
    public override string ToString() => $@"
    Order ID: {OrderId} 
    Customer Name: {CustomerName} 
    Email: {Email}
    Order Status: {OrderStatus}
    Date Ordered: {DateOrdered}
	Date Delivered: {DateDelivered}
	Date Received: {DateReceived}
    items ordered: Items.ForEach(item=>Console.WriteLine(item.ToString),
    Sum of order: {SumOfOrder}
";
}
