/// <summary>
///defines all the enums for BL layer
/// </summary>
namespace BL;


 /// <summary>
 /// enum for delivery state
 /// </summary>
 public enum EnumOrderStatus
{
    Ordered,
    Delivered,
    Received
}
public enum BookGenre
{
    Fiction,
    History,
    Biography,
    Fantasy,
    SciFi,
    Mystery,
    Thriller
}

public enum OptionsForMain
{
    Exit,
    Cart,
    Order,
    Product
}
public enum OptionsOfActions
{
    Exit = 0,
    AddItem = 'a',
    GetById = 'b',
    GetAll = 'c',
    UpdateItem = 'd',
    DeleteItem = 'e'
}