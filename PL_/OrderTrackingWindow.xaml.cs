using BlApi;
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
    public OrderTrackingWindow(IBl? b,int id,MainWindow mw)
    {
        InitializeComponent();
        Bl = b;
        mainWindow = mw;
        BO.OrderTracking? orderTracking = Bl?.Order.OrderTracking(id);
        info.Content = orderTracking?.ToString();
    }
}
