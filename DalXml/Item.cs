namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class Item : IItem
{
    public int Add(DO.Item item)
    {
        int temp;
        XElement? root = XDocument.Load(@"..\..\..\..\xml\Item.xml").Root;
        XElement? config = XDocument.Load(@"..\..\..\..\xml\Config.xml")?.Root;
        //הגישה לא עובדת צריך לבדוק
        string? id = config?.Element("ItemId")?.Value;
        item.ID = Convert.ToInt32(id);
        id = (item.ID + 1).ToString();
        config?.SetAttributeValue("ItemId", id);
        root?.Add(item);
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
