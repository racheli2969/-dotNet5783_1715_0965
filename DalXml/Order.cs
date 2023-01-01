using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class Order : IOrder
{
    private XElement? ordersXml = XDocument.Load(@"..\..\..\..\xml\Order.xml").Root;
    public int Add(DO.Order order)
    {
        XmlSerializer ser = new XmlSerializer(typeof(Order));
        StreamWriter w = new StreamWriter(@"..\..\..\..\xml\Order.xml");

        //the id from config can be shortened?
        XElement? configXml = XDocument.Load(@"..\..\..\..\xml\Config.xml").Root;
        string? id = (string?)(configXml?.Element("OrderId"));  
        order.OrderId = Convert.ToInt32(id);
        id = (order.OrderId + 1).ToString();
        configXml?.Element("ItemId")?.SetValue(id);
        configXml?.Save(@"..\..\..\..\xml\Config.xml"); 

        ser.Serialize(w, order);
        w.Close();
        return order.OrderId;

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order>? GetAll(Func<DO.Order, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order item)
    {
        throw new NotImplementedException();
    }
}

