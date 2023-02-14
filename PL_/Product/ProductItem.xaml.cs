using BlApi;
using System.Windows;

namespace PL_.Product;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItemWindow : Window
{
    private IBl? Bl { get; set; }
    private Product.ProductCatalog productCatalog { get; set; }
    private int ID { get; set; }
    private BO.Cart cart { get; set; }
    public ProductItemWindow(IBl? b, int id, ProductCatalog pc,BO.Cart c)
    {
        InitializeComponent();
        Bl = b;
        productCatalog = pc;
        BO.ProductItem? product = Bl?.Product?.GetProductForCustomer(id,c);
        ID = id;
        //lblId.Content = ID;
        cart = c;
        DataContext= product;
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        productCatalog.Show();
        this.Hide();
    }
    private void CloseAllWindows(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("GoodBye");
        for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            App.Current.Windows[intCounter].Close();

    }

    private void AddToCart_Click(object sender, RoutedEventArgs e)
    {
        Bl?.Cart.AddToCart(ID, cart);
        productCatalog.Show();
        this.Close();
    }
}
