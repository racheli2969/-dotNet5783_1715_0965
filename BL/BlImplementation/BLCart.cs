using System.Text.RegularExpressions;
namespace BlImplementation;

internal class BLCart : BlApi.ICart
{
 
    private DalApi.IDal? dal = DalApi.Factory.Get();

    public BO.Cart AddToCart(int productId, BO.Cart c)
    {
        //check if product exists if so get product
        try
        {
           List<DO.Item>? product = dal?.Item?.GetAll(x => x.ID == productId)?.ToList();
            //if not available
            if (dal?.Item?.Available(productId)==false)
                throw new BlApi.NotInStockException();
            //check if the item is in the cart already
            int idx = ProductIndexInCart(c, productId);
            if (idx >= 0)
            {
                //update the amount in the cart
                c.Items[idx].Amount++;
                c.Items[idx].PriceOfItems += c.Items[idx].ItemPrice;
            }
            else
            {    //add the product
                BO.OrderItem oi = new BO.OrderItem();
                oi.ItemId = product[0].ID;
                oi.ItemName = product[0].Name;
                oi.ItemPrice = product[0].Price;
                oi.Amount = 1;
                oi.PriceOfItems = product[0].Price;
               // oi.OrderItemId=
                c?.Items?.Add(oi);
            }
            double finalPrice = 0;
            foreach(BO.OrderItem item in c.Items)
            {
                finalPrice += item.ItemPrice;
            }
            //return updated cart
            c.FinalPrice = finalPrice;
            //c.Address =;
            //c.Email =;
            //c.CustomerName =;
            return c;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }
    public BO.Cart UpdateProductQuantity(int productId, BO.Cart c, int quantity)
    {
        int idx = ProductIndexInCart(c, productId);
        if (c?.Items?[idx].Amount + quantity == 0)
        {
            c.FinalPrice -= c.Items[idx].PriceOfItems;
            c.Items.RemoveAt(idx);
            return c;
        }
        if (quantity < 0 && c?.Items?[idx].Amount + quantity > 0)
        {
            c.Items[idx].Amount += quantity;
            c.FinalPrice += c.Items[idx].ItemPrice * quantity;
            c.Items[idx].PriceOfItems = c.Items[idx].Amount * c.Items[idx].ItemPrice;
            return c;
        }
        if (dal?.Item?.Available(productId, quantity)==false)
            throw new BlApi.NotInStockException();
        c.Items[idx].Amount += quantity;
        c.Items[idx].PriceOfItems += quantity * c.Items[idx].ItemPrice;
        c.FinalPrice += c.Items[idx].ItemPrice * quantity;
        return c;
    }

    public void OrderConfirmation(BO.Cart c, string name, string email, string city, string street, int numOfHouse)
    {
        if (name == null || email == null || city == null || street == null)
            throw new BlApi.EmptyStringException();
        if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new BlApi.WrongEmailFormatException();
        if (numOfHouse <= 0)
            throw new BlApi.NegativeHouseNumberException();
        c?.Items?.ForEach(validateItem);

        //create in dal layer and move the info
        DO.Order temp = new DO.Order();
        temp.CustomerName = name;
        temp.Email = email;
        temp.Address = city + " " + street + " " + numOfHouse;
        temp.DateOrdered = DateTime.Now;
        temp.DateDelivered = DateTime.MinValue;
        temp.DateReceived = DateTime.MinValue;
        //send to dal
        int id = dal.Order.Add(temp);

        //add the items to the order item array and update the products accordingly
        DO.OrderItem tempItem = new DO.OrderItem();
        for (int i = 0; i < c?.Items?.Count; i++)
        {
            //create order items in dal layer 
            tempItem.Amount = c.Items[i].Amount;
            tempItem.OrderID = temp.OrderId;
            tempItem.ItemId = c.Items[i].ItemId;
            id = dal.OrderItem.Add(tempItem);
            tempItem.OrderItemId = id;
            c.Items[i].OrderItemId = id;
            dal.Item.Update(tempItem.ItemId, tempItem.Amount);
        }
    }

    /// <summary>
    /// gets a cart and product id and searches for the product in the cart
    /// </summary>
    /// <param name="C">the cart to search in</param>
    /// <param name="productId">the product id to search for</param>
    /// <returns>if the product exists returns index else -1</returns>
    private int ProductIndexInCart(BO.Cart c, int productId)
    {
        if (c?.Items?.Count == 0||c?.Items==null)
            return -1;
        return c.Items.FindIndex(orderItem => orderItem.ItemId == productId);
    }

    private void validateItem(BO.OrderItem oi)
    {
        //product exists
        dal?.Item?.GetAll(o=>o.ID==oi.ItemId)?.ToList();
        //amount is a positive number
        if (oi.Amount <= 0)
            throw new BlApi.NegativeAmountException();
        //item is in stock
        if (dal?.Item?.Available(oi.ItemId, oi.Amount)==false)
            throw new BlApi.NotInStockException();
    }
}
