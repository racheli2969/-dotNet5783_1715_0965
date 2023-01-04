using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    public int Add(DO.OrderItem orderItem)
    {
        //
        XElement? configXml = XDocument.Load(@"..\..\..\..\xml\Config.xml").Root;
        string? id = (string?)(configXml?.Element("OrderItemId"));
        orderItem.OrderItemId = Convert.ToInt32(id);
        id = (orderItem.OrderItemId + 1).ToString();
        configXml?.Element("OrderItemId")?.SetValue(id);
        configXml?.Save(@"..\..\..\..\xml\Config.xml");
        StreamReader? reader = new(@"..\..\..\..\xml\OrderItem.xml");


        return 1;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem>? GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem item)
    {
        throw new NotImplementedException();
    }
}
