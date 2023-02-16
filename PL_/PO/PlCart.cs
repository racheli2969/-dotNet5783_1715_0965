using BO;
using PL_.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_.PO;

internal class PlCart : DependencyObject
{

    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public List<PlOrderItem>? Items { get; set; }
    public double FinalPrice
    {
        get { return (double)GetValue(FinalPriceProperty); }
        set { SetValue(FinalPriceProperty, value); }
    }

    // Using a DependencyProperty as the backing store for FinalPrice.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FinalPriceProperty =
        DependencyProperty.Register("FinalPrice", typeof(double), typeof(PlCart), new PropertyMetadata(0, new PropertyChangedCallback(OnPriceChanged)));

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
    public static PlCart ConvertBOCArtToPlCart(BO.Cart cart)
    {
        PlCart plCart = new PlCart();
        plCart.CustomerName = cart.CustomerName;
        plCart.Email = cart.Email;
        plCart.Address = cart.Address;
        List<PlOrderItem> tempItems = new();
        cart?.Items?.ForEach(item => tempItems.Add(PlOrderItem.ConvertBOorderItemToPoOrderItem(item)));
        plCart.Items = tempItems;
        return plCart;
    }
}
