using DalApi;
namespace Dal;
sealed public class DalList : IDal
{
    public DalList()
    {
        Dal.DataSource.S_Initalize();
    }
    public IItem Item => new DalItem();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}

