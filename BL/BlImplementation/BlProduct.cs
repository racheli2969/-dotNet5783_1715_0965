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
        private DalApi.IDal dal = new DalList();
        public IEnumerable<BO.ProductForList> GetProductList()
        {
            return
        }
        public BO.Product GetProductForManager(int id)
        {
            return
        }
        public BO.ProductItem GetProductForCustomer(int id)
        {
            return
        }
        public void AddProduct(BO.Product p) {
        }
    }
}
