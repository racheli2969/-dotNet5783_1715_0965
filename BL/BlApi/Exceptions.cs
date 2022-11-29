
namespace BlApi;

/// <summary>
/// Exception for not found
/// </summary>
public class ProductNotFoundException : Exception
{
    public override string Message => "Book not found";
}
/// <summary>
/// Exception for duplicated value
/// </summary>
public class ProductExistsException : Exception
{
    public override string Message => "Product already exists";

}
/// <summary>
/// Exception for missing info
/// </summary>
public class EmptyStringException : Exception
{
    public override string Message => "please fill in all the details";
}
/// <summary>
/// Exception for negative id value
/// </summary>
public class NegativeIdException: Exception
{
    public override string Message => " Id is a positive number";
}
/// <summary>
/// Exception for negative price value
/// </summary>
public class NegativePriceException : Exception
{
    public override string Message => " Price is a positive number";
}
/// <summary>
/// Exception for negative amount value
/// </summary>
public class NegativeAmountException : Exception
{
    public override string Message => " Amount is a positive number";
}
public class NegativeHouseNumberException : Exception
{
    public override string Message => " Amount is a positive number";
}
/// <summary>
/// Exception for not in stock
/// </summary>
public class NotInStockException : Exception
{
    public override string Message => "Sorry, not in stock";
}
public class WrongEmailFormatException : Exception
{
    public override string Message => "wrong email format, email should be valid";
}