using BO;
using PL_.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_.PO;

internal class PlOrderItem : DependencyObject
{
    public int OrderItemId { get; set; }
    public int ItemId { get; set; }
    public string? ItemName { get; set; }
    public double ItemPrice { get; set; }

    public int Amount
    {
        get { return (int)GetValue(AmountProperty); }
        set { SetValue(AmountProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Amount.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AmountProperty =
        DependencyProperty.Register("Amount", typeof(int), typeof(PlOrderItem), new PropertyMetadata(0, new PropertyChangedCallback(onAmountChanged)));

    private static void onAmountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlOrderItem pl = (PlOrderItem)d;
        pl.Amount = (int)e.NewValue;
    }


    public double PriceOfItems
    {
        get { return (double)GetValue(PriceOfItemsProperty); }
        set { SetValue(PriceOfItemsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for PriceOfItems.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PriceOfItemsProperty =
        DependencyProperty.Register("PriceOfItems", typeof(double), typeof(PlOrderItem), new PropertyMetadata(0.0, new PropertyChangedCallback(onPriceChanged)));

    private static void onPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlOrderItem pl = (PlOrderItem)d;
        pl.PriceOfItems = (double)e.NewValue;
    }
    public static PlOrderItem ConvertBOorderItemToPoOrderItem(OrderItem orderItem)
    {
        PlOrderItem plOrderItem = new PlOrderItem();
        plOrderItem.OrderItemId = orderItem.OrderItemId;
        plOrderItem.ItemId = orderItem.ItemId;
        plOrderItem.ItemName = orderItem.ItemName;
        plOrderItem.ItemPrice = orderItem.ItemPrice;
        plOrderItem.Amount = orderItem.Amount;
        plOrderItem.PriceOfItems = orderItem.PriceOfItems;
        return plOrderItem;
    }

    public override string ToString() => $@"ID of order item: {OrderItemId}, item id: {ItemId}, Customer Name: {ItemName}, Price per item: {ItemPrice}, amount: {Amount}, price all together: {PriceOfItems}";
}
