using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;
using PL_;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl? Bl { get; set; }
        private MainWindow mainWindow { get; set; }
        private PL_.Admin.OrderForAdmin.OrderList orderList { get; set; }   
        public ProductListWindow(IBl? b, MainWindow mw)
        {
            InitializeComponent();
            Bl = b;
            mainWindow = mw;
            orderList = new PL_.Admin.OrderForAdmin.OrderList(Bl,this);
            //CategorySelector.Items.Clear();
            string[] enumOptions = Enum.GetNames(typeof(BO.BookGenre));
            List<string> options = new();
            options.Insert(0, "GetAll");
            options.AddRange(enumOptions);
            CategorySelector.ItemsSource = options;
            ProductListView.ItemsSource = Bl?.Product.GetProductList();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product p = new(Bl,this);
            p.Show();
            this.Hide();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CategorySelector?.SelectedItem?.ToString()?.CompareTo("GetAll") != 0)
                ProductListView.ItemsSource = Bl?.Product.GetProductList((BO.BookGenre?)Enum.Parse(typeof(BO.BookGenre), CategorySelector?.SelectedItem.ToString()??""));
            else
                ProductListView.ItemsSource = Bl?.Product.GetProductList();

        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Product p = new(Bl, ((BO.ProductForList)ProductListView.SelectedItem).ItemId, this);
                p.Show();
                this.Hide();
            }
           catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void viewOrders_Click(object sender, RoutedEventArgs e)
        {
            orderList.Show();
            this.Hide();
        }
    }
}
