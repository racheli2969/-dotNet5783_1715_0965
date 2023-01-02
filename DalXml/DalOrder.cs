using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class DalOrder : IOrder
{
    //private XElement? ordersXml = XDocument.Load(@"..\..\..\..\xml\Order.xml").Root;
    public int Add(DO.Order order)
    {
        //the id from config can be shortened?
        XElement? configXml = XDocument.Load(@"..\..\..\..\xml\Config.xml").Root;
        string? id = (string?)(configXml?.Element("OrderId"));
        order.OrderId = Convert.ToInt32(id);
        id = (order.OrderId + 1).ToString();
        configXml?.Element("ItemId")?.SetValue(id);
        configXml?.Save(@"..\..\..\..\xml\Config.xml");

        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "orders";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);

            StreamReader? r = new(@"..\..\..\..\xml\Order.xml");
            //r.Read();
            XmlSerializer? serList = new(typeof(List<DO.Order>));
            List<DO.Order>? lst = (List<DO.Order>?)ser.Deserialize(r);
           // var lst = serList.Deserialize(r);
           lst?.Add(order);
            r.Close();
            StreamWriter w = new StreamWriter(@"..\..\..\..\xml\Order.xml");
           // serList.Serialize(w, lst);
            w.Close();
            return order.OrderId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.GetBaseException());
        }
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

