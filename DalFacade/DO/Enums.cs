
namespace DO;

/// <summary>
/// file for all enums in project
/// </summary>
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
public enum BookCategory
{ 
    Children,
    Teens,
    Adults
}
public enum OptionsForMain
{
    Exit,
    Item,
    Order,
    OrderItem
}
public enum OptionsOfActions
{   
    Exit=0,
    AddItem='a',
    ViewById='b',
    ViewAll='c',
    UpdateItem='d',
    DeleteItem='e'
}