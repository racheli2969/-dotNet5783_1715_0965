using System.Windows;

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
            ProductListWindow PL = new(b: Bl??null,this);
            PL.Show();
            this.Hide();
        }
    }
}
