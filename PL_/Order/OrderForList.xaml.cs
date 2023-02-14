using BlApi;
using BO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PL_.Order
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        private IBl? Bl { get; set; }
        private Admin admin { get; set; }
        private List<PlOrderForList>? orders { get; set; }
        public OrderList(IBl? b, Admin a)
        {
            InitializeComponent();
            Bl = b;
            admin = a;
            orders = getOrders();
            //OrderListView.DataContext = orders;
            OrderListView.ItemsSource = orders;

        }
        private List<PlOrderForList>? getOrders()
        {
            List<OrderForList>? ordersOfBl = Bl?.Order.GetOrderList().ToList();
            return convertBlOrdersToPl(ordersOfBl);
        }
        private List<PlOrderForList>? convertBlOrdersToPl(List<OrderForList>? ordersOfBl)
        {
            List<PlOrderForList>? ordersOfpl = new List<PlOrderForList>(ordersOfBl.Count());
            PlOrderForList pl;
            for (int i = 0; i < ordersOfBl.Count(); i++)
            {
                pl = new PlOrderForList();
                pl.Id = ordersOfBl[i].Id;
                pl.CustomerName = ordersOfBl[i].CustomerName;
                pl.OrderStatus = ordersOfBl[i]?.OrderStatus;
                pl.NumOfItems = ordersOfBl[i].NumOfItems;
                pl.Price = ordersOfBl[i].Price;
                ordersOfpl.Add(pl);
            }
            return ordersOfpl;
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((PlOrderForList)OrderListView.SelectedItem) != null)
            {
                OrderWindow o = new(Bl, ((PlOrderForList)OrderListView.SelectedItem).Id, this, updateOrders);
                o.Show();
                this.Hide();
            }
        }

        private void updateOrders(EnumOrderStatus status, int id)
        {
            // orders = (IEnumerable<PlOrderForList>?)(Bl?.Order.GetOrderList());
            if (orders != null)
                orders[orders.FindIndex(item => item.Id == id)].OrderStatus = status;
           // OrderListView.DataContext = orders;
        }
        private void backToAdmin_Click(object sender, RoutedEventArgs e)
        {
            admin.Show();
            this.Hide();
        }
    }
}
