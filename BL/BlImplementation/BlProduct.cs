using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;

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
            Product p = new Product();
            if (id >= 100000)
            {

                return
            }
            else
                throw new ProductNotFoundException();
        }
        public BO.ProductItem GetProductForCustomer(int id, Cart c)
        {
            ProductItem p = new ProductItem();
            if (id >= 100000)
            {
                return
            }
            else
                throw new ProductNotFoundException();
        }
        public void AddProduct(BO.Product p)
        {
            if (p.ID < 100000)
                throw new NegativeIdException();
            if (p.Price < 0)
                throw new NegativePriceException();
            if (p.Name == null || p.Category == null)
                throw new EmptyStringException();
            if (p.AmountInStock < 0)
                throw new NegativeAmountException();
            dal.Item.Add(p);
        }
        public void RemoveProduct(int productId)
        {
           
        }
        public void UpdateProduct(BO.Product p)
        {
            if (p.ID == 0)
            {
                throw new NegativeIdException();
            }
            if (p.Price < 0)
            {
                throw new NegativePriceException();
            }
            if (p.AmountInStock < 0)
            {
                throw new NegativeAmountException();
            }
            if (p.Name == null || p.Category == null)
            {
                throw new EmptyStringException();
            }

            dal.Item.Update(p);



        }
    }
}
