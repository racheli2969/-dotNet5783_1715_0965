
namespace DO;
/// <summary>
/// defines the item details
/// </summary>
public struct OrderItem
{
    public int Amount { get; set; }
    public int ID { get; set; }
    public int ItemId { get; set; }
    public int OrderItemId { get; set;}
    public double Price { get; set; }

    public override string ToString() => $@"
 ID={ID}, {OrderItemId}, {ItemId}, 
    	Price: {Price}
    	Amount : {Amount}
";

}
