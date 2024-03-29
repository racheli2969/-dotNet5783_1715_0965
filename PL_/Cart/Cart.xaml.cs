﻿using PL_.PO;
using PL_.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
        CartDisplayed.FinalPrice = 0;
        DataContext = CartDisplayed;
        CartItemsListView.DataContext = CartDisplayed.Items;
        this.cart = cart;

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
            int amount = obj.Amount + 1;
            if (CartDisplayed?.Items?.Count()==0)
            cart = Bl.Cart.AddToCart(id, cart ?? throw new BlApi.BlNOtImplementedException());
            else cart = Bl.Cart.UpdateProductQuantity(id, cart ?? throw new BlApi.BlNOtImplementedException(),amount);
            //update amount and price of product
            int? idx = CartDisplayed?.Items?.FindIndex(i => i.ItemId == id);
            (CartDisplayed?.Items ?? throw new BlApi.BlNOtImplementedException())[idx ?? 0].Amount++;
            (CartDisplayed?.Items ?? throw new BlApi.BlNOtImplementedException())[idx ?? 0].PriceOfItems = (cart?.Items?.Find(i => i.ItemId == id)?.PriceOfItems) ?? 0;

            CartDisplayed.FinalPrice = (cart ?? throw new BlApi.BlNOtImplementedException()).FinalPrice;
            lblFinalPriceValue.Content = CartDisplayed?.FinalPrice;
        }
        catch (BlApi.BlNOtImplementedException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NotInStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch
        {
            MessageBox.Show("unexplained error occured\n");
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

            CartItemsListView.ItemsSource = CartDisplayed?.Items;
            lblFinalPriceValue.Content = CartDisplayed?.FinalPrice;
            if (CartDisplayed?.Items?.Count() == 0)
            {
                CartItemsListView.Visibility = Visibility.Collapsed;
                imgEmptyCart.Visibility = Visibility.Visible;
                lblForEmptyCart.Visibility = Visibility.Visible;
            }

        }
        catch (BlApi.BlNOtImplementedException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NotInCartException ex)
        {
            
            MessageBox.Show(ex.Message);
        }
        catch
        {
            MessageBox.Show("unexplained error occured\n");
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
           
            if (amount == 0)
            {
                CartDisplayed = ConvertBOCArtToPlCart(cart);
                CartItemsListView.ItemsSource = CartDisplayed?.Items; 
            }
            else
            {
                int? idx = CartDisplayed?.Items?.FindIndex(i => i.ItemId == id);
                (CartDisplayed?.Items ?? throw new BlApi.BlNOtImplementedException())[idx ?? 0].Amount = amount;
                (CartDisplayed?.Items ?? throw new BlApi.BlNOtImplementedException())[idx ?? 0].PriceOfItems = (cart?.Items?.Find(i => i.ItemId == id)?.PriceOfItems)??0;
            }
            (CartDisplayed ?? throw new BlApi.BlNOtImplementedException()).FinalPrice = (cart ?? throw new BlApi.BlNOtImplementedException()).FinalPrice;
            lblFinalPriceValue.Content = CartDisplayed?.FinalPrice;
        }
        catch (BlApi.BlNOtImplementedException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch
        {
            MessageBox.Show("unexplained error occured\n");
        }
    }

    private void btnOrderConfirmation_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (cart == null)
                throw new BlApi.BlNOtImplementedException();
            cart.CustomerName = txtCustomerName.Text;
            cart.Email = txtEmail.Text;
            cart.Street = txtStreet.Text;
            cart.City = txtCity.Text;
            cart.NumOfHouse = int.Parse(txtNumHOuse.Text);
          int id= Bl.Cart.OrderConfirmation(cart);
            MessageBox.Show($"successfully created order with id: {id}");
            this.Close();
        }
        catch (BlApi.NoItemsInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.BlNOtImplementedException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.ExistsAlreadyException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.BlEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.EmptyStringException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NegativeIdException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NegativeHouseNumberException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NegativeAmountException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NegativePriceException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NotInStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (FormatException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch
        {
            MessageBox.Show("unexplained error occured\n");
        }
    }
    public void getUpdateForCAart(BO.Cart cart)
    {
        this.cart = cart;
        cart.FinalPrice=cart.Items.Select(i => i.PriceOfItems).Sum();
        this.CartDisplayed = ConvertBOCArtToPlCart(cart);
        if (this.CartDisplayed?.Items?.Count() > 0)
        {
            CartItemsListView.Visibility = Visibility.Visible;
            imgEmptyCart.Visibility = Visibility.Collapsed;
            lblForEmptyCart.Visibility = Visibility.Collapsed;
            CartItemsListView.ItemsSource = CartDisplayed.Items;
            lblFinalPriceValue.Content = CartDisplayed?.FinalPrice;
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
        plCart.Items = null;
        plCart.Items = tempItems;
        return plCart;
    }

    private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            cart?.Items?.RemoveAll(i => true);
            CartDisplayed?.Items?.RemoveAll(i => true);
            CartItemsListView.ItemsSource = null;
            (CartDisplayed ?? throw new BlApi.BlNOtImplementedException()).FinalPrice = 0;
            lblFinalPriceValue.Content = 0;
        }
        catch (BlApi.NotInCartException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlApi.BlNOtImplementedException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch
        {
            MessageBox.Show("unexplained error occured\n");
        }
    }

}
