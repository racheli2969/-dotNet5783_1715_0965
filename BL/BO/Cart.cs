
namespace BO;
public class Cart
{
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double FinalPrice { get; set; }
    public override string ToString() => $@"

Customer Name: {CustomerName}, 
Email: {Email},
Address: {Address},
items ordered: {Items.ToString},
Final price: {FinalPrice}
";
}

