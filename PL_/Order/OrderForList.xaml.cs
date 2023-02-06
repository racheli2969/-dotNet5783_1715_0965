using BlApi;
using BO;
using PL_.Order;
using System;
using System.Collections.Generic;
using System.Linq;
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


        //public IEnumerable<BO.OrderForList>? orders
        //{
        //    get { return (IEnumerable<BO.OrderForList>)GetValue(ordersProperty); }
        //    set { SetValue(ordersProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for orders.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ordersProperty =
        //    DependencyProperty.Register("orders", typeof(IEnumerable<BO.OrderForList>), typeof(OrderList), new PropertyMetadata(onOrdersChanged));

        //private static void onOrdersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    IEnumerable<BO.OrderForList>? o = (IEnumerable<OrderForList>?)d;

        //}

        private IEnumerable<PlOrderForList>? orders { get; set; }
        public OrderList(IBl? b, Admin a)
        {
            InitializeComponent();
            Bl = b;
            admin = a;
            List<OrderForList>? ordersOfBl = Bl?.Order.GetOrderList().ToList();
            orders = convertBlOrdersToPl(ordersOfBl);
            OrderListView.DataContext = orders;

        }

        private List<PlOrderForList>? convertBlOrdersToPl(List<OrderForList>? ordersOfBl)
        {
            List<PlOrderForList>? ordersOfpl = new List<PlOrderForList>(ordersOfBl.Count());
            for (int i = 0; i < ordersOfBl.Count(); i++)
            {
                ordersOfpl[i].Id = ordersOfBl[i].Id;
                ordersOfpl[i].CustomerName = ordersOfBl[i].CustomerName;
                ordersOfpl[i].OrderStatus = (EnumOrderStatus?)ordersOfBl[i]?.OrderStatus;
                ordersOfpl[i].NumOfItems = ordersOfBl[i].NumOfItems;
                ordersOfpl[i].Price = ordersOfBl[i].Price; 
            }
            return ordersOfpl;
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
            // orders = (IEnumerable<PlOrderForList>?)(Bl?.Order.GetOrderList());
            OrderListView.DataContext = orders;
        }
        private void backToAdmin_Click(object sender, RoutedEventArgs e)
        {
            admin.Show();
            this.Hide();
        }
    }
}
