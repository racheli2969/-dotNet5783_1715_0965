using BlApi;
namespace BlImplementation;

internal class BLCart : ICart
{
    private DalApi.IDal dal = new Dal.DalList();
    public BO.Cart AddProduct(int productId, BO.Cart c)
    {
        DO.Item product = dal.Item.GetById(productId);
        if (!dal.Item.Available(productId))
            throw new NotAmountInStockException();
        //check if the item is in the cart already
        int idx = ProductIndexInCart(c, productId);
        if (idx > 0)
        {
            c.Items[idx].Amount++;
        }
        else
        {
           // c.Items.Add(product);
        }
        //add to cart approvingly
        //return updated cart
        return c;
    }

    public BO.Cart UpdateProductQuantity(int productId, BO.Cart c, int quantity)
    {
        return c;
    }

    public void OrderConfirmation(BO.Cart c, string name, string email, string city, string street, int numOfHouse)
    {

    }

    public int ProductIndexInCart(BO.Cart C, int productId)
    {
        //search in c.items
        return -1;
    }

}
