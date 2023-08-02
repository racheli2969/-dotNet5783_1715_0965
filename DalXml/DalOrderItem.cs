using DalApi;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem orderItem)
    {
        //the id from config can be shortened?
        XElement? configXml = XDocument.Load(@"..\..\xml\Config.xml").Root;//@"..\..\..\..\xml\Config.xml"
        string? id = (string?)(configXml?.Element("OrderId"));
        orderItem.OrderItemId = Convert.ToInt32(id);
        id = (orderItem.OrderItemId + 1).ToString();
        configXml?.Element("OrderItemId")?.SetValue(id);
        configXml?.Save(@"..\..\xml\Config.xml");//@"..\..\..\..\xml\Config.xml"
        //serialize the new obj
        StreamReader r = new("..\\..\\xml\\OrderItem.xml");//@"..\\..\\..\\..\\xml\\OrderItem.xml"
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot);
        List<DO.OrderItem>? lst;
        lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        r.Close();
        StreamWriter w = new("..\\..\\xml\\OrderItem.xml");//@"..\\..\\..\\..\\xml\\OrderItem.xml"
        lst?.Add(orderItem);
        ser.Serialize(w, lst);
        w.Close();
        return orderItem.OrderItemId;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot);
        StreamReader reader = new StreamReader(@"..\..\xml\OrderItem.xml");//@"..\..\..\..\xml\OrderItem.xml"
        List<DO.OrderItem>? lst = new();
        lst = (List<DO.OrderItem>?)serializer.Deserialize(reader);
        reader.Close();
        int idx = lst.FindIndex(s => s.OrderItemId == id);
        if (idx == -1)
            throw new DalApi.EntityNotFoundException();
        lst.RemoveAt(idx);
        StreamWriter writer = new StreamWriter(@"..\..\xml\OrderItem.xml");//@"..\..\..\..\xml\OrderItem.xml"
        serializer.Serialize(writer, lst);
        writer.Close();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem>? GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot);
        StreamReader reader = new StreamReader(@"..\..\xml\OrderItem.xml");//@"..\..\..\..\xml\OrderItem.xml"
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)serializer.Deserialize(reader);
        reader.Close();
        if (func == null)
        {
            return lst;
        }
        List<DO.OrderItem>? l = lst?.Where(func).ToList();
        return l;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem orderItem)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot);
        StreamReader reader = new StreamReader(@"..\..\xml\OrderItem.xml");//@"..\..\..\..\xml\OrderItem.xml"
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)serializer.Deserialize(reader);
        int i = lst.FindIndex(o => orderItem.OrderItemId == o.OrderItemId);
        if (i == -1)
            throw new DalApi.EntityNotFoundException();
        lst[i] = orderItem;
        StreamWriter writer = new StreamWriter(@"..\..\xml\OrderItem.xml");//@"..\..\..\..\xml\OrderItem.xml"
        serializer.Serialize(writer, lst);
        writer.Close();
        reader.Close();
    }
}
