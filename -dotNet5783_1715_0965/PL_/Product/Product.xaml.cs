using System;
using System.Windows;
using BlApi;
using System.Text.RegularExpressions;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl? Bl { get; set; }
        private BO.Product? product = new();
        private ProductListWindow productListWindow { get; set; }
        /// <summary>
        /// a constructor for adding 
        /// </summary>
        /// <param name="b"></param>
        public ProductWindow(IBl? b, ProductListWindow pwl)
        {
            InitializeComponent();
            Bl = b;
            productListWindow = pwl;
            btnUpdateProduct.Visibility = Visibility.Hidden;
            btnDeleteProduct.Visibility = Visibility.Hidden;
            cmbCategoryForProduct.ItemsSource = Enum.GetValues(typeof(BO.BookGenre));
        }
        public ProductWindow(IBl? b, int id, ProductListWindow pwl)
        {
            InitializeComponent();
            Bl = b;
            productListWindow = pwl;
            btnAddProduct.Visibility = Visibility.Hidden;
            product = Bl?.Product.GetProductForManager(id);
            txtProductName.Text = product?.Name;
            if (product != null)
            {
                txtProductPrice.Text = product.Price.ToString();
                txtProductAmount.Text = product.AmountInStock.ToString();
                cmbCategoryForProduct.SelectedItem = product.Category;
                cmbCategoryForProduct.ItemsSource = Enum.GetValues(typeof(BO.BookGenre));
            }
        }
        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validateInput();
                buildProduct();
                if (product != null)
                    Bl?.Product.UpdateProduct(product);
                MessageBox.Show("product was updated successfully");
                productListWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buildProduct()
        {
            int temp;
            double t;
            double.TryParse(txtProductPrice.Text, out t);
            if (product != null)
            {
                product.Price = t;
                product.Name = txtProductName.Text;
                product.Category = (BO.BookGenre)cmbCategoryForProduct.SelectedItem;
                int.TryParse(txtProductAmount.Text, out temp);
                product.AmountInStock = temp;
            }
        }

        private void validateInput()
        {
            if (String.IsNullOrEmpty(txtProductName.Text))
                throw new Exception("NO name entered!\n");
            if (char.IsWhiteSpace(txtProductName.Text[0]))
                throw new Exception("name does not start with a whitespace!\n");
            if (cmbCategoryForProduct.SelectedIndex == -1)
                throw new Exception("NO Category Chosen!\n");
            if (String.IsNullOrEmpty(txtProductPrice.Text))
                throw new Exception("NO Price entered!\n");
            if (!Regex.IsMatch(txtProductPrice.Text, @"^\-?[0-9]+(?:\.[0-9]+)?$"))
                throw new Exception("price is a number!\n");
            if (String.IsNullOrEmpty(txtProductAmount.Text))
                throw new Exception("NO Amount entered!\n");
            if (!Regex.IsMatch(txtProductAmount.Text, @"^[0-9]+$"))
                throw new Exception("amount is a number!\n");
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validateInput();
                buildProduct();
                if (product != null)
                {
                    int? result = Bl?.Product.AddProduct(product);
                    MessageBox.Show("the product was successfully added with id: " + result);
                }
                productListWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBackToProductList_Click(object sender, RoutedEventArgs e)
        {
            productListWindow.Show();
            this.Close();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"are you sure you want to delete {product?.Name}", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    if (product != null)
                        Bl?.Product.RemoveProduct(product.ID);
                    MessageBox.Show("successfully deleted");
                     productListWindow.Show();
                    this.Close();
                }
                catch (BlApi.ErrorDeleting ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
    }
}
