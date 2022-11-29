using Dal;
namespace DO;
/// <summary>
/// defines the item details
/// </summary>
public struct Item
{
    public string? Name { get; set; }
    public BookGenre Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
    public int ID { get; set; }

    public override string ToString() => $@"
Product ID={ID}: {Name}, 
category - {Category}
    	Price: {Price}
    	 In stock: {InStock}
";
}
