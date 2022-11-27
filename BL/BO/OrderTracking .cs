using Dal;
namespace BO;

public class OrderTracking
{
    public int Id { get; set; }
    public EnumOrderStatus OrderStatus { get; set; }
    public List<(DateTime,EnumOrderStatus)>? TrackingTuples { get; set; }                    
    public override string ToString() => $@"

 ID={Id},
    	Order Status: {OrderStatus},
Tracking: {TrackingTuples}

";
   
}

