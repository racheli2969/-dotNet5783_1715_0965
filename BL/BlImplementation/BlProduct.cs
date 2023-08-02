
using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;
/// <summary>
/// implementation of BlApiProduct
/// </summary>
public class BlProduct : BlApi.IProduct
{
    private DalApi.IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList?> GetProductList(BO.BookGenre? category)
    {
        List<BO.ProductForList>? products = new();
        List<DO.Item>? productsFromDal = new();
        BO.ProductForList temp;
        //gets all products from dal
        lock (dal ?? throw new DalApi.NullObject())
        {
            if (category != null)
                productsFromDal = dal?.Item?.GetAll(o => ((BO.BookGenre)o.Category).CompareTo(category) == 0)?.ToList();
            else
                productsFromDal = dal?.Item?.GetAll()?.ToList();
        }
        for (int i = 0; i < productsFromDal?.Count; i++)
        {
            temp = new BO.ProductForList();
            temp.ItemId = productsFromDal[i].ID;
            temp.ItemName = productsFromDal[i].Name;
            temp.Category = (BO.BookGenre)productsFromDal[i].Category;
            temp.ItemPrice = productsFromDal[i].Price;
            products.Add(temp);
        }
        return products ?? throw new BlApi.BlEntityNotFoundException();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product GetProductForManager(int id)
    {
        try
        {
            BO.Product p = new BO.Product();
            if (id < 100000)
                throw new BlApi.NegativeIdException();
            lock (dal ?? throw new DalApi.NullObject())
            {
                List<DO.Item>? productFromDal = dal?.Item.GetAll(i => i.ID == id)?.ToList();

                p.Name = productFromDal?[0].Name;
                p.ID = (productFromDal?? throw new NullObject())[0].ID;
                p.AmountInStock = productFromDal[0].AmountInStock;
                p.Price = productFromDal[0].Price;
                p.Category = (BO.BookGenre)productFromDal[0].Category;
                return p;
            }
        }
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetProductForCustomer(int id, BO.Cart c)
    {
        try
        {
            BO.ProductItem p = new BO.ProductItem();
            if (id < 100000)
                throw new BlApi.NegativeIdException();
            int count = 0;
            lock (dal ?? throw new DalApi.NullObject())
            {
                List<DO.Item>? product = dal?.Item?.GetAll(i => i.ID == id)?.ToList();
                p.Name = product?[0].Name;
                if (product != null)
                {
                    p.ID = product[0].ID;
                    if (product[0].AmountInStock > 0)
                        p.IsAvailable = true;
                    else
                        p.IsAvailable = false;
                    p.Price = product[0].Price;
                    p.Category = (BO.BookGenre)product[0].Category;
                }
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
        }
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int AddProduct(BO.Product p)
    {
        try
        {
            if (p.Price < 0)
                throw new BlApi.NegativePriceException();
            if (p.Name == null || p.Category.ToString() == null)
                throw new BlApi.EmptyStringException();
            if (p.AmountInStock < 0)
                throw new BlApi.NegativeAmountException();
            DO.Item item = new DO.Item();
            item.Price = p.Price;
            item.Name = p.Name;
            item.ID = p.ID;
            item.Category = (DO.BookGenre)p.Category;
            item.AmountInStock = p.AmountInStock;
            int id;
            lock (dal ?? throw new NullObject())
            {
                id = dal.Item.Add(item);
            }
            return id;
        }
        catch (DalApi.EntityDuplicateException)
        {
            throw new BlApi.ExistsAlreadyException();
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void RemoveProduct(int productId)
    {
        try
        {
            lock (dal ?? throw new NullObject())
            {
                List<DO.OrderItem>? orderItems = dal?.OrderItem?.GetAll(i => i.ItemId == productId)?.ToList();
                List<DO.Order>? orders = dal?.Order?.GetAll(o => orderItems?.FindIndex(i => i.OrderID == o.OrderId) > 0)?.ToList();


                if (orders?.Count < 0)
                    throw new BlApi.ErrorDeleting();
                dal?.Item.Delete(productId);

            }
        }
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
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
            if (p.Name == null || p.Category.ToString() == null)
                throw new BlApi.EmptyStringException();
            DO.Item i = new DO.Item();
            i.Price = p.Price;
            i.Name = p.Name;
            i.AmountInStock = p.AmountInStock;
            i.ID = p.ID;
            i.Category = (DO.BookGenre)p.Category;
            lock (dal ?? throw new NullObject())
            {
                dal?.Item.Update(i);
            }
        }
        catch (DalApi.EntityNotFoundException ex)
        {
            throw new BlApi.BlEntityNotFoundException(ex);
        }

    }

}
