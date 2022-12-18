namespace BlApi;
public static class Factory
{
    public static IBl MyProperty
    {
        get;
    } = new BlImplementation.Bl();
}

