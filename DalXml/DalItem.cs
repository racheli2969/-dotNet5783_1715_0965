namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

internal class DalItem : IItem
{
    private XElement? itemsXml = XDocument.Load(@"..\..\..\..\xml\Item.xml").Root;
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Item item)
    {
        XElement? configXml = XDocument.Load(@"..\..\..\..\xml\Config.xml").Root;
        string? id = (string?)(configXml?.Element("ItemId"));
        item.ID = Convert.ToInt32(id);
        //update in the config
        id = (item.ID + 1).ToString();
        configXml?.Element("ItemId")?.SetValue(id);
        configXml?.Save(@"..\..\..\..\xml\Config.xml");
        XElement newItem = new("Item",
                               new XElement("Id", item.ID),
                               new XElement("Name", item.Name),
                               new XElement("Category", item.Category),
                               new XElement("Price", item.Price),
                               new XElement("AmountInStock", item.AmountInStock)
                               );
        itemsXml?.Add(newItem);
        itemsXml?.Save(@"..\..\..\..\xml\Item.xml");
        return item.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool Available(int id)
    {
        // Item? item = new Item();
        XElement? item = (itemsXml?.Elements("Item")?.
                      Where(s => (id.ToString().CompareTo(s.Element("Id")?.Value.ToString()) == 0))
                      .Elements("AmountInStock").FirstOrDefault());
        int t = Convert.ToInt32((string?)item?.Value.ToString());
        return t - 1 >= 0;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool Available(int id, int amount)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                      Where(s => (id.ToString().CompareTo(s?.Element("Id")?.ToString()) == 0))
                      .Elements("AmountInStock").FirstOrDefault());
        int t = Convert.ToInt32((string?)item?.Value.ToString());
        return t - amount >= 0;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                       Where(s => (id.ToString().CompareTo(s.Element("Id")?.ToString()) == 0))
                     .FirstOrDefault());
        item?.Remove();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Item>? GetAll(Func<DO.Item, bool>? func = null)
    {

        List<XElement>? itemsXelement;
        List<DO.Item?>? items = new();
        itemsXelement = ((itemsXml?.Elements("Item"))?.ToList());
        if (itemsXelement == null)
            throw new EntityNotFoundException();
        DO.Item item;
        for (int i = 0; i < itemsXelement?.Count; i++)
        {
            item = new();
            item.ID = int.Parse(itemsXelement.ElementAt(i).Element("Id")?.Value ?? 0.ToString());
            item.Name = itemsXelement.ElementAt(i).Element("Name")?.Value.ToString();
            DO.BookGenre bookGenre = new DO.BookGenre();
            Enum.TryParse(itemsXelement.ElementAt(i).Element("Category")?.Value.ToString(), out bookGenre);
            item.Category = bookGenre;
            item.Price = double.Parse(itemsXelement.ElementAt(i)?.Element("Price")?.Value ?? 0.ToString());
            item.AmountInStock = int.Parse(itemsXelement.ElementAt(i).Element("AmountInStock")?.Value ?? 0.ToString());
            items.Add(item);
        }
        if (func != null)
            items = items.Where(item => item.HasValue && func((DO.Item)item)).ToList();
        return (IEnumerable<DO.Item>?)items.Cast<DO.Item>();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int id, int amount)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                       Where(s => (id.ToString().CompareTo(s.Element("Id")?.Value.ToString()) == 0))
                     .FirstOrDefault());
        int a = Convert.ToInt32(item?.Element("AmountInStock")?.Value.ToString()) - amount;
        item?.Element("Amount")?.SetValue(a);
        itemsXml?.Save(@"..\..\..\..\xml\Item.xml");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Item itemToUpdate)
    {
        XElement? item = (itemsXml?.Elements("Item")?.
                       Where(s => ((itemToUpdate.ID).ToString().CompareTo(s.Element("Id")?.Value.ToString()) == 0))
                     .FirstOrDefault());
        item?.Element("Name")?.SetValue(itemToUpdate.Name ?? "");
        item?.Element("Category")?.SetValue(itemToUpdate.Category);
        item?.Element("Price")?.SetValue(itemToUpdate.Price);
        item?.Element("AmountInStock")?.SetValue(itemToUpdate.AmountInStock);
        itemsXml?.Save(@"..\..\..\..\xml\Item.xml");
    }
}
