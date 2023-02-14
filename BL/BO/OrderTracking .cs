using DO;
using System.Text;

namespace BO;

public class OrderTracking
{
    public int Id { get; set; }
    public EnumOrderStatus? OrderStatus { get; set; }
    public List<(DateTime?, EnumOrderStatus)>? TrackingTuples { get; set; }
    //    public override string ToString() => $@"
    //   ID: {Id},
    //   Order Status: {OrderStatus},
    //   Tracking: {TrackingTuples}
    //";

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Order ID:{0}\n Current Status:{1}\nTracking:\n", Id, OrderStatus);
        if (TrackingTuples != null)
            foreach ((DateTime?, EnumOrderStatus) item in TrackingTuples)
            {
                sb.AppendFormat(" Date: {0}, Status{1}: \n", item.Item1, item.Item2);
            }
        return sb.ToString();
    }

}

