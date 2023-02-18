﻿using PL_.PO;
using PL_.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private BlApi.IBl Bl { get; set; }
    public CartWindow(BlApi.IBl bl, ProductCatalog p, BO.Cart cart)
    {
        InitializeComponent();
        productCatalog = p;
        Bl = bl;
        CartDisplayed = ConvertBOCArtToPlCart(cart);
        this.cart = cart;
        DataContext = CartDisplayed;
        CartItemsListView.DataContext = CartDisplayed.Items;
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
            cart = Bl.Cart.AddToCart(id, cart);
            CartDisplayed = ConvertBOCArtToPlCart(cart);
            DataContext = CartDisplayed;
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
            cart = Bl.Cart.UpdateProductQuantity(id, cart, obj.Amount);
            CartDisplayed = ConvertBOCArtToPlCart(cart);
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btnOrderConfirmation_Click(object sender, RoutedEventArgs e)
    {

    }
    public void getUpdateForCAart(BO.Cart cart)
    {
        this.cart = cart;
        this.CartDisplayed =ConvertBOCArtToPlCart(cart);
        MessageBox.Show(CartDisplayed?.ToString());
        if (this.CartDisplayed?.Items?.Count() > 0)
        {
            CartItemsListView.Visibility = Visibility.Visible;
            imgEmptyCart.Visibility = Visibility.Collapsed;
            lblForEmptyCart.Visibility = Visibility.Collapsed;
            CartItemsListView.ItemsSource = CartDisplayed.Items;
          //  CartItemsListView.DataContext = CartDisplayed.Items;
        }
    }

    private static PlCart ConvertBOCArtToPlCart(BO.Cart cart)
    {
        PlCart plCart = new PlCart();
        string? tempstr = new(cart.CustomerName);
        plCart.CustomerName = tempstr;
        plCart.Email = cart.Email;
        plCart.Address = cart.Address;
        plCart.FinalPrice = cart.FinalPrice;
        ObservableCollection<PlOrderItem> tempItems = new();
        cart?.Items?.ForEach(item => tempItems.Add(PlOrderItem.ConvertBOorderItemToPoOrderItem(item)));
        plCart.Items = tempItems;
        return plCart;
    }
}
