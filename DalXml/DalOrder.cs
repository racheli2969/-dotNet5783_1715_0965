using DalApi;
using DO;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
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
        XmlSerializer serializer = new XmlSerializer(typeof(DO.Order));
        StreamReader reader = new StreamReader(@"..\..\..\..\xml\Order.xml");
        List<DO.Order>lst = (List<DO.Order>?)serializer.Deserialize(reader);
        List<DO.Order> l = (List<DO.Order>)lst.Where(s => s.OrderId == id);
        reader.Close();
        if (l.Count==0)
            throw new NotImplementedException();
        DO.Order o = l[0];
        lst.Remove(o);
        StreamWriter writer = new StreamWriter(@"..\..\..\..\xml\Order.xml");
        serializer.Serialize(writer,lst);
        writer.Close();
    }

    public IEnumerable<DO.Order>? GetAll(Func<DO.Order, bool>? func = null)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DO.Order));
        StreamReader reader = new StreamReader(@"..\..\..\..\xml\Order.xml");
        List<DO.Order> lst = (List<DO.Order>?)serializer.Deserialize(reader);
        if (func == null)
        {
            return lst;
        }
        List<DO.Order> l = (List<DO.Order>)lst.Where(func);
        return l;
    }

    public void Update(DO.Order item)
    {
        throw new NotImplementedException();
    }
}

