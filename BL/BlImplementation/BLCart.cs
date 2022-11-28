using BlApi;
namespace BlImplementation;

internal class BLCart:ICart
{
    private DalApi.IDal dal = new DalList();
    public BO.Cart AddProduct(int productId, BO.Cart c)
    {
        //find if id exists
        //check if there is enough in stock
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
