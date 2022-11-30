

BlApi.IBl bl = new BlImplementation.Bl();
/*static void Navigate(int type)
{
    Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit = 0,Add = 'a',GetById = 'b',
    GetAll = 'c',
    UpdateItem = 'd',
    DeleteItem = 'e'")

}*/
static void NavigateCart()
{
    Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit = 0,Add = 'a',GetById = 'b', GetAll = 'c', UpdateItem = 'd', DeleteItem = 'e'")


}
static void NavigateOrder()
{

}
static void NavigateProduct()
{

}
int Main()
{
    BL.OptionsForMain num;
    Console.WriteLine("welcome to the store, start testing...\nEnter a number between 0-3 as follows: 0 to Exit, 1 to Cart, 2 to Order, 3 to Product");
    BL.OptionsForMain.TryParse(Console.ReadLine(), out num);

    while (num <BL.OptionsForMain.Exit || num > BL.OptionsForMain.Product)
    {
        Console.WriteLine("Enter a number between 0-3 as follows: 0 to Exit, 1 to Cart, 2 to Order, 3 to Product");
        BL.OptionsForMain.TryParse(Console.ReadLine(), out num);
    }
    switch (num)
    {
        case BL.OptionsForMain.Exit:Console.WriteLine("guess you're not intrested in testing now...");
            break;
        case BL.OptionsForMain.Cart:NavigateCart();
            break;
        case BL.OptionsForMain.Order:NavigateOrder();
            break;
        case BL.OptionsForMain.Product:NavigateProduct();
            break;
        default: Console.WriteLine("unexpected error occured");
            break;
    }
    return 0;
}

Main();