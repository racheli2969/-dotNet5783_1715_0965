using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_.PO;

internal class PlProductItem : DependencyObject
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public BO.BookGenre Category { get; set; }
    public bool IsAvailable
    {
        get { return (bool)GetValue(IsAvailableProperty); }
        set { SetValue(IsAvailableProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsAvailable.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsAvailableProperty =
        DependencyProperty.Register("IsAvailable", typeof(bool), typeof(PlProductItem), new PropertyMetadata(true, new PropertyChangedCallback(onAvailabilityChanged)));

    private static void onAvailabilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlProductItem plProductItem = (PlProductItem)d;
        plProductItem.IsAvailable = (bool)e.NewValue;
    }

    public int Amount
    {
        get { return (int)GetValue(AmountProperty); }
        set { SetValue(AmountProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Amount.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AmountProperty =
        DependencyProperty.Register("Amount", typeof(int), typeof(PlProductItem), new PropertyMetadata(0, new PropertyChangedCallback(onAmountChanged)));

    private static void onAmountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlProductItem plProductItem = (PlProductItem)d;
        plProductItem.Amount = (int)e.NewValue;
    }

    public static PlProductItem ConvertProductItemFromBOToPo(BO.ProductItem productItem)
    {
        PlProductItem plProductItem = new PlProductItem();
        plProductItem.ID = productItem.ID;
        plProductItem.Name = productItem.Name;
        plProductItem.Price = productItem.Price;
        plProductItem.Category = productItem.Category;
        plProductItem.IsAvailable = productItem.IsAvailable;
        plProductItem.Amount = productItem.Amount;
        return plProductItem;
    }

    public override string ToString() => $@"
Product ID={ID},
Name: {Name}, 
Price: {Price},
category - {Category},
Available: {IsAvailable},
Amount in cart: {Amount}
";
}
