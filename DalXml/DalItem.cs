namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class DalItem : IItem
{
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
                      Where(s => (id.ToString().CompareTo(s.Element("Id")?.Value.ToString()) == 0))
                      .Elements("AmountInStock").FirstOrDefault());
        int t = Convert.ToInt32((string?)item?.Value.ToString());
        return t - 1 >= 0;
    }

    public bool Available(int id, int amount)
    {
        //from v in itemsXml?.Elements("Item")
        //where id.ToString().CompareTo(v.Element("Id").Value.ToString())//(s => (id.ToString().CompareTo(s.Element("Id").ToString()) == 0))
        //select v.Elements("AmountInStock").FirstOrDefault();
        XElement? item = (itemsXml?.Elements("Item")?.
                      Where(s => (id.ToString().CompareTo(s?.Element("Id")?.ToString()) == 0))
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
        List<DalItem?>? items;
        if(func==null)
        items = (itemsXml?.Elements("Item")) as List<DalItem?>;
        else
        items = (itemsXml?.Elements("Item")?.Where(func)) as List<DalItem?>;
        if (items == null)
            throw new EntityNotFoundException();
        return (IEnumerable<DO.Item>?)items;
    }

    public IEnumerable<DO.Item>? GetAll(Func<DO.Item, bool>? func = null)
    {
        List < DalItem? >? items=new();
        return (IEnumerable<DO.Item>?)items;
    }

    public void Update(int id, int amount)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                       Where(s => (id.ToString().CompareTo(s.Element("Id")?.Value.ToString()) == 0))
                     .FirstOrDefault());
        int a =Convert.ToInt32( item?.Element("AmountInStock")?.Value.ToString()) - amount;
        item?.Element("Amount")?.SetValue(a);
        itemsXml?.Save(@"..\..\..\..\xml\Item.xml");
    }

    public void Update(DO.Item itemToUpdate)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                       Where(s => ((itemToUpdate.ID).ToString().CompareTo(s.Element("Id")?.Value.ToString()) == 0))
                     .FirstOrDefault());
        item?.Element("Name")?.SetValue(itemToUpdate.Name);
        item?.Element("Category")?.SetValue(itemToUpdate.Category);
        item?.Element("Price")?.SetValue(itemToUpdate.Price);
        item?.Element("AmountInStock")?.SetValue(itemToUpdate.AmountInStock);
        itemsXml?.Save(@"..\..\..\..\xml\Item.xml");
    }
}
