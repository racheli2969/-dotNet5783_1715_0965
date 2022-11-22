using Dal;
namespace BO;
public class Order
{
    public int OrderId { get; set; }
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public EnumOrderStatus OrderStatus { get; set; }
    public DateTime DateOrdered { get; set; }
    public DateTime DateDelivered { get; set; }
    public DateTime DateReceived { get; set; }
    public List<OrderItem> ?Items { get; set; }
    public double SumOfOrder { get; set; }
    public override string ToString() => $@"

Order ID={OrderId}, Customer Name: {CustomerName}, 
Email: {Email},
    	Order Status: {OrderStatus},
    	Date Ordered: {DateOrdered},
	Date Delivered: {DateDelivered},
	Date Received: {DateReceived},
items ordered: {Items.ToString},
Sum of order: {SumOfOrder}
";
}
