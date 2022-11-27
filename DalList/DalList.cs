
using DalApi;

namespace Dal;

sealed public class DalList : IDal
{
    public IItem Item => new DalItem();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}

