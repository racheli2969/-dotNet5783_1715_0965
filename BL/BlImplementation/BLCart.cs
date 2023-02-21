using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace BlImplementation;

internal class BLCart : BlApi.ICart
{

    private DalApi.IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart AddToCart(int productId, BO.Cart c)
    {
        //check if product exists if so get product
        try
        {
            DO.Item? product = dal?.Item?.GetAll(x => x.ID == productId)?.ToList().FirstOrDefault();
            //if not available
            if (dal?.Item?.Available(productId) == false)
                throw new BlApi.NotInStockException();
            //check if the item is in the cart already
            int idx = ProductIndexInCart(c, productId);
            if (idx >= 0)
            {
                //update the amount in the cart                 
                ((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].Amount++;
                ((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].PriceOfItems
                    += ((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].ItemPrice;
            }
            else
            {    //add the product
                BO.OrderItem oi = new BO.OrderItem();
                if (product != null)
                {
                    oi.ItemId = product.Value.ID;
                    oi.ItemName = product.Value.Name;
                    oi.ItemPrice = product.Value.Price;
                    oi.Amount = 1;
                    oi.PriceOfItems = product.Value.Price;
                    if (c!=null&&c.Items == null)
                    {
                        List<BO.OrderItem> temp = new();
                        temp.Add(oi);
                        c.Items = temp;
                    }
                    else
                        c?.Items?.Add(oi);
                }
            }
            if (c?.Items != null)
                c.FinalPrice = c.Items.Select(i => i.ItemPrice).Sum();
            //return updated cart
            return c ?? throw new BlApi.BlNOtImplementedException();
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlApi.BlEntityNotFoundException();
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart UpdateProductQuantity(int productId, BO.Cart c, int quantity)
    {
        //check if the product is in the cart
        int idx = ProductIndexInCart(c, productId);
        if (idx == -1)
            throw new BlApi.NotInCartException();
        //if the new amount is a negative number
        if (quantity < 0)
            throw new BlApi.NegativeAmountException();
        //if the new amount is zero
        if (quantity == 0)
        {
            if (c != null && c.Items != null)
            {

                c.FinalPrice -= c.Items[idx].PriceOfItems;
                c.Items.RemoveAt(idx);
                return c;
            }
        }
        //if the added amount is smaller then the current amount
        if (c?.Items?[idx].Amount - quantity > 0)
        {
            if (c != null && c.Items != null)
            {

                //reduce the amount in the cart
                c.FinalPrice -= c.Items[idx].ItemPrice * (c.Items[idx].Amount -quantity);
                c.Items[idx].Amount = quantity;
                c.Items[idx].PriceOfItems = c.Items[idx].Amount * c.Items[idx].ItemPrice;

                return c;
            }
        }
        //if the added amount is bigger then the current amount then needed to check if there is enough in stock
        if (dal?.Item?.Available(productId, quantity) == false)
        {
            throw new BlApi.NotInStockException();
        }       
        //if in stock add the difference to the cart then calculates the final price 
        (c ?? throw new BlApi.BlNOtImplementedException()).FinalPrice
            += ((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].ItemPrice
            * (((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].Amount - quantity);

        ((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].Amount = quantity;

        ((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].PriceOfItems
            = quantity
            * ((c ?? throw new BlApi.BlNOtImplementedException()).Items ?? throw new BlApi.BlNOtImplementedException())[idx].ItemPrice;
        return c;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void OrderConfirmation(BO.Cart cart)
    {
        //check all information was received
        if (cart.CustomerName == null || cart.Email == null || cart.City == null || cart.Street == null)
            throw new BlApi.EmptyStringException();
        //validates the email address
        MailAddress m = new(cart.Email);
        if (cart.NumOfHouse <= 0)
            throw new BlApi.NegativeHouseNumberException();
        if (cart?.Items?.Count() == 0)
            throw new BlApi.NoItemsInCartException();
        cart?.Items?.ForEach(validateItem);
        //create in dal layer and move the info
        DO.Order temp = new DO.Order();
        temp.CustomerName = cart.CustomerName;
        temp.Email = cart.Email;
        temp.Address = cart.City + " " + cart.Street + " " + cart.NumOfHouse;
        temp.DateOrdered = DateTime.Now;
        temp.DateShipped = DateTime.MinValue;
        temp.DateDelivered = DateTime.MinValue;
        //send to dal
        if (dal != null)
        {
            int id = dal.Order.Add(temp);

            //add the items to the order item array and update the products accordingly
            DO.OrderItem tempItem = new DO.OrderItem();
            for (int i = 0; i < cart?.Items?.Count; i++)
            {
                //create order items in dal layer 
                tempItem.Amount = cart.Items[i].Amount;
                tempItem.OrderID = temp.OrderId;
                tempItem.ItemId = cart.Items[i].ItemId;
                //adds to order items
                id = dal.OrderItem.Add(tempItem);
                tempItem.OrderItemId = id;
                cart.Items[i].OrderItemId = id;
                //updates amount
                dal.Item.Update(tempItem.ItemId, tempItem.Amount);
            }
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
        if (c?.Items?.Count == 0 || c?.Items == null)
            return -1;
        return c.Items.FindIndex(orderItem => orderItem.ItemId == productId);
    }

    private void validateItem(BO.OrderItem oi)
    {
        //product exists
        dal?.Item?.GetAll(o => o.ID == oi.ItemId)?.ToList();
        //amount is a positive number
        if (oi.Amount <= 0)
            throw new BlApi.NegativeAmountException();
        //item is in stock
        if (dal?.Item?.Available(oi.ItemId, oi.Amount) == false)
            throw new BlApi.NotInStockException();
    }
}
