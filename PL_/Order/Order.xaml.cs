using BlApi;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl? Bl { get; set; }
        private OrderList orderListWindow { get; set; }

        public BO.Order? order
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(BO.Order), typeof(OrderWindow));


        ///// <summary>
        ///// dependency property for date of delivery ro update
        ///// </summary>
        //public DateTime? DateDeliveredProp
        //{
        //    get { return (DateTime)GetValue(DateDeliveredProperty); }
        //    set { SetValue(DateDeliveredProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for TextDateDelivered.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty? DateDeliveredProperty =
        //    DependencyProperty.Register("TextDateDeliveredProp", typeof(string), typeof(OrderWindow));

        ///// <summary>
        ///// dependency property for date received
        ///// </summary>
        //public DateTime DateReceivedProp
        //{
        //    get { return (DateTime)GetValue(DateReceivedProperty); }
        //    set { SetValue(DateReceivedProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for DateReceived.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DateReceivedProperty =
        //    DependencyProperty.Register("DateReceived", typeof(string), typeof(OrderWindow));

        public OrderWindow(IBl? bl, int id, OrderList ol)
        {
            InitializeComponent();
            Bl = bl;
            orderListWindow = ol;

            ////set the delivery date picker
            //DateDeliveredPicker.SelectedDateChanged += DateDeliveredProp_selectionChanged;
            //DateDeliveredPicker.DisplayDateStart = DateTime.Now;//.Subtract(new TimeSpan(30,2,2,2));
            //DateDeliveredPicker.DisplayDateEnd = DateTime.Now.AddMonths(6);

            ////set the received date picker
            //ReceivedDatePicker.SelectedDateChanged += DateReceived_selectionChanged;
            //ReceivedDatePicker.DisplayDateStart = DateTime.Now;
            //ReceivedDatePicker.DisplayDateEnd = DateTime.Now.AddMonths(4);

            order = bl?.Order?.GetOrderDetails(id);
            DataContext = order;
            OrderItemListView.ItemsSource = order?.Items;
            switch (order?.OrderStatus)
            {
                case BO.EnumOrderStatus.Ordered:
                    btnUpdateDeliveryDate.Visibility = Visibility.Visible;
                    btnReceivedDate.Visibility = Visibility.Visible;
                    //lblDateDelivered.Visibility = Visibility.Hidden;
                    btnReceivedDate.IsEnabled = false;
                    break;
                case BO.EnumOrderStatus.Delivered:
                    btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
                    btnReceivedDate.Visibility = Visibility.Visible; break;
                case BO.EnumOrderStatus.Received:
                    btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
                    btnReceivedDate.Visibility = Visibility.Hidden; break;
                default:
                    break;
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            orderListWindow.Show();
            this.Close();
        }

        private void btnUpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            //if (order?.DateOrdered > order?.DateDelivered)
            //    throw new Exception("delivery date must be a later date than the order date");

            //if (order?.DateDelivered <= DateTime.Now)
            //{
            //    order.OrderStatus = BO.EnumOrderStatus.Delivered;
            //}
            order=Bl?.Order.UpdateOrderShipping(order.OrderId);
            btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
            lblDateDelivered.Visibility = Visibility.Visible;
           // lblReceivedDate.Visibility = Visibility.Visible;
           
        }

        //private void DateDeliveredProp_selectionChanged(object sender, RoutedEventArgs e)
        //{// validate input
        //    btnUpdateDeliveryDate.IsEnabled = true;
        //}
        //private void DateReceived_selectionChanged(object sender, RoutedEventArgs e)
        //{// validate input
        //    btnReceivedDate.IsEnabled = true;
        //}

        private void UpdateReceivedDate_Click(object sender, RoutedEventArgs e)
        {
          order=Bl?.Order.UpdateOrderDelivery(order.OrderId);
         
            //lblReceivedDate.Visibility = Visibility.Visible;    
            btnReceivedDate.Visibility = Visibility.Hidden;
            lblReceivedDate.Content = order.DateReceived;
        }
        private void UpdateOrderItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
