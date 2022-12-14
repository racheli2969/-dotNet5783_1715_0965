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
using BO;
using BlImplementation;

namespace PL
{
<<<<<<< HEAD
    private IBl Bl { get; set; } 
    public ProductList(IBl b)
    {
        InitializeComponent();
        try
        {
            Bl = b;
        }catch(Exception e)
        {
            MessageBox.Show(e.Message);
        }
        //ProductListView.ItemsSource = Bl.Product.GetProductList();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.BookGenre));
    
=======
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        private IBl Bl { get; set; }
        public ProductList(IBl b)
        {
            InitializeComponent();
            Bl = b;
            //ProductListView.ItemsSource = Bl.Product.GetProductList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.BookGenre));

        }
>>>>>>> 2239648d27eb74dab295007bcbd2514a472beb52
    }
}
