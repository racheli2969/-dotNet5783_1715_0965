using BlApi;
using PL;
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

namespace PL_.Product
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class ProductItem : Window
    {
        private IBl? Bl { get; set; }
        private PL.Product.ProductCatalog productCatalog { get; set; }
        public ProductItem(IBl? b, int id, PL.Product.ProductCatalog pc)
        {
            Bl = b;

            InitializeComponent();
        }
    }
}
