using BlApi;
namespace BlImplementation;

internal class BLCart : ICart
{
    private DalApi.IDal dal = new Dal.DalList();
    public BO.Cart AddProduct(int productId, BO.Cart c)
    {
        DO.Item product = dal.Item.GetById(productId);
        if (!dal.Item.Available(productId))
            throw new NotInStockException();
        Action a=new Action(int id=>

        c.Items.ForEach()
        if (c.Items.Exists(product.ID))
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

    /*public BO.ProductItem GetProductDetails(BO.Cart C, int productId)
    {
        return 
    }*/

}
