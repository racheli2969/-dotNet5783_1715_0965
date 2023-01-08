namespace Dal;
using DalApi;
sealed public class DalXml : IDal
{
    public IItem Item { get; } = new Dal.DalItem();

    public IOrder Order { get; } = new Dal.DalOrder();

    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();
}