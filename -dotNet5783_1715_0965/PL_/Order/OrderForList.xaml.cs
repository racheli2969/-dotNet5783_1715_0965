using BlApi;
using System.Windows;
using System.Windows.Input;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        private IBl? Bl { get; set; }
        private Admin admin { get; set; }
        public OrderList(IBl? b, Admin a)
        {
            InitializeComponent();
            Bl = b;
            admin = a;
            OrderListView.ItemsSource = Bl?.Order.GetOrderList();
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((BO.OrderForList)OrderListView.SelectedItem)!=null)
            {
                OrderWindow o = new(Bl, ((BO.OrderForList)OrderListView.SelectedItem).Id, this);
                o.Show();
                this.Hide();
            }
        }

        private void backToAdmin_Click(object sender, RoutedEventArgs e)
        {
            admin.Show();
            this.Hide();
        }
    }
}
