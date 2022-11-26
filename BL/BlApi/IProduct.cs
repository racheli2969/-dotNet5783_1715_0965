using BL.BO;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList> GetProductList()
        {
            return 
        }
        public ProductItem GetProductDetails(Cart C,int productId)
        {
            return
        }
        public  void AddProduct(Product p)
        {

        }
        public void RemoveProduct(int productId)
        {

        }
        public void UpdateProduct(Product p)
        {

        }

    }
}
