﻿using Dal;
using DO;

void PrintOptions(){
    Console.WriteLine("To Exit:0,\r\nTo  Add: a,\r\nTo View By Id: b,\r\nTo View All:c,\r\nTo Update: d,\r\nTo Delete: e");
}

  DateTime DateInputControl(string? s)
{
    DateTime result=DateTime.MinValue;
    bool a = false;
    while (a == false)
    {
        a = DateTime.TryParse(s, out result);
        if (a == false)
        {
            Console.WriteLine("not legal input, try again\n");
            s = Console.ReadLine();
        }  
    }
    return result;
}
void Add(int characterType){
    int intTemp;
   double doubleTemp;
        switch (characterType)
        {
            case (int)OptionsForMain.Item:
                Item item = new Item();
                Console.WriteLine("enter details for new book: Name, Category, Price, Is In Stock");
                item.Name = Console.ReadLine();
                int.TryParse(Console.ReadLine(), out intTemp);
                item.Category = intTemp;
                double.TryParse(Console.ReadLine(), out doubleTemp);
                item.Price = doubleTemp;
                item.InStock = Convert.ToBoolean(Console.Read());
                DalItem.Add(item);
                break;
            case (int)OptionsForMain.Order:
                Order order = new Order();
                Console.WriteLine("enter details for new order: CustomerName, Email, Address, DateOrdered, DateDelivered, DateReceived  ");
                order.CustomerName = Console.ReadLine();
                order.Email = Console.ReadLine();
                order.Address = Console.ReadLine();
                order.DateOrdered = DateInputControl(Console.ReadLine());
                order.DateDelivered = DateInputControl(Console.ReadLine());
                order.DateReceived = DateInputControl(Console.ReadLine());
                DalOrder.Add(order);
                break;
        case (int)OptionsForMain.OrderItem:
                OrderItem orderItem = new OrderItem();
                Console.WriteLine("enter details for new orderItem:");
                Console.WriteLine("enter details for new orderItem:: Amount,  OrderItemId, Price");
                int.TryParse(Console.ReadLine(), out intTemp);
                orderItem.Amount = intTemp;
                int.TryParse(Console.ReadLine(), out intTemp);
                orderItem.OrderItemId = intTemp;
                double.TryParse(Console.ReadLine(), out doubleTemp);
                orderItem.Price = doubleTemp;
                DalOrderItem.Add(orderItem);
                break;
        }
}
 void ViewById(int characterType)
{
    int a;
    try
    {
        switch (characterType)
        {
            case (int)OptionsForMain.Item:
                Console.WriteLine("enter id");
                int.TryParse(Console.ReadLine(), out a);
                Item item = DalItem.ViewById(a);
                Console.WriteLine(item.ToString());
                break;
            case (int)OptionsForMain.Order:
                Console.WriteLine("enter id");
                int.TryParse(Console.ReadLine(), out a);
                Order order = DalOrder.ViewById(a);
                Console.WriteLine(order.ToString());
                break;
            case (int)OptionsForMain.OrderItem:
                Console.WriteLine("enter id");
                int.TryParse(Console.ReadLine(), out a);
                OrderItem orderItem = DalOrderItem.ViewById(a);
                Console.WriteLine(orderItem.ToString());
                break;
        }
    }
    catch (Exception msg)
    {
        Console.WriteLine(msg);
    }
}            
 void ViewAll(int characterType)
{
    try
    {
        switch (characterType)
        {
            case (int)OptionsForMain.Item:
                Item[] items = DalItem.ViewAll();
                foreach (Item item in items)
                    Console.WriteLine(item.ToString());
                break;
            case (int)OptionsForMain.Order:
                Order[] orders = DalOrder.ViewAll();
                foreach (Order order in orders)
                    Console.WriteLine(order.ToString());
                break;
            case (int)OptionsForMain.OrderItem:
                OrderItem[] orderItems = DalOrderItem.ViewAll();
                foreach (OrderItem orderItem in orderItems)
                    Console.WriteLine(orderItem.ToString());
                break;
        }
    }
    catch (Exception msg)
    {
        Console.WriteLine(msg);
    }
} 

 void Update(int characterType)
{
    int intTemp;
    double doubleTemp;
    try
    {
        switch (characterType)
        {
            case (int)OptionsForMain.Item:
                Item item = new Item();
                Console.WriteLine("enter datails of book to update:Name, Category, Price, Is In Stock, ID");
                item.Name = Console.ReadLine();
                int.TryParse(Console.ReadLine(), out intTemp);
                item.Category = intTemp;
                double.TryParse(Console.ReadLine(), out doubleTemp);
                item.Price = doubleTemp;
                item.InStock = Convert.ToBoolean(Console.Read());
                int.TryParse(Console.ReadLine(), out intTemp);
                item.ID = intTemp;
                DalItem.Update(item);
                break;
            case (int)OptionsForMain.Order:
                Order order = new Order();
                Console.WriteLine("enter details of order to update: OrderId, CustomerName, Email, Address, DateOrdered, DateDelivered, DateReceived  ");
                int.TryParse(Console.ReadLine(), out intTemp);
                order.OrderId = intTemp;
                order.CustomerName = Console.ReadLine();
                order.Email = Console.ReadLine();
                order.Address = Console.ReadLine();
                order.DateOrdered = DateInputControl(Console.ReadLine());
                order.DateDelivered = DateInputControl(Console.ReadLine());
                order.DateReceived = DateInputControl(Console.ReadLine());
                DalOrder.Update(order);
                break;
            case (int)OptionsForMain.OrderItem:
                OrderItem orderItem = new OrderItem();
                Console.WriteLine("enter details of order to update: Amount, OrderID, ItemId, OrderItemId, Price");
                int.TryParse(Console.ReadLine(), out intTemp);
                orderItem.Amount = intTemp;
                int.TryParse(Console.ReadLine(), out intTemp);
                orderItem.OrderID = intTemp;
                int.TryParse(Console.ReadLine(), out intTemp);
                orderItem.ItemId = intTemp;
                int.TryParse(Console.ReadLine(), out intTemp);
                orderItem.OrderItemId = intTemp;
                double.TryParse(Console.ReadLine(), out doubleTemp);
                orderItem.Price = doubleTemp;
                DalOrderItem.Update(orderItem);
                break;
        }
    }
    catch (Exception msg)
    {
        Console.WriteLine(msg);
    }
}
 void Delete(int characterType)
{
    int id;
    try
    {
        switch (characterType)
        {
            case (int)OptionsForMain.Item:
                Console.WriteLine("enter id of book to be deleted");
                int.TryParse(Console.ReadLine(), out id);
                DalItem.Delete(id);
                break;
            case (int)OptionsForMain.Order:
                Console.WriteLine("enter id of order to be deleted");
                int.TryParse(Console.ReadLine(), out id);
                DalOrder.Delete(id);
                break;
            case (int)OptionsForMain.OrderItem:
                Console.WriteLine("enter id of order item to be deleted");
                int.TryParse(Console.ReadLine(), out id);
                DalOrderItem.Delete(id);
                break;
        }
    }
    catch (Exception msg)
    {
        Console.WriteLine(msg);
    }
}               
/// <summary>
/// navigates between the options of deleting etc...
/// </summary>
 void ControlOptions(int characterType){
    PrintOptions();
    char x=char.Parse(Console.ReadLine());
    while (x!='0')
    {
        switch (x)
        {
            case (char)OptionsOfActions.Exit:
                break;
            case (char)OptionsOfActions.AddItem:
                Add(characterType);
                break;
            case (char)OptionsOfActions.ViewById:
                ViewById(characterType);
                break;
            case (char)OptionsOfActions.ViewAll:
                ViewAll(characterType);
                break;
            case (char)OptionsOfActions.UpdateItem:
                Update(characterType);
                break;
            case (char)OptionsOfActions.DeleteItem:
                Delete(characterType);
                break;
        }
        PrintOptions();
        x = char.Parse(Console.ReadLine());
    }
}

 void Main()
{
    int number;
    string input;
    DataSource.S_Initalize();
    Console.WriteLine("For Item Enter 1\nFor Order Enter 2\nFor Order Item Enter 3\nTo Exit Enter 0\n");
    input = Console.ReadLine();
    int.TryParse(input, out number);
    while (number < 0 || number > 3)
    {
        Console.WriteLine("Input needs to be between 0 and 3\nPlease try again");//three options:order, item and order item
        input = Console.ReadLine();
        int.TryParse(input, out number);
    }
    if (number != 0)
        ControlOptions(number);
}
Main();