using BlApi;
using System.Windows;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private IBl? Bl { get; set; }
        private PL.Product.ProductCatalog productCatalog { get; set; }
        public ProductItemWindow(IBl? b, int id, PL.Product.ProductCatalog pc,BO.Cart c)
        {
            Bl = b;
            productCatalog = pc;
            BO.ProductItem product = Bl?.Product.GetProductForCustomer(id,c);


            InitializeComponent();
        }

    }
}
