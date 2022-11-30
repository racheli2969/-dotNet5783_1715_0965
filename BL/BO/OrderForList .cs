using BL;
namespace BO;

public class OrderForList
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public EnumOrderStatus OrderStatus { get; set; }
    public int NumOfItems { get; set; }
    public double Price { get; set; }
    public override string ToString() => $@"
 ID={Id}, Customer Name: {CustomerName}, 
    	Order Status: {OrderStatus},
Price: {Price}
";

}

