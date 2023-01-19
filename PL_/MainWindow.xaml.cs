using PL.Product;
using System.Windows;

namespace PL
{
    //private BlAp
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl? Bl { get; set; }
        private Admin admin { get; set; }
        private PL.Product.ProductCatalog ProductCatalog { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Bl = BlApi.Factory.Get();
            admin = new(Bl ?? null, this);
            ProductCatalog = new(b: Bl ?? null, this);
            // MessageBox.Show("Welcome to My store");
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            admin.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductCatalog.Show();
            this.Hide();
        }
    }
}
