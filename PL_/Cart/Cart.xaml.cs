using PL_.PO;
using PL_.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL_.Cart;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class CartWindow : Window
{
    private ProductCatalog productCatalog { get; set; }
    private static PlCart? cartDisplayed { get; set; }
    private static BO.Cart? Cart { get; set; }
    private BlApi.IBl Bl { get; set; }
    public CartWindow(BlApi.IBl bl, ProductCatalog p, BO.Cart cart)
    {
        InitializeComponent();
        productCatalog = p;
        Bl = bl;
        cartDisplayed = PlCart.ConvertBOCArtToPlCart(cart);
        Cart = cart;
        CartItemsListView.ItemsSource = cartDisplayed.Items;
        if (cart?.Items == null || cart?.Items.Count() == 0)
        {
            CartItemsListView.Visibility = Visibility.Collapsed;
        }
        else
        {
            imgEmptyCart.Visibility = Visibility.Collapsed;
            lblForEmptyCart.Visibility = Visibility.Collapsed;
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        productCatalog.Show();
        this.Hide();
    }

    private void Add1_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PlOrderItem? obj = ((FrameworkElement)sender).DataContext as PlOrderItem;
            if (obj == null) return;
            int id = obj.ItemId;
            Cart = Bl.Cart.AddToCart(id, Cart);
            cartDisplayed = PlCart.ConvertBOCArtToPlCart(Cart);
            DataContext = cartDisplayed;
        }
        catch (BlApi.NotInStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Decrease1_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PlOrderItem? obj = ((FrameworkElement)sender).DataContext as PlOrderItem;
            if (obj == null) return;
            int id = obj.ItemId;
            Cart = Bl.Cart.UpdateProductQuantity(id, Cart,obj.Amount);
            cartDisplayed = PlCart.ConvertBOCArtToPlCart(Cart);
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btnOrderConfirmation_Click(object sender, RoutedEventArgs e)
    {

    }
    public static void GetChangesInCartFromProductItem(BO.Cart updatedCart)
    { 
        Cart=updatedCart;
        cartDisplayed=PlCart.ConvertBOCArtToPlCart(Cart);
    }
}
