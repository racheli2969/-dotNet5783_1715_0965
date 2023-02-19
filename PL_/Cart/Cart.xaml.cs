using DO;
using PL_.PO;
using PL_.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    private PlCart? CartDisplayed { get; set; }
    private BO.Cart? cart { get; set; }
   // public ObservableCollection<PlOrderItem>? items { get; set; }=new();
    private BlApi.IBl Bl { get; set; }
    public CartWindow(BlApi.IBl bl, ProductCatalog p, BO.Cart cart)
    {
        InitializeComponent();
        productCatalog = p;
        Bl = bl;
       // CartDisplayed = new();
        CartDisplayed = ConvertBOCArtToPlCart(cart);
        DataContext = CartDisplayed;
        this.cart = cart;
        this.DataContext = CartDisplayed;
       CartItemsListView.ItemsSource = CartDisplayed.Items;
        //CartItemsListView.DataContext = CartDisplayed.Items;
        // CartItemsListView.ItemsSource = null;
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
            cart = Bl.Cart.AddToCart(id, cart ?? throw new BlApi.BlNOtImplementedException());
            CartDisplayed = ConvertBOCArtToPlCart(cart);
        }
        catch (BlApi.NotInStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PlOrderItem? obj = ((FrameworkElement)sender).DataContext as PlOrderItem;
            if (obj == null) return;
            int id = obj.ItemId;
            cart = Bl.Cart.UpdateProductQuantity(id, cart ?? throw new BlApi.BlNOtImplementedException(), 0);
            CartDisplayed = ConvertBOCArtToPlCart(cart);
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Decrease1_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PlOrderItem? obj = ((FrameworkElement)sender).DataContext as PlOrderItem;
            if (obj == null) return;
            int id = obj.ItemId;
            cart = Bl.Cart.UpdateProductQuantity(id, cart ?? throw new BlApi.BlNOtImplementedException(), obj.Amount);
            CartDisplayed = ConvertBOCArtToPlCart(cart);
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btnOrderConfirmation_Click(object sender, RoutedEventArgs e)
    {
        //get all fields of the cart then send to bl to confirm
    }
    public void getUpdateForCAart(BO.Cart cart)
    {
        this.cart = cart;
        this.CartDisplayed = ConvertBOCArtToPlCart(cart);
        MessageBox.Show(CartDisplayed?.ToString());
        if (this.CartDisplayed?.Items?.Count() > 0)
        {
            CartItemsListView.Visibility = Visibility.Visible;
            imgEmptyCart.Visibility = Visibility.Collapsed;
            lblForEmptyCart.Visibility = Visibility.Collapsed;
            CartItemsListView.DataContext = CartDisplayed.Items;
            MessageBox.Show(CartItemsListView.Items[0].ToString());
        }
    }

    private static PlCart ConvertBOCArtToPlCart(BO.Cart cart)
    {
        PlCart plCart = new PlCart();
        plCart.CustomerName = cart.CustomerName;
        plCart.Email = cart.Email;
        plCart.Address = cart.Address;
        plCart.FinalPrice = cart.FinalPrice;
        List<PlOrderItem> tempItems = new();
        cart?.Items?.ForEach(item => tempItems.Add(PlOrderItem.ConvertBOorderItemToPoOrderItem(item)));
        plCart.Items = tempItems;
        return plCart;
    }

    private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            (cart ?? throw new BlApi.BlNOtImplementedException()).Items = new List<BO.OrderItem>();
            (CartDisplayed ?? throw new BlApi.BlNOtImplementedException()).Items = new List<PlOrderItem>();
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
