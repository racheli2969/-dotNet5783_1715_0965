﻿using System;
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

namespace PL
{
    //private BlAp
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl? Bl { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Bl = BlApi.Factory.Get();
           // MessageBox.Show("Welcome to My store");
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow PL = new(Bl);
            PL.Show();
            this.Close();
        }
    }
}
