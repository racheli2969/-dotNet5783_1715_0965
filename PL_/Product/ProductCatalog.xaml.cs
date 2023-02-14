using BlApi;
using PL_;
using PL_.Cart;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace PL_.Product;

/// <summary>
/// Interaction logic for ProductCatalog.xaml
/// </summary>
public partial class ProductCatalog : Window
{
    private IBl? Bl { get; set; }
    private MainWindow mainWindow { get; set; }
    private BO.Cart cart { get; set; }
    private PL_.Cart.CartWindow cartWindow { get; set; }
    public ProductCatalog(IBl? b, MainWindow mw)
    {
        InitializeComponent();
        Bl = b;
        mainWindow = mw;
        cart = new BO.Cart();
        cartWindow = new CartWindow(this,cart);
        //the combo box
        string[] enumOptions = Enum.GetNames(typeof(BO.BookGenre));
        List<string> options = new();
        options.Insert(0, "GetAll");
        options.AddRange(enumOptions);
        CategorySelector.ItemsSource = options;
        ProductCatalogView.ItemsSource = Bl?.Product.GetProductList();
    }
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (CategorySelector?.SelectedItem?.ToString()?.CompareTo("GetAll") != 0)
            ProductCatalogView.ItemsSource = Bl?.Product.GetProductList((BO.BookGenre?)Enum.Parse(typeof(BO.BookGenre), CategorySelector?.SelectedItem.ToString() ?? ""));
        else
            ProductCatalogView.ItemsSource = Bl?.Product.GetProductList();

    }

    private void ProductCatalogView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            ProductItemWindow p = new ProductItemWindow(Bl, ((BO.ProductForList)ProductCatalogView.SelectedItem).ItemId, this, cart);
            p.Show();
            this.Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ToCart_Click(object sender, RoutedEventArgs e)
    {
        cartWindow.Show();
        this.Hide();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.Show();
        this.Hide();
    }
}