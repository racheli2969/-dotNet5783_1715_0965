using BlApi;
using PL_.Product;
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

namespace PL_;

/// <summary>
/// Interaction logic for Admin.xaml
/// </summary>
public partial class Admin : Window
{
    private IBl? Bl { get; set; }
    //maybe to just create them everytime?
    private ProductListWindow productListWindow { get; set; }
    private PL_.Order.OrderList orderList { get; set; }
    private MainWindow mainWindow { get; set; }
    public Admin(IBl? bl, MainWindow mw)
    {
        InitializeComponent();
        Bl= bl;
        mainWindow= mw;
        productListWindow = new(b: Bl ?? null, this);
        orderList = new(b: Bl ?? null, this);
    }

    private void btnViewOrders_Click(object sender, RoutedEventArgs e)
    {
        orderList.Show();
        this.Hide();
    }

    private void BtnViewProduct_Click(object sender, RoutedEventArgs e)
    {
        productListWindow.Show();
        this.Hide();
    }
}
