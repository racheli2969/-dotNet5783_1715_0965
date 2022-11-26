using BL.BO;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
    public interface ICart
    {
        public Cart AddProduct(int productId, Cart c);
      
        public Cart UpdateProductQuantity(int productId, Cart c, int quantity);
        
        public void OrderConfirmation(Cart c, string name, string email, string city, string street, int numOfHouse);
        
    }
}
