
namespace BlImplementation;
sealed public class Bl : BlApi.IBl
{ 
    public BlApi.ICart Cart => new BlImplementation.BLCart();
    public BlApi.IOrder Order => new BlImplementation.BlOrder();
    public BlApi.IProduct Product => new BlImplementation.BlProduct();
}