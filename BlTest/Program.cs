
BlApi.IBl bl = new BlImplementation.Bl();
BO.Cart myCart = new();
myCart.Items = new();

void NavigateCart()
{
    int id;
    int amount;
    BO.OptionsForCart option;
    Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, AddToCart:1, UpdateProductQuantity:2, OrderConfirmation:3, ProductIndexInCart:4\n");
    BO.OptionsForCart.TryParse(Console.ReadLine(), out option);

    while (option != BO.OptionsForCart.Exit)
    {
        try
        {
            switch (option)
            {
                case BO.OptionsForCart.Exit:
                    Console.WriteLine("see you a different time...\n");
                    break;
                case BO.OptionsForCart.AddToCart:
                    Console.Write("Enter id of product to add to cart\n");
                    int.TryParse(Console.ReadLine(), out id);
                    myCart = bl.Cart.AddToCart(id, myCart);
                    Console.Write(myCart.ToString());
                    break;
                case BO.OptionsForCart.UpdateProductQuantity:
                    Console.Write("Enter id of product to add to cart and amount \n");
                    int.TryParse(Console.ReadLine(), out id);
                    int.TryParse(Console.ReadLine(), out amount);
                    myCart = bl.Cart.UpdateProductQuantity(id, myCart, amount);
                    Console.Write(myCart.ToString());
                    break;
                case BO.OptionsForCart.OrderConfirmation:
                    string? name, email, city, street;
                    int numOfHouse;
                    Console.Write("Almost done, we just need a few details to complete your order.\nPlease enter: name, email, city, street, numOfHouse\n");
                    name = Console.ReadLine();
                    email = Console.ReadLine();
                    city = Console.ReadLine();
                    street = Console.ReadLine();
                    int.TryParse(Console.ReadLine(), out numOfHouse);
                    bl.Cart.OrderConfirmation(myCart, name, email, city, street, numOfHouse);
                    Console.WriteLine("order created successfully");
                    break;
            }
            Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, AddToCart:1, UpdateProductQuantity:2, OrderConfirmation:3\n");
            BO.OptionsForCart.TryParse(Console.ReadLine(), out option);
        }
        catch (BlApi.ExistsAlreadyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.BlEntityNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.EmptyStringException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NegativeIdException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NegativeHouseNumberException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NegativeAmountException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NegativePriceException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NotInStockException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch(BlApi.NotInCartException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (FormatException ex)
        {
           Console.WriteLine(ex.Message);
        }
        catch
        {
            Console.WriteLine("unexplained error occured\n");
        }
        Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, AddToCart:1, UpdateProductQuantity:2, OrderConfirmation:3\n");
        BO.OptionsForCart.TryParse(Console.ReadLine(), out option);
    }

}
void NavigateOrder()
{
    BO.OptionsForOrder option;
    BO.Order order;
    int id;
    Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetOrderList:1, GetOrderDetails:2, UpdateOrderShipping:3, UpdateOrderDelivery:4, UpdateOrderDetails:5\n");
    BO.OptionsForOrder.TryParse(Console.ReadLine(), out option);

    while (option != BO.OptionsForOrder.Exit)
    {
        try
        {
            switch (option)
            {
                case BO.OptionsForOrder.Exit:
                    Console.WriteLine("see you a different time...");
                    break;
                case BO.OptionsForOrder.GetOrderList:
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
                case BO.OptionsForOrder.GetOrderDetails:
                    Console.Write("Enter id of order to search for");
                    int.TryParse(Console.ReadLine(), out id);
                    order = bl.Order.GetOrderDetails(id);
                    Console.Write(order);
                    break;
                case BO.OptionsForOrder.UpdateOrderShipping:
                    Console.Write("Enter id of order to update shipping date for");
                    int.TryParse(Console.ReadLine(), out id);
                    order = bl.Order.UpdateOrderShipping(id);
                    Console.Write(order.ToString());
                    break;
                case BO.OptionsForOrder.UpdateOrderDelivery:
                    Console.Write("Enter id of order to update delivery date for\n");
                    int.TryParse(Console.ReadLine(), out id);
                    order = bl.Order.UpdateOrderDelivery(id);
                    Console.Write(order.ToString());
                    break;
                case BO.OptionsForOrder.UpdateOrderDetails:
                    int amount, orderId;
                    Console.Write("Enter id of order to update,the amount and item to update \n");
                    int.TryParse(Console.ReadLine(), out orderId);
                    int.TryParse(Console.ReadLine(), out id);
                    int.TryParse(Console.ReadLine(), out amount);
                    order = bl.Order.UpdateOrderDetails(orderId, id, amount);
                    Console.Write(order.ToString());
                    break;
            }
            Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetOrderList:1, GetOrderDetails:2, UpdateOrderShipping:3, UpdateOrderDelivery:4, UpdateOrderDetails:5\n");
            BO.OptionsForOrder.TryParse(Console.ReadLine(), out option);
        }
        catch (BlApi.ExistsAlreadyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.BlEntityNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.deliveredAlreadyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.SentAlreadyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch
        {
            Console.WriteLine("unexplained error occured");
        }
        Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetOrderList:1, GetOrderDetails:2, UpdateOrderShipping:3, UpdateOrderDelivery:4, UpdateOrderDetails:5\n");
        BO.OptionsForOrder.TryParse(Console.ReadLine(), out option);

    }

}
void NavigateProduct()
{
    BO.OptionsForProduct option;
    BO.Order order;
    int id;
    BO.Product product = new BO.Product();
    Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetProductList:1, GetProductForManager:2, GetProductForCustomer:3, AddProduct:4, RemoveProduct:5, UpdateProduct:6\n");
    BO.OptionsForProduct.TryParse(Console.ReadLine(), out option);
    List<BO.ProductForList> products = new List<BO.ProductForList>();
    while (option != BO.OptionsForProduct.Exit)
    {
        try
        {
            switch (option)
            {
                case BO.OptionsForProduct.Exit:
                    Console.WriteLine("see you a different time...\n");
                    break;
                case BO.OptionsForProduct.GetProductList:

                    products = (List<BO.ProductForList>)bl.Product.GetProductList(null);
                    if (products.Count == 0)
                        Console.Write("No products yet...\n");
                    else
                        Console.Write("products:\n");
                    {
                        products.ForEach(item =>
                        {
                            Console.WriteLine(item.ToString());
                        });
                    }
                    break;
                case BO.OptionsForProduct.GetProductForManager:
                    Console.Write("Enter id of product to search for\n");
                    int.TryParse(Console.ReadLine(), out id);
                    BO.Product product1 = bl.Product.GetProductForManager(id);
                    Console.Write(product1.ToString());
                    break;
                case BO.OptionsForProduct.GetProductForCustomer:
                    Console.Write("Enter id of product to search for\n");
                    int.TryParse(Console.ReadLine(), out id);
                    BO.ProductItem product2 = bl.Product.GetProductForCustomer(id, myCart);
                    Console.Write(product2.ToString());
                    break;
                case BO.OptionsForProduct.AddProduct:
                    int amount;
                    double price;
                    BO.BookGenre category;
                    string? name;
                    Console.Write("Enter details of new product: price, name, Category, amount\n");
                    double.TryParse(Console.ReadLine(), out price);
                    Enum.TryParse(Console.ReadLine(), out category);
                    int.TryParse(Console.ReadLine(), out amount);
                    name = Console.ReadLine();
                    product.Price = price;
                    product.Name = name;
                    product.Category = category;
                    product.AmountInStock = amount;
                    bl.Product.AddProduct(product);
                    break;
                case BO.OptionsForProduct.RemoveProduct:
                    Console.Write("Enter id of product to remove\n");
                    int.TryParse(Console.ReadLine(), out id);
                    bl.Product.RemoveProduct(id);
                    break;
                case BO.OptionsForProduct.UpdateProduct:
                    Console.Write("Enter details of new product: id, price, name, Category, amount\n");
                    int.TryParse(Console.ReadLine(), out id);
                    double.TryParse(Console.ReadLine(), out price);
                    name = Console.ReadLine();
                    BO.BookGenre.TryParse(Console.ReadLine(), out category);
                    int.TryParse(Console.ReadLine(), out amount);
                    product.ID = id;
                    product.Price = price;
                    product.Name = name;
                    product.Category = category;
                    product.AmountInStock = amount;
                    bl.Product.UpdateProduct(product);
                    break;
            }
            Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetProductList:1, GetProductForManager:2, GetProductForCustomer:3, AddProduct:4, RemoveProduct:5, UpdateProduct:6\n");
            BO.OptionsForProduct.TryParse(Console.ReadLine(), out option);
        }
        catch (BlApi.ExistsAlreadyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.BlEntityNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.ErrorDeleting ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NegativeIdException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NegativeAmountException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.NegativePriceException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (BlApi.EmptyStringException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch
        {
            Console.WriteLine("unexplained error occured\n");
        }
        Console.WriteLine("What Would you like to check?\nhere are the options to choose from:\nExit:0, GetProductList:1, GetProductForManager:2, GetProductForCustomer:3, AddProduct:4, RemoveProduct:5, UpdateProduct:6\n");
        BO.OptionsForProduct.TryParse(Console.ReadLine(), out option);
    }
}
int Main()
{
    BO.OptionsForMain num;
    Console.WriteLine("welcome to the store, start testing...\nEnter a number between 0-3 as follows: 0 to Exit, 1 to Cart, 2 to Order, 3 to Product\n");
    BO.OptionsForMain.TryParse(Console.ReadLine(), out num);

    while (num < BO.OptionsForMain.Exit || num > BO.OptionsForMain.Product)
    {
        Console.WriteLine("Enter a number between 0-3 as follows: 0 to Exit, 1 to Cart, 2 to Order, 3 to Product\n");
        BO.OptionsForMain.TryParse(Console.ReadLine(), out num);
    }
    switch (num)
    {
        case BO.OptionsForMain.Exit:
            Console.WriteLine("guess you're not intrested in testing now...\n");
            break;
        case BO.OptionsForMain.Cart:
            NavigateCart();
            break;
        case BO.OptionsForMain.Order:
            NavigateOrder();
            break;
        case BO.OptionsForMain.Product:
            NavigateProduct();
            break;
        default:
            Console.WriteLine("unexpected error occured\n");
            break;
    }
    return 0;
}

Main();