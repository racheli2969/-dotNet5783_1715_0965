
namespace BO;
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int ItemId { get; set; }
    public string ?ItemName { get; set; }
    public double ItemPrice { get; set; }
    public int Amount { get; set; }
    public double PriceOfItems { get; set; }

    public override string ToString() => $@"

 ID of order item={OrderItemId},
item id:{ItemId}, 
Customer Name: {ItemName}, 
Ptice per item: {ItemPrice},
amount: {Amount},
price all together: {PriceOfItems}
";


}
