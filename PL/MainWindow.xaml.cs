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
using System.Windows.Navigation;
using System.Windows.Shapes;
<<<<<<< HEAD
//using BlApi;
//using BlImplementation;
using BO;
=======
using BlApi;
using BlImplementation;
>>>>>>> 2239648d27eb74dab295007bcbd2514a472beb52

namespace PL
{
<<<<<<< HEAD
    public BlApi.IBl Bl { get; set; } 
    //private BlApi.IBl Bl_ { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        Bl = new BlImplementation.Bl();
=======
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl Bl { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Bl = new Bl();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
          new ProductList(Bl).Show();
        }
>>>>>>> 2239648d27eb74dab295007bcbd2514a472beb52
    }
}
