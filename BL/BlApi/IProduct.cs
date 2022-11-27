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
        public IEnumerable<ProductForList> GetProductList();
       
        public ProductItem GetProductDetails(Cart C, int productId);
        
        public void AddProduct(Product p);
        
        public void RemoveProduct(int productId);
       
        public void UpdateProduct(Product p);
        

    }
}
