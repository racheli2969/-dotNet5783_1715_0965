using Dal;
namespace BO;

public class ProductForList
{
    public int ItemId { get; set; }
    public string? ItemName { get; set; }
    public double ItemPrice { get; set; }
    public BookGenre? Category { get; set; }

    public override string ToString() => $@"

item id:{ItemId}, 
Item Name: {ItemName}, 
Ptice per item: {ItemPrice}, 
Category: {Category}
";

}

