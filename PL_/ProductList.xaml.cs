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
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.BookGenre));
            ProductListView.ItemsSource = Bl.Product.GetProductList();
        }

        private void CategorySelector_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = Bl.Product.GetProductList((BO.BookGenre?)CategorySelector.SelectedItem);

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product p = new(Bl);
            p.Show();
            this.Hide();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = Bl.Product.GetProductList((BO.BookGenre?)CategorySelector.SelectedItem);

        }
    }
}
