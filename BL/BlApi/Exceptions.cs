
namespace BlApi;


public class ProductNotFoundException : Exception
{
    public override string Message => "Book not found";
}
public class ProductExistsException : Exception
{
    public override string Message => "Product already exists";

}
public class EmptyStringException : Exception
{
    public override string Message => "please fill in all the details";
}
public class NegativeIdException: Exception
{
    public override string Message => " Id is a positive number";
}

public class NegativePriceException : Exception
{
    public override string Message => " Price is a positive number";
}

public class NegativeAmountdException : Exception
{
    public override string Message => " Amount is a positive number";
}

public class NotInStockException : Exception
{
    public override string Message => "Sorry, not in stock";
}