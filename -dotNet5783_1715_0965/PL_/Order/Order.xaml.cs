using BlApi;
using PL;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl? Bl { get; set; }
        private OrderList orderListWindow { get; set; }
        public OrderWindow(IBl? bl, int id, OrderList ol)
        {
            InitializeComponent();
            Bl = bl;
            orderListWindow = ol;
            txtDateDelivered.TextChanged += DateDeliveredChanged_TextChanged;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            orderListWindow.Show();
            this.Close();
        }

        private void DateDeliveredChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
            //validate input? check if can update?
            btnUpdateDeliveryDate.IsEnabled = true;
        }

        private void btnUpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            //validate input
        }
    }
}
