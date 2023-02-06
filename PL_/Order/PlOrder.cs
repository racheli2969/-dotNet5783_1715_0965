using BO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;   // Remember to reference WindowsBase

namespace Order;

public class PlOrder : DependencyObject
{

    /// <summary>
    /// order id
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// customer name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customer email
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// order status
    /// </summary>


    public EnumOrderStatus OrderStatus
    {
        get { return (EnumOrderStatus)GetValue(OrderStatusProperty); }
        set { SetValue(OrderStatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for OrderStatus.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrderStatusProperty =
        DependencyProperty.Register("OrderStatus", typeof(EnumOrderStatus), typeof(PlOrder), new PropertyMetadata(0, new PropertyChangedCallback(OnOrderStatusChanged)));

    private static void OnOrderStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlOrder plOrder = (PlOrder)d;
        plOrder.OrderStatus = (EnumOrderStatus)e.NewValue;
    }
    /// <summary>
    /// date of order
    /// </summary>
    public DateTime? DateOrdered { get; set; }
    /// <summary>
    /// date of delivery
    /// </summary>
    /// 
    public DateTime DateShipped
    {
        get { return (DateTime)GetValue(DateShippedProperty); }
        set { SetValue(DateShippedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DateOrdered.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DateShippedProperty =
        DependencyProperty.Register("DateShipped", typeof(DateTime), typeof(PlOrder), new PropertyMetadata(DateTime.MinValue, dateShippingChanged));

    private static void dateShippingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlOrder plOrder = (PlOrder)d;
        plOrder.DateShipped = (DateTime)e.NewValue;
        //OnOrderStatusChanged(plOrder, new DependencyPropertyChangedEventArgs (plOrder, EnumOrderStatus.Ordered, EnumOrderStatus.Delivered));
    }
    /// <summary>
    /// date received
    /// </summary>
    public DateTime DateReceived
    {
        get { return (DateTime)GetValue(DateReceivedProperty); }
        set { SetValue(DateReceivedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DateReceived.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DateReceivedProperty =
        DependencyProperty.Register("DateReceived", typeof(DateTime), typeof(PlOrder), new PropertyMetadata(DateTime.MinValue, DateDeliveryChanged));

    private static void DateDeliveryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlOrder plOrder = (PlOrder)d;
        plOrder.DateReceived = (DateTime)e.NewValue;
    }
    /// <summary>
    /// list of items of type orderItem
    /// </summary>
    public List<OrderItem>? Items { get; set; }
    /// <summary>
    /// sum of order type double
    /// </summary>
    public double SumOfOrder { get; set; }
    /// <summary>
    /// override of the class to string
    /// </summary>
    /// <returns> Order ID , Customer Name, Email,Order Status,	Date Ordered,Date Delivered, Date Received, items ordered,Sum of order</returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Order ID:{0}\n Customer Name:{1}\n Email:{2}\n Order Status:{3}\nDate Ordered:{4}\n Date Delivered{5}\nDate Received: {6}\nfinal price:{7};", OrderId, CustomerName, Email, OrderStatus, DateOrdered, DateShipped, DateReceived, SumOfOrder);
        foreach (BO.OrderItem item in Items)
        {
            sb.AppendFormat(" Order Item: {0}\n", item);
        }
        return sb.ToString();
    }
}
