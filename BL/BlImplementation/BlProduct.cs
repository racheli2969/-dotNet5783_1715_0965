using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BL.BlImplementation
{

    internal class BlProduct : IProduct
    {
        private DalApi.IDal dal = new Dal.DalList();
        public IEnumerable<BO.ProductForList> GetProductList()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            products.AddRange((IEnumerable<BO.ProductForList>)dal.Item.GetAll());
            return products;
        }
        public BO.Product GetProductForManager(int id)
        {

            return
        }
        public BO.ProductItem GetProductForCustomer(int id)
        {
            return
        }
        public void AddProduct(BO.Product p)
        {

        }
        public void RemoveProduct(int productId)
        {
   
        }
        public void UpdateProduct(BO.Product p)
        {
            bool b = false;
            if (p.ID == 0) 
            {
                b = true;
                throw new NegativeIdException();
            }
            if (p.Price < 0) 
            {
                b = true;
                throw new NegativePriceException(); 
            }
            if (p.Name != null && p.Category != null && p.AmountInStock >= 0 && b == false)
            {

                dal.Item.Update((DO.Item)p);
            }


        }
    }
}
