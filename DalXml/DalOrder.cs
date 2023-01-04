using DalApi;
using DO;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml;
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
        configXml?.Element("OrderId")?.SetValue(id);
        configXml?.Save(@"..\..\..\..\xml\Config.xml");


        try
        {
            StreamReader r = new("..\\..\\..\\..\\xml\\Order.xml");
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);
            List<DO.Order>? lst = new();
            lst = (List<DO.Order>?)ser.Deserialize(r);
            r.Close();
            StreamWriter w = new("..\\..\\..\\..\\xml\\Order.xml");
            lst?.Add(order);
            ser.Serialize(w, lst);
            w.Close();
        }
        catch
        {
        }
        return order.OrderId;

    }

    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Order>), xRoot);
        StreamReader reader = new StreamReader(@"..\..\..\..\xml\Order.xml");
        List<DO.Order>? lst = new();
        lst = (List<DO.Order>?)serializer.Deserialize(reader);
        reader.Close();
        int idx = lst.FindIndex(s => s.OrderId == id);
        if (idx == -1)
            throw new DalApi.EntityNotFoundException();
        lst.RemoveAt(idx);
        StreamWriter writer = new StreamWriter(@"..\..\..\..\xml\Order.xml");
        serializer.Serialize(writer, lst);
        writer.Close();
    }

    public IEnumerable<DO.Order>? GetAll(Func<DO.Order, bool>? func = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Order>), xRoot);
        StreamReader reader = new StreamReader(@"..\..\..\..\xml\Order.xml");
        List<DO.Order> lst = (List<DO.Order>?)serializer.Deserialize(reader);
        reader.Close();
        if (func == null)
        {
            return lst;
        }
        List<DO.Order> l = (List<DO.Order>)lst.Where(func);
        return l;
    }

    public void Update(DO.Order order)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Order>));
        StreamReader reader = new StreamReader(@"..\..\..\..\xml\Order.xml");
        List<DO.Order> lst = (List<DO.Order>?)serializer.Deserialize(reader);
        List<DO.Order> l = (List<DO.Order>)lst.Where(o => order.OrderId == o.OrderId);
        if (l.Count() > 0)
        {
            int i = lst.FindIndex(o => order.OrderId == o.OrderId);
            DO.Order o = l[0];
            o.Address = order.Address;
            o.Email = order.Email;
            o.OrderId = order.OrderId;
            o.CustomerName = order.CustomerName;
            o.DateDelivered = order.DateOrdered;
            o.DateOrdered = order.DateOrdered;
            o.DateReceived = order.DateReceived;
            lst[i] = o;
            StreamWriter writer = new StreamWriter(@"..\..\..\..\xml\Order.xml");
            serializer.Serialize(writer, lst);
            writer.Close();
            reader.Close();
        }
        throw new NotImplementedException(); throw new NotImplementedException();
    }
}

