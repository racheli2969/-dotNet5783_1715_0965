using BO;
using Microsoft.VisualBasic;
using PL_.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL_.PO;

internal class PlCart : DependencyObject
{
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    //  public List<PlOrderItem>? Items { get; set; }

    ///public ObservableCollection<PlOrderItem>? Items { get; set; }

    public ObservableCollection<PlOrderItem>? Items
    {
        get { return (ObservableCollection<PlOrderItem>?)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register("Items", typeof(ObservableCollection<PlOrderItem>), typeof(PlOrderItem), new PropertyMetadata(new ObservableCollection<PlOrderItem>(), new PropertyChangedCallback(onItemsChanged)));//new PropertyChangedCallback(onItemsChanged),

    private static void onItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlCart plcart = (PlCart)d;
        plcart.Items = (ObservableCollection<PlOrderItem>)e.NewValue;
    }
    public double FinalPrice
    {
        get { return (double)GetValue(FinalPriceProperty); }
        set { SetValue(FinalPriceProperty, value); }
    }

    // Using a DependencyProperty as the backing store for FinalPrice.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FinalPriceProperty =
        DependencyProperty.Register("FinalPrice", typeof(double), typeof(PlCart), new PropertyMetadata(0.0d, new PropertyChangedCallback(OnPriceChanged)));
    private static void OnPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlCart plOrder = (PlCart)d;
        plOrder.FinalPrice = (double)e.NewValue;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Final price: {0}\n Customer Name: {1}\n Address: {2}\n Email: {3}\n", FinalPrice, CustomerName, Address, Email);
        if (Items != null)
            foreach (PlOrderItem item in Items)
            {
                sb.AppendLine(item.ToString());
            }
        return sb.ToString();
    }
    //public static PlCart ConvertBOCArtToPlCart(BO.Cart cart)
    //{
    //    PlCart plCart = new PlCart();
    //    plCart.CustomerName = cart.CustomerName;
    //    plCart.Email = cart.Email;
    //    plCart.Address = cart.Address;
    //    List<PlOrderItem> tempItems = new();
    //    cart?.Items?.ForEach(item => tempItems.Add(PlOrderItem.ConvertBOorderItemToPoOrderItem(item)));
    //    plCart.Items = tempItems;
    //    return plCart;
    //}
}
