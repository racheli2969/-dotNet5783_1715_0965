using BO;
using Microsoft.VisualBasic;
using PL_.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
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
    public List<PlOrderItem>? Items
    {
        get { return (List<PlOrderItem>?)GetValue(ItemsProperty); }
        set
        {
            SetValue(ItemsProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemsProperty =
      DependencyProperty.Register("Items", typeof(List<PlOrderItem>), typeof(PlCart), new PropertyMetadata(null, new PropertyChangedCallback(onItemsChanged)));
    private static void onItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlCart plCart = (PlCart)d;
        List<PlOrderItem> items = (List<PlOrderItem>)e.NewValue;
        plCart.Items = items;
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
    
}
