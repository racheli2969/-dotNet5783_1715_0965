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

public class PlCart : DependencyObject//, INotifyCollectionChanged
{
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public List<PlOrderItem>? Items
    {
        get { return (List<PlOrderItem>?)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemsProperty =
      DependencyProperty.Register("Items", typeof(List<PlOrderItem>), typeof(PlOrderItem), new PropertyMetadata(null, new PropertyChangedCallback(onItemsChanged)));

    private static void onItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PlCart plCart = (PlCart)d;
        List<PlOrderItem> items = (List<PlOrderItem>)e.NewValue;
        plCart.Items = items;
    }

    //DependencyProperty.RegisterAttached("Items", typeof(ObservableCollection<PlOrderItem>), typeof(PlCart), new UIPropertyMetadata(null, onItemsChanged));
    //public event NotifyCollectionChangedEventHandler? CollectionChanged
    //{
    //    add
    //    {
    //        ((INotifyCollectionChanged)Items).CollectionChanged += new NotifyCollectionChangedEventHandler(ItemsCollection_CollectionChanged);
    //    }

    //    remove
    //    {
    //        ((INotifyCollectionChanged)Items).CollectionChanged -= new NotifyCollectionChangedEventHandler(ItemsCollection_CollectionChanged);
    //    }
    //}

    //private static void onItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //{
    //    var cart = d as PlCart;

    //    if (e.OldValue != null)
    //    {
    //        var coll = (INotifyCollectionChanged)e.OldValue;
    //        // Unsubscribe from CollectionChanged on the old collection
    //        coll.CollectionChanged -= ItemsCollection_CollectionChanged;
    //    }

    //    if (e.NewValue != null)
    //    {
    //        var coll = (ObservableCollection<PlOrderItem>)e.NewValue;
    //        //calendar.DayTemplateSelector = new SpecialDaySelector(coll, GetSpecialDayTemplate(d));
    //        // Subscribe to CollectionChanged on the new collection
    //        coll.CollectionChanged += ItemsCollection_CollectionChanged;
    //    }
    //}
    //public static void ItemsCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    //{
    //    switch (e.Action)
    //    {
    //        case NotifyCollectionChangedAction.Add:
    //            foreach (PlOrderItem s in e.NewItems)
    //            {
    //                InternalAdd(s);
    //            }
    //            break;

    //        case NotifyCollectionChangedAction.Remove:
    //            foreach (PlOrderItem s in e.OldItems)
    //            {
    //                InternalRemove(s);
    //            }
    //            break;

    //        case NotifyCollectionChangedAction.Reset:
    //            ReadOnlyObservableCollection<PlOrderItem> col = sender as ReadOnlyObservableCollection<PlOrderItem>;
    //            InternalClearAll();
    //            if (col != null)
    //            {
    //                foreach (PlOrderItem s in col)
    //                {
    //                    InternalAdd(s);
    //                }
    //            }
    //            break;
    //    }
    //}

    //private static void InternalClearAll()
    //{
    //    PlCart plcart = new PlCart();
    //    ObservableCollection<PlOrderItem> coll = new ObservableCollection<PlOrderItem>();
    //    plcart.Items = coll;
    //}

    //private static void InternalRemove(PlOrderItem s)
    //{
    //    PlCart plcart = new PlCart();
    //    ObservableCollection<PlOrderItem> coll = new ObservableCollection<PlOrderItem>();
    //    coll.Remove(s);
    //    plcart.Items = coll;
    //}

    //private static void InternalAdd(PlOrderItem s)
    //{
    //    PlCart plcart = new PlCart();
    //    ObservableCollection<PlOrderItem> coll = new ObservableCollection<PlOrderItem>();
    //    coll.Add(s);
    //    plcart.Items = coll;
    //}
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
