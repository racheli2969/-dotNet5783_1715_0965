using DalApi;
namespace Dal;
sealed public class DalList : IDal
{
    public DalList()
    {
        if(DataSource.wasInitalized==false)
        Dal.DataSource.S_Initalize();
    }
    public IItem Item => new DalItem();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}

