
using DO;
using System.Text;

namespace BO;
public class Cart
{
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public int NumOfHouse { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double FinalPrice { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Final price: {0}\n Customer Name: {1}\n Address: {2} {3},{4}\n Email: {5}\n", FinalPrice, CustomerName, Street, NumOfHouse, City, Email);
        if (Items != null)
            foreach (BO.OrderItem item in Items)
            {
                //sb.AppendFormat(" Order Item: {0}\n", item);
                sb.AppendLine(item.ToString());
            }
        return sb.ToString();
    }

}

