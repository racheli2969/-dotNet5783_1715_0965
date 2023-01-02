using DalApi;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    public int Add(DO.OrderItem item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem>? GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem item)
    {
        throw new NotImplementedException();
    }
}
