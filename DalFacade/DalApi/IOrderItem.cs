
using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    IEnumerable<OrderItem>? GetAll(Func<OrderItem, bool> func);
}

