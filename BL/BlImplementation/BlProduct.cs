﻿
namespace BlImplementation;
/// <summary>
/// implementation of BlApiProduct
/// </summary>
public class BlProduct : BlApi.IProduct
{
    private DalApi.IDal dal = new Dal.DalList();
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        List<BO.ProductForList> products = new List<BO.ProductForList>();
        List<DO.Item> productsFromDal = new List<DO.Item>();
        BO.ProductForList temp;
        //gets all products from dal
        productsFromDal = (List<DO.Item>)dal.Item.GetAll();
        for (int i = 0; i < productsFromDal.Count; i++)
        {
            temp = new BO.ProductForList();
            temp.ItemId = productsFromDal[i].ID;
            temp.ItemName = productsFromDal[i].Name;
            temp.Category = (BO.BookGenre)(DO.BookGenre)productsFromDal[i].Category;
            temp.ItemPrice = productsFromDal[i].Price;
            products.Add(temp);
        }
        return products;
    }
    public BO.Product GetProductForManager(int id)
    {
        try
        {
            BO.Product p = new BO.Product();
            if (id < 100000)
                throw new BlApi.NegativeIdException();
            DO.Item product = dal.Item.GetById(id);
            p.Name = product.Name;
            p.ID = product.ID;
            p.AmountInStock = product.AmountInStock;
            p.Price = product.Price;
            p.Category = (BO.BookGenre)product.Category;
            return p;
        }
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }
    }
    public BO.ProductItem GetProductForCustomer(int id, BO.Cart c)
    {
        try
        {
            BO.ProductItem p = new BO.ProductItem();
            if (id < 100000)
                throw new BlApi.NegativeIdException();
            int count = 0;
            DO.Item product = dal.Item.GetById(id);
            p.Name = product.Name;
            p.ID = product.ID;
            if (product.AmountInStock > 0)
                p.IsAvailable = true;
            else
                p.IsAvailable = false;
            p.Price = product.Price;
            p.Category = (BO.BookGenre)product.Category;
            if (c.Items != null)
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
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }
    }
    public void AddProduct(BO.Product p)
    {
        try
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
            item.Category = (DO.BookGenre)p.Category;
            item.AmountInStock = p.AmountInStock;
            dal.Item.Add(item);
        }
        catch (DalApi.EntityDuplicateException)
        {
            throw new BlApi.ExistsAlreadyException();
        }
    }
    public void RemoveProduct(int productId)
    {
        try
        {
            List<DO.OrderItem> orderItems = (List<DO.OrderItem>)dal.OrderItem.GetAll();
            List<DO.Order> orders = (List<DO.Order>)dal.Order.GetAll();

            try
            {
                //take all the order items with the id of product to delete if there are none will throw error but is a sign that it's not in any order and we can try deleting
                List<DO.OrderItem> productToDeleteAsOrderItems = orderItems.FindAll(item => item.ItemId == productId);
                //check if the product is in any order if so throws exception
                productToDeleteAsOrderItems.ForEach((item) =>
                {
                    if (orders.FindIndex(order => order.OrderId == item.OrderID) > 0)
                        throw new BlApi.ErrorDeleting();
                });
                dal.Item.Delete(productId);
            }

            catch (ArgumentNullException)
            {
                dal.Item.Delete(productId);
            }
        }
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }
    }
    public void UpdateProduct(BO.Product p)
    {
        try
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
            i.Category = (DO.BookGenre)p.Category;
            dal.Item.Update(i);
        }
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }

    }
}
