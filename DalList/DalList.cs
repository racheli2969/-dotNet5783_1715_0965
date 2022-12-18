using DalApi;
namespace Dal;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList()
    {
        if(DataSource.wasInitalized==false)
        Dal.DataSource.S_Initalize();
    }
    public IItem Item => new DalItem();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}

