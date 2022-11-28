
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public IEnumerable<OrderItem> GetByOrderId(int orderId);
    public OrderItem GetById(int orderId, int productId);
}

