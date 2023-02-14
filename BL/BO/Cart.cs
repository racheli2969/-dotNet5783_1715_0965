
using DO;
using System.Text;

namespace BO;
public class Cart
{
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double FinalPrice { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Final price: {0}\n Customer Name: {1}\n Address: {2}\n Email: {3}\n", FinalPrice, CustomerName, Address, Email);
        if (Items != null)
            foreach (BO.OrderItem item in Items)
            {
                //sb.AppendFormat(" Order Item: {0}\n", item);
                sb.AppendLine(item.ToString());
            }
        return sb.ToString();
    }

}

