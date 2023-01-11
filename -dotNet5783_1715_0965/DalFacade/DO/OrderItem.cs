namespace DO;
/// <summary>
/// defines the order item details
/// </summary>
public struct OrderItem
{
    public int Amount { get; set; }
    public int OrderID { get; set; }
    public int ItemId { get; set; }
    public int OrderItemId { get; set;}
    /// <summary>
    /// price for one item
    /// </summary>
    public double Price { get; set; }

    public override string ToString() => $@"
-------------------------------------------
 ID={OrderID}, {OrderItemId}, {ItemId}, 
 Price: {Price}
 Amount : {Amount}
-------------------------------------------
";

    public static explicit operator OrderItem(Order? v)
    {
        throw new NotImplementedException();
    }
}
