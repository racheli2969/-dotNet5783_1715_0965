using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Order;

public class PlOrderForList:DependencyObject
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public EnumOrderStatus? OrderStatus
    {
        get { return (EnumOrderStatus)GetValue(OrderStatusProperty); }
        set { SetValue(OrderStatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for OrderStatus.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrderStatusProperty =
        DependencyProperty.Register("OrderStatus", typeof(EnumOrderStatus), typeof(PlOrderForList), new PropertyMetadata(EnumOrderStatus.Ordered, new PropertyChangedCallback(OnOrderStatusChanged)));

    private static void OnOrderStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlOrderForList plOrder = (PlOrderForList)d;
        plOrder.OrderStatus = (EnumOrderStatus)e.NewValue;
    }
    public int NumOfItems { get; set; }
    public double Price { get; set; }
    public override string ToString() => $@"
    ID: {Id},
    Customer Name: {CustomerName}, 
    Order Status: {OrderStatus},
    Numbers of items: {NumOfItems},
    Price: {Price}
";
}
