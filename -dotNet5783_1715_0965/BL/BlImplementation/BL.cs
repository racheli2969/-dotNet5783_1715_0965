using BlApi;
namespace BlImplementation;
sealed public class Bl : IBl
{ 
   public ICart Cart => new BLCart();
    public IOrder Order => new BlOrder();
    public IProduct Product => new BlProduct();
}