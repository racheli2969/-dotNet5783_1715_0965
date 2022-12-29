namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

internal class Item : IItem
{
    public int Add(DO.Item item)
    {
        int temp;
        XElement? root = XDocument.Load(@"..\..\..\..\xml\Item.xml").Root;
       // XmlDocument config = new XmlDocument();
        //config.Load(@"..\..\..\..\xml\Config.xml");
        XDocument doc = XDocument.Load(@"..\..\..\..\xml\Config.xml");
        string? id = doc?.XPathSelectElement("//ItemId")?.Value;
        item.ID = Convert.ToInt32(id);

        //update in the config
        id = (item.ID + 1).ToString();
        // doc?.XPathSelectElement("//ItemId").Value;
        //need to save changes to xml
        root?.Add(item);
        // root.Save(@"..\..\..\..\xml\Item.xml");
        return item.ID;
    }

    public bool Available(int id)
    {
        throw new NotImplementedException();
    }

    public bool Available(int id, int amount)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Item>? GetAll(Func<DO.Item, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(int id, int amount)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Item item)
    {
        throw new NotImplementedException();
    }
}
