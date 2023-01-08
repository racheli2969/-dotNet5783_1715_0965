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
    public int Add(DO.Order order)
    {
        XElement? configXml = XDocument.Load(@"..\..\..\..\xml\Config.xml").Root;
        string? id = (string?)(configXml?.Element("OrderId"));
        order.OrderId = Convert.ToInt32(id);
        id = (order.OrderId + 1).ToString();
        configXml?.Element("OrderId")?.SetValue(id);
        configXml?.Save(@"..\..\..\..\xml\Config.xml");

        StreamReader r = new("..\\..\\..\\..\\xml\\Order.xml");
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>), xRoot);
        List<DO.Order>? lst;
        lst = (List<DO.Order>?)ser.Deserialize(r);
        r.Close();
        StreamWriter w = new("..\\..\\..\\..\\xml\\Order.xml");
        lst?.Add(order);
        ser.Serialize(w, lst);
        w.Close();

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
        List<DO.Order>? lst = (List<DO.Order>?)serializer.Deserialize(reader);
        reader.Close();
        if (func == null)
        {
            return lst;
        }
        List<DO.Order>? l = (List<DO.Order>?)lst?.Where(func).ToList();
        return l;
    }

    public void Update(DO.Order order)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Order>), xRoot);
        StreamReader reader = new StreamReader(@"..\..\..\..\xml\Order.xml");
        List<DO.Order>? lst = (List<DO.Order>?)serializer.Deserialize(reader);
        int i = lst.FindIndex(o => order.OrderId == o.OrderId);
        if (i == -1)
            throw new DalApi.EntityNotFoundException();
        lst[i] = order;
        StreamWriter writer = new StreamWriter(@"..\..\..\..\xml\Order.xml");
        serializer.Serialize(writer, lst);
        writer.Close();
        reader.Close();
    }
}

