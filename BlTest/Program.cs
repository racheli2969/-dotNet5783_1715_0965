

BlApi.IBl bl = new BlImplementation.Bl();



/*public enum OptionsForProduct
{
    Exit,
    GetProductList,
    GetProductForManager,
    GetProductForCustomer,
    AddProduct,
    RemoveProduct,
    UpdateProduct
}*/
void NavigateCart()
{
    int id;
    int amount;
    BL.OptionsForCart option;
    Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, AddToCart:1, UpdateProductQuantity:2, OrderConfirmation:3, ProductIndexInCart:4");
    BL.OptionsForCart.TryParse(Console.ReadLine(), out option);
    BO.Cart myCart = new BO.Cart();
    while (option != BL.OptionsForCart.Exit)
    {
        switch (option)
        {
            case BL.OptionsForCart.Exit:
                Console.WriteLine("see you a different time...");
                break;
            case BL.OptionsForCart.AddToCart:
                Console.Write("Enter id of product to add to cart");
                int.TryParse(Console.ReadLine(), out id);
                myCart = bl.Cart.AddToCart(id, myCart);
                Console.Write(myCart.ToString());
                break;
            case BL.OptionsForCart.UpdateProductQuantity:
                Console.Write("Enter id of product to add to cart and amount ");
                int.TryParse(Console.ReadLine(), out id);
                int.TryParse(Console.ReadLine(), out amount);
                myCart = bl.Cart.UpdateProductQuantity(id, myCart, amount);
                Console.Write(myCart.ToString());
                break;
            case BL.OptionsForCart.OrderConfirmation:
                string name, email, city, street;
                int numOfHouse;
                Console.Write("Almost done, we just need a few details to complete your order.\nPlease enter: name, email, city, street, numOfHouse");
                name = Console.ReadLine();
                email = Console.ReadLine();
                city = Console.ReadLine();
                street = Console.ReadLine();
                int.TryParse(Console.ReadLine(), out numOfHouse);
                bl.Cart.OrderConfirmation(myCart, name, email, city, street, numOfHouse);

                break;
            case BL.OptionsForCart.ProductIndexInCart:
                Console.Write("Please enter the id of product to search for in your cart");
                int.TryParse(Console.ReadLine(), out id);
                id = bl.Cart.ProductIndexInCart(myCart, id);
                Console.Write(myCart.Items[id].ToString());
                break;
        }
        Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, AddToCart:1, UpdateProductQuantity:2, OrderConfirmation:3, ProductIndexInCart:4");
    }
}
void NavigateOrder()
{
    BL.OptionsForOrder option;
    BO.Order order;
    int id;
    Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetOrderList:1, GetOrderDetails:2, UpdateOrderShipping:3, UpdateOrderDelivery:4, UpdateOrderDetails:5");
    BL.OptionsForOrder.TryParse(Console.ReadLine(), out option);
    while (option != BL.OptionsForOrder.Exit)
    {
        switch (option)
        {
            case BL.OptionsForOrder.Exit:
                Console.WriteLine("see you a different time...");
                break;
            case BL.OptionsForOrder.GetOrderList:
                List<BO.OrderForList> orders = (List<BO.OrderForList>)bl.Order.GetOrderList();
                if (orders.Count == 0)
                    Console.Write("No orders yet...");
                else
                    Console.Write("orders:");
                {
                    orders.ForEach(item =>
                    {
                        Console.WriteLine(item.ToString());
                    });
                }
                break;
            case BL.OptionsForOrder.GetOrderDetails:
                Console.Write("Enter id of order to search for");
                int.TryParse(Console.ReadLine(), out id);
                order = bl.Order.GetOrderDetails(id);
                Console.Write(order.ToString());
                break;
            case BL.OptionsForOrder.UpdateOrderShipping:
                Console.Write("Enter id of order to update shipping date for");
                int.TryParse(Console.ReadLine(), out id);
                order = bl.Order.UpdateOrderShipping(id);
                Console.Write(order.ToString());
                break;
            case BL.OptionsForOrder.UpdateOrderDelivery:
                Console.Write("Enter id of order to update delivery date for");
                int.TryParse(Console.ReadLine(), out id);
                order = bl.Order.UpdateOrderDelivery(id);
                Console.Write(order.ToString());
                break;
            case BL.OptionsForOrder.UpdateOrderDetails:
                int amount, orderId;
                Console.Write("Enter id of order to update,the amount and item to update ");
                int.TryParse(Console.ReadLine(), out orderId);
                int.TryParse(Console.ReadLine(), out id);
                int.TryParse(Console.ReadLine(), out amount);
                order = bl.Order.UpdateOrderDetails(orderId,id,amount);
                Console.Write(order.ToString());
                break;
        }
        Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetOrderList:1, GetOrderDetails:2, UpdateOrderShipping:3, UpdateOrderDelivery:4, UpdateOrderDetails:5");
    }
}
void NavigateProduct()
{

}
int Main()
{
    BL.OptionsForMain num;
    Console.WriteLine("welcome to the store, start testing...\nEnter a number between 0-3 as follows: 0 to Exit, 1 to Cart, 2 to Order, 3 to Product");
    BL.OptionsForMain.TryParse(Console.ReadLine(), out num);

    while (num < BL.OptionsForMain.Exit || num > BL.OptionsForMain.Product)
    {
        Console.WriteLine("Enter a number between 0-3 as follows: 0 to Exit, 1 to Cart, 2 to Order, 3 to Product");
        BL.OptionsForMain.TryParse(Console.ReadLine(), out num);
    }
    switch (num)
    {
        case BL.OptionsForMain.Exit:
            Console.WriteLine("guess you're not intrested in testing now...");
            break;
        case BL.OptionsForMain.Cart:
            NavigateCart();
            break;
        case BL.OptionsForMain.Order:
            NavigateOrder();
            break;
        case BL.OptionsForMain.Product:
            NavigateProduct();
            break;
        default:
            Console.WriteLine("unexpected error occured");
            break;
    }
    return 0;
}

Main();