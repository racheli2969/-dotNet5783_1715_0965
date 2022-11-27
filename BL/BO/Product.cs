
namespace BO;
public class Product
{
    public int ID { get; set; }
    public string ?Name { get; set; }
    public double Price { get; set; }
    public int Category { get; set; }
    public int AmountInStock { get; set; }
    

    public override string ToString() => $@"
Product ID={ID}: {Name}, 
category - {Category}
    	Price: {Price}
    	Amount in stock: {AmountInStock}
";

}
