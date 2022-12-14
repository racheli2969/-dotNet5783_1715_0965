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
using BlApi;
using BlImplementation;

namespace PL_
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        private IBl Bl { get; set; }
        public Product(IBl b)
        {
            InitializeComponent();
            Bl = b;
        }
        public Product(IBl b,int id)
        {
            InitializeComponent();
            Bl = b;
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            Bl.
        }
    }
}
