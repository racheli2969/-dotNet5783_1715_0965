using PL.Product;
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

namespace PL_
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        private ProductCatalog productCatalog { get; set; }
        public Cart(ProductCatalog p)
        {
            InitializeComponent();
            productCatalog = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            productCatalog.Show();
            this.Hide();
        }
    }
}
