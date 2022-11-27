
namespace DalApi;

public class EntityNotFoundException:Exception
{
    public override string Message => "Entity not found";
}
public class EntityDuplicateException : Exception 
{
    public override string Message => "Already exists";

}

