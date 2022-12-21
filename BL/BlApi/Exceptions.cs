
namespace BlApi;

/// <summary>
/// Exception for not found
/// </summary>
public class BlEntityNotFoundException : Exception
{
    public BlEntityNotFoundException(DalApi.EntityNotFoundException? inner = null) : base("entity not found", inner) { }
    public override string Message =>
                    "entity not found";
}
/// <summary>
/// Exception for duplicated value
/// </summary>


    public class ExistsAlreadyException : Exception
{
    public ExistsAlreadyException(DalApi.EntityDuplicateException? inner = null) : base("Exists already", inner) { }

    public override string Message => "Product already exists";

}
/// <summary>
/// Exception for missing string type info
/// </summary>
public class EmptyStringException : Exception
{
    public override string Message => "please fill in all the details";
}
/// <summary>
/// Exception for negative id value
/// </summary>
public class NegativeIdException : Exception
{
    public override string Message => " Id is a positive number bigger than 100000";
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
    public override string Message => " HouseNumber is a positive number";
}
/// <summary>
/// Exception for not in stock
/// </summary>
public class NotInStockException : Exception
{
    public override string Message => "Sorry, not in stock";
}

    public class NotInCartException : Exception
{
    public override string Message => "Sorry, not in cart";
}
public class ErrorDeleting : Exception
{
    public override string Message => "Sorry, it is not possible to delete this product";
}
public class SentAlreadyException : Exception
{
    public override string Message => "The order has already been sent";
}
public class deliveredAlreadyException : Exception
{
    public override string Message => "The order has already been delivered";
}