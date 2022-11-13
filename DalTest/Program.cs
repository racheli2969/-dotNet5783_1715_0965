using Dal;
using DO;

void PrintOptions(){
    Console.WriteLine("To Exit:0,\r\n  To  Add: a,\r\n  To View By Id: b,\r\n   To View All:c,\r\n  To Update: d,\r\n   To Delete: e");
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
    switch(characterType){
        case (int)OptionsForMain.Item:
            Item item = new Item();
            Console.WriteLine("enter details for new book:");
            DalItem.Add(item);
            break;
        case (int)OptionsForMain.Order:
            Order order = new Order();
            Console.WriteLine("enter details for new order:");
            DalOrder.Add(order);
            break;
        case (int)OptionsForMain.OrderItem:
            Console.WriteLine("enter details for new orderItem:");
            break;
    }
}
 void ViewById(int characterType)
{
    switch (characterType)
    {
        case (int)OptionsForMain.Item:
            break;
        case (int)OptionsForMain.Order:
            break;
        case (int)OptionsForMain.OrderItem:
            break;
    }
}            
 void ViewAll(int characterType)
{
    switch (characterType)
    {
        case (int)OptionsForMain.Item:
            break;
        case (int)OptionsForMain.Order:
            break;
        case (int)OptionsForMain.OrderItem:
            break;
    }
}             
 void Update(int characterType)
{
    switch (characterType)
    {
        case (int)OptionsForMain.Item:
            break;
        case (int)OptionsForMain.Order:
            break;
        case (int)OptionsForMain.OrderItem:
            break;
    }
}
 void Delete(int characterType)
{
    switch (characterType)
    {
        case (int)OptionsForMain.Item:
            break;
        case (int)OptionsForMain.Order:
            break;
        case (int)OptionsForMain.OrderItem:
            break;
    }
}               
 void ControlOptions(int characterType){
    PrintOptions();
    int x = Convert.ToInt32(Console.Read());
    while (x!=0)
    {
        PrintOptions();
        x = Convert.ToInt32(Console.Read());
        switch (x)
        {
            case (int)OptionsOfActions.Exit:
                break;
            case (int)OptionsOfActions.AddItem:
                Add(characterType);
                break;
            case (int)OptionsOfActions.ViewById:
                ViewById(characterType);
                break;
            case (int)OptionsOfActions.ViewAll:
                ViewAll(characterType);
                break;
            case (int)OptionsOfActions.UpdateItem:
                Update(characterType);
                break;
            case (int)OptionsOfActions.DeleteItem:
                Delete(characterType);
                break;
        }
    }
}
 void Main(string[] args){
    int number;
    string input;
    Console.WriteLine("For Item Enter 1\nFor Order Enter 2\nFor Order Item Enter 3\nTo Exit Enter 0\n");
    input = Console.ReadLine();
    int.TryParse(input, out number);
    while (number < 0 || number > 3)
    {
        Console.WriteLine("Input needs to be between 0 and 3\nPlease try again");
        input = Console.ReadLine();
        int.TryParse(input, out number);
    }
    if (number != 0)
        ControlOptions(number);
}
