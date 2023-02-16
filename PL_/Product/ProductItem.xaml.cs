using BlApi;
using PL_.PO;
using System;
using System.Windows;

namespace PL_.Product;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItemWindow : Window
{
    private IBl? Bl { get; set; }
    private Product.ProductCatalog productCatalog { get; set; }
    private BO.Cart? cart { get; set; }
    private PlProductItem plproductItem { get; set; }
    bool wasCartChanged = false;
    Action<BO.Cart> sendChangesToCatalog;
    public ProductItemWindow(IBl? b, int id, ProductCatalog pc,BO.Cart c,Action<BO.Cart> getChangesOnCartFromProductItem)
    {
        InitializeComponent();
        Bl = b;
        productCatalog = pc;
        plproductItem =PlProductItem.ConvertProductItemFromBOToPo(Bl?.Product?.GetProductForCustomer(id, c)??throw new BlApi.BlNOtImplementedException());
        cart = c;
        DataContext= plproductItem;
        sendChangesToCatalog = getChangesOnCartFromProductItem;
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        if (wasCartChanged&&cart!=null)
        {
            sendChangesToCatalog(cart);
        }
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
        try
        {
           if(cart != null)
            cart = Bl?.Cart?.AddToCart(plproductItem.ID, cart);
            MessageBox.Show($"successfully added {plproductItem.Name} to cart");
           wasCartChanged = true;
        }
        catch (BlApi.NotInStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void DecreaseToCart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (cart != null)
                cart = Bl?.Cart?.UpdateProductQuantity(plproductItem.ID, cart,plproductItem.Amount);
            wasCartChanged=true;
        }
        catch (BlApi.NotInStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
