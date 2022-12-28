namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Item : IItem
{
    
    public int Add(DO.Item item)
    {
        XElement? root = XDocument.Load("..\\..\\Item.xml").Root;
        return 1;
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
