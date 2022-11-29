using BlApi;
using BO;
using Dal;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class BLCart : ICart
{
    private DalApi.IDal dal = new Dal.DalList();
    public BO.Cart AddProduct(int productId, BO.Cart c)
    {
        //check if product exists if so get product
        DO.Item product = dal.Item.GetById(productId);
        //if not available
        if (!dal.Item.Available(productId))
            throw new NotInStockException();
        //check if the item is in the cart already
        int idx = ProductIndexInCart(c, productId);
        if (idx >= 0)
        {
            //update the amount in the cart
            c.Items[idx].Amount++;
            c.Items[idx].PriceOfItems = c.Items[idx].Amount * c.Items[idx].ItemPrice;
        }
        else
        {    //add the product
            BO.OrderItem oi = new BO.OrderItem();
            oi.ItemId = product.ID;
            oi.ItemName = product.Name;
            oi.ItemPrice = product.Price;
            oi.Amount = 1;
            oi.PriceOfItems = product.Price;
            c.Items.Add(oi);
        }
        //return updated cart
        return c;
    }

    public BO.Cart UpdateProductQuantity(int productId, BO.Cart c, int quantity)
    {
        int idx = ProductIndexInCart(c, productId);
        if (c.Items[idx].Amount + quantity == 0)
        {
            c.Items.RemoveAt(idx);
            return c;
        }
        if (quantity < 0 && c.Items[idx].Amount + quantity > 0)
        {
            c.Items[idx].Amount += quantity;
            c.Items[idx].PriceOfItems = c.Items[idx].Amount * c.Items[idx].ItemPrice;
            return c;
        }
        if (!dal.Item.Available(productId, quantity))
            throw new NotInStockException();
        c.Items[idx].Amount += quantity;
        c.Items[idx].PriceOfItems = c.Items[idx].Amount * c.Items[idx].ItemPrice;
        return c;
    }

    public void OrderConfirmation(BO.Cart c, string name, string email, string city, string street, int numOfHouse)
    {
        if (name == null || email == null || city == null || street == null)
            throw new EmptyStringException();
        if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new WrongEmailFormatException();
        if (numOfHouse <= 0)
            throw new NegativeHouseNumberException();
        c.Items.ForEach(validateItem);
    }

    public int ProductIndexInCart(BO.Cart c, int productId)
    {
        for (int i = 0; i < c.Items.Count; i++)
        {
            if (c.Items[i].ItemId == productId)
                return i;
        }
        return -1;
    }
    private void validateItem(BO.OrderItem oi)
    {
        //product exists
        dal.Item.GetById(oi.ItemId);
        //amount is a positive number
        if (oi.Amount <= 0)
            throw new NegativeAmountException();
        //item is in stock
        if (!dal.Item.Available(oi.ItemId, oi.Amount))
            throw new NotInStockException();
    }
}
