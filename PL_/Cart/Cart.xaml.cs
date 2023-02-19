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
        CartItemsListView.DataContext = CartDisplayed.Items;
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
            int? idx = CartDisplayed?.Items?.FindIndex(i => i.ItemId == id);
            (CartDisplayed?.Items ?? throw new BlApi.BlNOtImplementedException())[idx ?? 0].Amount++;
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
            int? idx = CartDisplayed?.Items?.FindIndex(i => i.ItemId == id);
            CartDisplayed?.Items?.RemoveAt(idx ?? 0);
            cart = Bl.Cart.UpdateProductQuantity(id, cart ?? throw new BlApi.BlNOtImplementedException(), 0);
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
            int amount = obj.Amount - 1;
            cart = Bl.Cart.UpdateProductQuantity(id, cart ?? throw new BlApi.BlNOtImplementedException(), amount);
            int? idx = CartDisplayed?.Items?.FindIndex(i => i.ItemId == id);
            if (amount > 0)
            {
                (CartDisplayed?.Items ?? throw new BlApi.BlNOtImplementedException())[idx ?? 0].Amount--;
            }
            else
            {
                CartDisplayed?.Items?.RemoveAt(idx ?? 0);
            }
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btnOrderConfirmation_Click(object sender, RoutedEventArgs e)
    {
        if (cart != null)
        {
            cart.CustomerName = txtCustomerName.Text;
            cart.Email = txtEmail.Text;
            cart.Street = txtStreet.Text;
            cart.City= txtCity.Text;    
            cart.NumOfHouse = int.Parse(txtNumHOuse.Text);
            Bl.Cart.OrderConfirmation(cart);
        }     
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
            CartItemsListView.ItemsSource = CartDisplayed.Items;
        }
    }

    private static PlCart ConvertBOCArtToPlCart(BO.Cart cart)
    {
        PlCart plCart = new PlCart();
        //plCart.CustomerName = cart.CustomerName;
        //plCart.Email = cart.Email;
        //plCart.Address = cart.City;
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
