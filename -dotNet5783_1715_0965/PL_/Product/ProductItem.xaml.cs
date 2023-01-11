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
        private int ID { get; set; }
        public ProductItemWindow(IBl? b, int id, PL.Product.ProductCatalog pc,BO.Cart c)
        {
            InitializeComponent();
            Bl = b;
            productCatalog = pc;
            BO.ProductItem product = Bl.Product.GetProductForCustomer(id,c);
            ID = id;
            //lblId.Content = ID;
            DataContext= product;
        }

    }
}
