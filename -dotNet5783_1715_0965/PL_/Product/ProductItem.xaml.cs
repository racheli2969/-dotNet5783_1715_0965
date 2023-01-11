using BlApi;
<<<<<<< HEAD:-dotNet5783_1715_0965/PL_/Product/ProductItem.xaml.cs
using BO;
using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
>>>>>>> 29b807a8f3d7d997189f50c1c799f8a1b61d4830:PL_/Product/ProductItem.xaml.cs
using System.Windows;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private IBl? Bl { get; set; }
        private PL.Product.ProductCatalog productCatalog { get; set; }
        public ProductItemWindow(IBl? b, int id, PL.Product.ProductCatalog pc,BO.Cart c)
        {
            Bl = b;
            productCatalog = pc;
<<<<<<< HEAD:-dotNet5783_1715_0965/PL_/Product/ProductItem.xaml.cs
            
            BO.ProductItem product = Bl?.Product.GetProductForCustomer(id,)
=======
            BO.ProductItem product = Bl?.Product.GetProductForCustomer(id,c);
>>>>>>> 29b807a8f3d7d997189f50c1c799f8a1b61d4830:PL_/Product/ProductItem.xaml.cs


            InitializeComponent();
        }

    }
}
