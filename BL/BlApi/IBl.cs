
namespace BlApi;
/// <summary>
/// contains all the interfaces of BL
/// </summary>
public interface IBl
{ 
    /// <summary>
/// attribute of type product interface 
/// </summary>
    public IProduct Product { get; }
    /// <summary>
    ///attribute of type order interface 
    /// </summary>
    public IOrder Order { get; }
    /// <summary>
    /// attribute of type Cart interface 
    /// </summary>
    public ICart Cart { get; }
}
