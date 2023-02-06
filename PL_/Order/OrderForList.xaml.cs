using BlApi;
using BO;
using System.Collections.Generic;
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
        private IEnumerable<BO.OrderForList>? orders { get; set; }
        public OrderList(IBl? b, Admin a)
        {
            InitializeComponent();
            Bl = b;
            admin = a;
            orders = Bl?.Order.GetOrderList();
            OrderListView.ItemsSource = orders;
            OrderListView.DataContext = orders;
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((BO.OrderForList)OrderListView.SelectedItem) != null)
            {
                OrderWindow o = new(Bl, ((BO.OrderForList)OrderListView.SelectedItem).Id, this, updateOrders);
                o.Show();
                this.Hide();
            }
        }

        private void updateOrders()
        {
            orders = Bl?.Order.GetOrderList();
            OrderListView.DataContext = orders;
        }
        private void backToAdmin_Click(object sender, RoutedEventArgs e)
        {
            admin.Show();
            this.Hide();
        }
    }
}
