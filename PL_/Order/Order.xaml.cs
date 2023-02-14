﻿using BlApi;
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
        private OrderList? orderListWindow { get; set; }
        private MainWindow? mainWindow { get; }
        private Action<BO.EnumOrderStatus, int>? updateOrders;
        public BO.Order? order { get; set; }
        /// <summary>
        /// constructor for admin to update reached through the order list window
        /// </summary>
        /// <param name="bl">the blapi obj through it we access the bl layer</param>
        /// <param name="id">id of the order we want to open for admin</param>
        /// <param name="ol">the order list window where the admin could return to when he's done</param>
        /// <param name="updateOs">a function that we call only when there's an update in the order staus and is used to update the staus in order list using the functionality of dependency object</param>
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
            mainWindow = null;
        }
        public OrderWindow(IBl? bl, int id, MainWindow mw)
        {
            InitializeComponent();
            Bl = bl;
            order = bl?.Order?.GetOrderDetails(id);
            DataContext = order;
            mainWindow = mw;
            OrderItemListView.ItemsSource = order?.Items;
            orderListWindow = null;
            updateOrders = null;
            btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
            btnReceivedDate.Visibility = Visibility.Hidden;
            updateColumn.Width = 0;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //if you came through the admin you go back to orders list else you go back to main page
            if (orderListWindow != null)
                orderListWindow.Show();
            else if(mainWindow!=null)
                mainWindow.Show();
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
            if (order != null&&updateOrders!=null)
                updateOrders(BO.EnumOrderStatus.Shipped, order.OrderId);
        }

        private void UpdateReceivedDate_Click(object sender, RoutedEventArgs e)
        {
            if (order != null)
                order = Bl?.Order.UpdateOrderDelivery(order.OrderId);
            DataContext = order;
            btnReceivedDate.Visibility = Visibility.Hidden;
            lblReceivedDate.Content = order?.DateReceived;
            if (order != null && updateOrders != null)
                updateOrders(BO.EnumOrderStatus.Delivered, order.OrderId);
        }
        //for bonus
        private void UpdateOrderItem_Click(object sender, RoutedEventArgs e)
        {
            // updateOrders();
        }
    }
}
