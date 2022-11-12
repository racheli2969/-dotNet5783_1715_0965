

using Dal;
namespace DO;

public struct Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public DateTime DateOrdered { get; set; }
    public DateTime DateDelivered { get; set; }
    public DateTime DateReceived { get; set; }

    public override string ToString() => $@"
Order ID={OrderId}, Customer Name: {CustomerName}, 
Email: {Email},
    	Address: {Address},
    	Date Ordered: {DateOrdered},
	Date Delivered: {DateDelivered},
	Date Received: {DateReceived}
";
}