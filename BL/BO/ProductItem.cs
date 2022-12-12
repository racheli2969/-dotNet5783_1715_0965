using BL;

namespace BO;
public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public BookGenre? Category { get; set; }
    public bool IsAvailable { get; set; }
    public int Amount { get; set; }


    public override string ToString() => $@"
---------------------------
Product ID={ID},
Name: {Name}, 
Price: {Price},
category - {Category},
Available: {IsAvailable},
Amount in cart: {Amount}
---------------------------
";


}
