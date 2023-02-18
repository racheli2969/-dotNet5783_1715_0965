using PL_.Product;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL_;

//private BlAp
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBl? Bl { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        Bl = BlApi.Factory.Get();
    }

    private void BtnEnter_Click(object sender, RoutedEventArgs e)
    {
        Admin admin = new(Bl ?? null, this);
        admin.Show();
        this.Hide();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Product.ProductCatalog ProductCatalog = new(b: Bl ?? null, this);
        ProductCatalog.Show();
        this.Hide();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        int x;
        Int32.TryParse(textBox.Text, out x);
        OrderTrackingWindow otw = new(Bl,x,this);
        otw.Show();
        this.Hide();

    }
}
