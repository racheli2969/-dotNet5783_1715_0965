using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;

namespace PL_.Product;

/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl? Bl { get; set; }
    private Admin admin { get; set; }
    private IEnumerable<BO.ProductForList>? products { get; set; }

    public ProductListWindow(IBl? b, Admin a)
    {
        InitializeComponent();
        Bl = b;
        admin = a;
        string[] enumOptions = Enum.GetNames(typeof(BO.BookGenre));
        List<string> options = new();
        options.Insert(0, "GetAll");
        options.AddRange(enumOptions);
        CategorySelector.ItemsSource = options;
        products = Bl?.Product.GetProductList();
        ProductListView.DataContext= products;
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        ProductWindow p = new(Bl, this,updateProducts);
        p.Show();
        this.Hide();
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (CategorySelector?.SelectedItem?.ToString()?.CompareTo("GetAll") != 0)
            ProductListView.DataContext = Bl?.Product.GetProductList((BO.BookGenre?)Enum.Parse(typeof(BO.BookGenre), CategorySelector?.SelectedItem.ToString() ?? ""));
        else
            ProductListView.DataContext = Bl?.Product.GetProductList();

    }

    private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            ProductWindow p = new(Bl, ((BO.ProductForList)ProductListView.SelectedItem).ItemId, this,updateProducts);
            p.Show();
            this.Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void backToAdmin_Click(object sender, RoutedEventArgs e)
    {
        admin.Show();
        this.Hide();

    }

    private void updateProducts()
    {
        products = Bl?.Product.GetProductList();
        ProductListView.DataContext = products;
    }

    //private void viewOrders_Click(object sender, RoutedEventArgs e)
    //{
    //    orderList.Show();
    //    this.Hide();
    //}
}
