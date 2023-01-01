using System;
using System.Collections.Generic;
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
using BlApi;
using BlImplementation;
using PL_;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl Bl { get; set; }
        public ProductListWindow(IBl b)
        {
            InitializeComponent();
            Bl = b;
            CategorySelector.Items.Clear();
        string[] enumOptions= Enum.GetNames(typeof(BO.BookGenre));
            List<string> options = new();
            options.Insert(0, "GetAll");
            options.AddRange(enumOptions);
            CategorySelector.ItemsSource =options;
            ProductListView.ItemsSource = Bl.Product.GetProductList();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product p = new(Bl);
            p.Show();
            this.Close();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CategorySelector?.SelectedItem?.ToString()?.CompareTo("GetAll")!=0)
                ProductListView.ItemsSource = Bl.Product.GetProductList((BO.BookGenre?)Enum.Parse( typeof(BO.BookGenre),CategorySelector.SelectedItem.ToString()));
            else
                ProductListView.ItemsSource = Bl.Product.GetProductList();

        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product p = new(Bl,(ProductListView.SelectedItem as BO.ProductForList).ItemId);
            p.Show();
            this.Close();
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
