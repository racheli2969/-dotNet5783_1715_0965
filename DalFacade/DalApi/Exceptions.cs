
namespace DalApi;

public class EntityNotFoundException:Exception
{
    public override string Message => "Entity not found";
}
public class EntityDuplicateException : Exception 
{
    public override string Message => "Already exists";

} 
public class NegativeAmount:Exception
{
    public override string Message => "Amount cant be a negative number";
}


