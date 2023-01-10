using BlApi;
using PL;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductCatalog.xaml
    /// </summary>
    public partial class ProductCatalog : Window
    {
        private IBl? Bl { get; set; }
        private MainWindow mainWindow { get; set; }
        public ProductCatalog(IBl? b, MainWindow mw)
        {
            InitializeComponent();
            Bl = b;
            mainWindow = mw;
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
            //PL.Product p = new(Bl, ((BO.ProductForList)ProductCatalog.SelectedItem).ItemId, this);
            //p.Show();
            //this.Hide();
        }

        private void ToCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
