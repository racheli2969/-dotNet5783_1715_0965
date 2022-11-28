
using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// gets all the products
    /// </summary>
    /// <returns>products</returns>
    public IEnumerable<ProductForList> GetProductList();
    /// <summary>
    /// gets id of product and searches for it in the BL layer if id does not exist will throw exception
    /// </summary>
    /// <param name="id">indetifier for product</param>
    /// <returns>the product for manager</returns>
    public Product GetProductForManager(int id);
    /// <summary>
    /// gets id of product and searches for it in the BL layer if id does not exist will throw exception
    /// </summary>
    /// <param name="id">indetifier for product</param>
    /// <returns>the product for Customer</returns>
    public ProductItem GetProductForCustomer(int id);
    /// <summary>
    /// gets a product and adds it to the product list if information is not valid will throw exception
    /// </summary>
    /// <param name="p">new product to add</param>
    public void AddProduct(Product p);
    /// <summary>
    /// deletes a product from the product list if id does not exist or product exists in orders will throw exception
    /// </summary>
    /// <param name="productId">product to delete</param>
    public void RemoveProduct(int productId);
    /// <summary>
    /// updates the product to the new info if the information is not valid will throw exception
    /// </summary>
    /// <param name="p">product to update</param>
    public void UpdateProduct(Product p);


}
