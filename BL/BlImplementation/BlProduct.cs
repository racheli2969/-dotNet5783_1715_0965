﻿

namespace BlImplementation
{

    public class BlProduct : BlApi.IProduct
    {

        private DalApi.IDal dal = new Dal.DalList();
        public IEnumerable<BO.ProductForList> GetProductList()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            List<DO.Item> productsFromDal = new List<DO.Item>();
            BO.ProductForList temp = new BO.ProductForList();
            //gets all products from dal
            productsFromDal = (List<DO.Item>)dal.Item.GetAll();
            for (int i = 0; i < productsFromDal.Count; i++)
            {
                temp.ItemId = productsFromDal[i].ID;
                temp.ItemName = productsFromDal[i].Name;
                temp.Category = (Dal.BookGenre)productsFromDal[i].Category;
                temp.ItemPrice = productsFromDal[i].Price;
                products.Add(temp);
            }
            return products;
        }
        public BO.Product GetProductForManager(int id)
        {
            BO.Product p = new BO.Product();
            if (id >= 100000)
            {
                DO.Item product = dal.Item.GetById(id);
                p.Name = product.Name;
                p.ID = product.ID;
                p.AmountInStock = product.AmountInStock;
                p.Price = product.Price;
                p.Category = (BL.BookGenre)product.Category;
                return p;
            }
            else
                throw new BlApi.BlEntityNotFoundException();
        }
        public BO.ProductItem GetProductForCustomer(int id, BO.Cart c)
        {
            BO.ProductItem p = new BO.ProductItem();
            if (id >= 100000)
            {
                int count = 0;
                DO.Item product = dal.Item.GetById(id);
                p.Name = product.Name;
                p.ID = product.ID;
                if (product.AmountInStock > 0)
                    p.IsAvailable = true;
                else
                    p.IsAvailable = false;
                p.Price = product.Price;
                p.Category = (BL.BookGenre)product.Category;
                if (c.Items.Count != null)
                {
                    for (int i = 0; i < c.Items.Count; i++)
                    {
                        if (id == c.Items[i].ItemId)
                            count++;
                    }
                }
                p.Amount = count;
                return p;
            }
            else
                throw new BlApi.BlEntityNotFoundException();
        }
        public void AddProduct(BO.Product p)
        {
            if (p.ID < 100000)
                throw new BlApi.NegativeIdException();
            if (p.Price < 0)
                throw new BlApi.NegativePriceException();
            if (p.Name == null || p.Category == null)
                throw new BlApi.EmptyStringException();
            if (p.AmountInStock < 0)
                throw new BlApi.NegativeAmountException();
            DO.Item item = new DO.Item();
            item.Price = p.Price;
            item.Name = p.Name;
            item.ID = p.ID;
            item.Category = (Dal.BookGenre)p.Category;
            item.AmountInStock = p.AmountInStock;
            try
            {
                dal.Item.Add(item);
            }
            catch (DalApi.EntityDuplicateException)
            {
                throw new BlApi.ExistsAlreadyException();
            }
        }
        public void RemoveProduct(int productId)
        {
            bool b = false;
            List<DO.OrderItem> oi = (List<DO.OrderItem>)dal.OrderItem.GetAll();
            List<DO.Order> o = (List<DO.Order>)dal.Order.GetAll();
            for (int i = 0; i < oi.Count(); i++)
            {
                if (oi[i].ItemId == productId)
                {
                    b = true;
                    for (int j = 0; j < o.Count(); j++)
                    {
                        if (oi[i].OrderID == o[j].OrderId)
                        {
                            if (o[j].DateDelivered == null)
                                throw new BlApi.ErrorDeleting();
                        }
                    }
                }
            }
            if (b == false)
                throw new BlApi.BlEntityNotFoundException();
            dal.Item.Delete(productId);
        }
        public void UpdateProduct(BO.Product p)
        {
            if (p.ID == 0)
                throw new BlApi.NegativeIdException();
            if (p.Price < 0)
                throw new BlApi.NegativePriceException();
            if (p.AmountInStock < 0)
                throw new BlApi.NegativeAmountException();
            if (p.Name == null || p.Category == null)
                throw new BlApi.EmptyStringException();
            DO.Item i = new DO.Item();
            i.Price = p.Price;
            i.Name = p.Name;
            i.AmountInStock = p.AmountInStock;
            i.ID = p.ID;
            i.Category = (Dal.BookGenre)p.Category;
            dal.Item.Update(i);
        }
    }
}
