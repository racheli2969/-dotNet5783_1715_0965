using BlApi;
using PL;
using PL_;
using System.Windows;
using System.Windows.Input;

namespace PL_.Admin.OrderForAdmin
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        private IBl? Bl { get; set; }
        private ProductListWindow productListWindow { get; set; }
        public OrderList(IBl? b, ProductListWindow plw)
        {
            InitializeComponent();
            Bl = b;
            productListWindow = plw;
            OrderListView.ItemsSource = Bl?.Order.GetOrderList();
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((BO.OrderForList)OrderListView.SelectedItem)!=null)
            {
                Order o = new(Bl, ((BO.OrderForList)OrderListView.SelectedItem).Id, this);
                o.Show();
                this.Hide();
            }
        }
    }
}
