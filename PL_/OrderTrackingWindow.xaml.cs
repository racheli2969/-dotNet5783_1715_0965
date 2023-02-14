using BlApi;
using PL_.Order;
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
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    private IBl? Bl { get; set; }
    private MainWindow mainWindow { get; set; }
    private int Id { get; set; }
    public OrderTrackingWindow(IBl? b,int id,MainWindow mw)
    {
        InitializeComponent();
        Bl = b;
        mainWindow = mw;
        Id = id;
        BO.OrderTracking? orderTracking = Bl?.Order.OrderTracking(id);
        info.Content = orderTracking?.ToString();
    }

    private void btnBackToMain_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.Show();
        this.Close();
    }

    private void btnExit_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("GoodBye");
        for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            App.Current.Windows[intCounter].Close();
    }

    private void btnViewMyOrder_Click(object sender, RoutedEventArgs e)
    {
        OrderWindow ow = new(Bl, Id, mainWindow);
        ow.Show();
        this.Close();
    }
}
