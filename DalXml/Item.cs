namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

internal class Item : IItem
{
    //another way to access config
    //  XDocument doc = XDocument.Load(@"..\..\..\..\xml\Config.xml");
    //string? id = doc?.XPathSelectElement("//ItemId")?.Value;
    private XElement? itemsXml = XDocument.Load(@"..\..\..\..\xml\Item.xml").Root;
    public int Add(DO.Item item)
    {
        XElement? configXml = XDocument.Load(@"..\..\..\..\xml\Config.xml").Root;
        string? id = (string?)(configXml?.Element("ItemId"));
        item.ID = Convert.ToInt32(id);
        //update in the config
        id = (item.ID + 1).ToString();
        configXml?.Element("ItemId")?.SetValue(id);
        configXml?.Save(@"..\..\..\..\xml\Config.xml");
        XElement newItem = new("Item");
        newItem.Add(new XElement("Id", item.ID));
        newItem.Add(new XElement("Name", item.Name));
        newItem.Add(new XElement("Category", item.Category));
        newItem.Add(new XElement("Price", item.Price));
        newItem.Add(new XElement("AmountInStock", item.AmountInStock));
        itemsXml?.Add(newItem);
        itemsXml?.Save(@"..\..\..\..\xml\Item.xml");
        return item.ID;
    }

    public bool Available(int id)
    {
        // Item? item = new Item();
        XElement? item = (itemsXml?.Elements("Item")?.
                      Where(s => (id.ToString().CompareTo(s.Element("Id").Value.ToString()) == 0))
                      .Elements("AmountInStock").FirstOrDefault());
        int t = Convert.ToInt32((string?)item?.Value.ToString());
        return t - 1 >= 0;
    }

    public bool Available(int id, int amount)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                      Where(s => (id.ToString().CompareTo(s.Element("Id").ToString()) == 0))
                      .Elements("AmountInStock").FirstOrDefault());
        int t = Convert.ToInt32((string?)item?.Value.ToString());
        return t - amount >= 0;
    }

    public void Delete(int id)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                       Where(s => (id.ToString().CompareTo(s.Element("Id")?.ToString()) == 0))
                     .FirstOrDefault());
        item?.Remove();
    }

    public IEnumerable<DO.Item>? GetAll(Func<XElement, bool>? func = null)
    {
        List<Item?>? items;
        if(func==null)
        items = (itemsXml?.Elements("Item")) as List<Item?>;
        else
       
            //ParallelQuery<Item> items1=items.
            //ParallelEnumerable.Where<Item>(ParallelQuery<Item>, Func<Item, bool>)//ParallelQuery<Item>'   
           // items = items.Where<Item>(func);
        items = (itemsXml?.Elements("Item")?.Where(func)) as List<Item?>;
        if (items == null)
            throw new EntityNotFoundException();
        return (IEnumerable<DO.Item>?)items;
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
