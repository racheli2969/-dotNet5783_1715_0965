using BO;
namespace BlApi;

public interface ICart
{
    /// <summary>
    /// gets a product id and adds it to the cart throws if there isn't enough in stock or not valid info
    /// </summary>
    /// <param name="productId">gets product id</param>
    /// <param name="c">gets the cart</param>
    /// <returns> the updated cart</returns>
    public Cart AddToCart(int productId, Cart c);
  /// <summary>
  /// gets a product id the cart and quantity and updates the amount to the quantity
  /// </summary>
  /// <param name="productId">product id</param>
  /// <param name="c">the cart to update</param>
  /// <param name="quantity"> the quantity to update to</param>
  /// <returns></returns>
    public Cart UpdateProductQuantity(int productId, Cart c, int quantity);
    /// <summary>
    /// gets the customer details and his cart if all the info is valid will create an order if not will throw exception
    /// </summary>
    /// <param name="c">the cart</param>
    /// <param name="name">customer name</param>
    /// <param name="email">customer email</param>
    /// <param name="city">customer city</param>
    /// <param name="street">customer street</param>
    /// <param name="numOfHouse">customer num of house</param>
    public int OrderConfirmation(Cart cart);
    /// <summary>
    /// gets a cart and product id and searches for the product in the cart
    /// </summary>
    /// <param name="C">the cart to search in</param>
    /// <param name="productId">the product id to search for</param>
    /// <returns>if the product exists returns index else -1</returns>
    // private int ProductIndexInCart(Cart C, int productId);     
}
