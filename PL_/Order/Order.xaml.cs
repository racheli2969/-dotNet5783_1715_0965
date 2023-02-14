using BlApi;
using Microsoft.VisualBasic;
using PL_.Order;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PL_.Order
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl? Bl { get; set; }
        private OrderList orderListWindow { get; set; }
        private Action<BO.EnumOrderStatus,int> updateOrders;
        public BO.Order? order { get; set; }

        public OrderWindow(IBl? bl, int id, OrderList ol, Action<BO.EnumOrderStatus, int> updateOs)
        {
            InitializeComponent();
            Bl = bl;
            updateOrders = updateOs;
            orderListWindow = ol;
            order = bl?.Order?.GetOrderDetails(id);
            DataContext = order;
            OrderItemListView.ItemsSource = order?.Items;
            switch (order?.OrderStatus)
            {
                case BO.EnumOrderStatus.Ordered:
                    btnUpdateDeliveryDate.Visibility = Visibility.Visible;
                    btnReceivedDate.Visibility = Visibility.Visible;
                    break;
                case BO.EnumOrderStatus.Shipped:
                    btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
                    btnReceivedDate.Visibility = Visibility.Visible;
                    btnReceivedDate.IsEnabled = true;
                    break;
                case BO.EnumOrderStatus.Delivered:
                    btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
                    btnReceivedDate.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            orderListWindow.Show();
            this.Close();
        }
        /// <summary>
        /// updates the date of shipping to be now
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            if (order != null)
                order = Bl?.Order.UpdateOrderShipping(order.OrderId);
            DataContext = order;
            btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
            btnReceivedDate.IsEnabled = true;
            if (order != null)
                updateOrders(BO.EnumOrderStatus.Shipped, order.OrderId);
        }

        private void UpdateReceivedDate_Click(object sender, RoutedEventArgs e)
        {
            if (order != null)
                order = Bl?.Order.UpdateOrderDelivery(order.OrderId);
            DataContext = order;
            btnReceivedDate.Visibility = Visibility.Hidden;
            lblReceivedDate.Content = order?.DateReceived;
            if (order != null)
                updateOrders(BO.EnumOrderStatus.Delivered, order.OrderId);
        }
        //for bonus
        private void UpdateOrderItem_Click(object sender, RoutedEventArgs e)
        {
           // updateOrders();
        }
    }
}
