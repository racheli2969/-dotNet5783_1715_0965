using BlApi;
using System.Windows;

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
            txtDateDelivered.LostFocus += DateDelivered_LostFocus;
            txtReceivedDate.LostFocus += DateReceived_LostFocus;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            orderListWindow.Show();
            this.Close();
        }

        private void btnUpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            //validate input
            //do something
        }

        private void DateDelivered_LostFocus(object sender, RoutedEventArgs e)
        {// validate input
            btnUpdateDeliveryDate.IsEnabled = true;
        }
        private void DateReceived_LostFocus(object sender, RoutedEventArgs e)
        {// validate input
            btnReceivedDate.IsEnabled = true;
        }

        private void UpdateReceivedDate_Click(object sender, RoutedEventArgs e)
        {
            //validate input
            //do something
        }
    }
}
