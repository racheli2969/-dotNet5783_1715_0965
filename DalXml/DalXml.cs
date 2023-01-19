namespace Dal;
using DalApi;
using System.Diagnostics;

sealed public class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml()
    { }
    public IItem Item { get; } = new Dal.DalItem();

    public IOrder Order { get; } = new Dal.DalOrder();

    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();
}