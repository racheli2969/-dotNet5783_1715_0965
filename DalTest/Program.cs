using Dal;

 void PrintOptions(){
    Console.WriteLine("To Exit:0,\r\n  To  Add: a,\r\n  To View By Id: b,\r\n   To View All:c,\r\n  To Update: d,\r\n   To Delete: e");
}
 
 void Add(int characterType){
    switch(characterType){
        case OptionsForMain.Item:
            break;
            case OptionsForMain.Order:
            break;
            case OptionsForMain.OrderItem:
            break;
    }
}
 void ViewById(int characterType)
{
switch(characterType){
        case OptionsForMain.Item:
            break;
            case OptionsForMain.Order:
            break;
            case OptionsForMain.OrderItem:
            break;
}
  }            
 void ViewAll(int characterType)
{
switch(characterType){
        case OptionsForMain.Item:
            break;
            case OptionsForMain.Order:
            break;
            case OptionsForMain.OrderItem:
            break;
}
  }             
 void Update(int characterType)
{
switch(characterType){
        case OptionsForMain.Item:
            break;
            case OptionsForMain.Order:
            break;
            case OptionsForMain.OrderItem:
            break;
}
}
 void Delete(int characterType)
{
switch(characterType){
        case OptionsForMain.Item:
            break;
            case OptionsForMain.Order:
            break;
            case OptionsForMain.OrderItem:
            break;
}
}               

 void ControlOptions(int characterType){
    PrintOptions();
    int x = int.Parse(Console.ReadLine());
    while (x!=0)
    {
        PrintOptions();
        switch (x)
        {
            case OptionsOfActions.Exit:
                break;
            case OptionsOfActions.AddItem:
                Add(characterType);
                break;
            case OptionsOfActions.ViewById:
                ViewById(characterType);
                break;
            case OptionsOfActions.ViewAll:
                ViewAll(characterType);
                break;
            case OptionsOfActions.UpdateItem:
                Update(characterType);
                break;
            case OptionsOfActions.DeleteItem:
                Delete(characterType);
                break;

        }
    }

}


static void Main(){
    Console.WriteLine("For Item Enter 1\nFor Order Enter 2\nFor Order Item Enter 3\nTo Exit Enter 0\n");
    int x = int.Parse(Console.ReadLine());
    while (x < 0 || x > 3)
    {
        Console.WriteLine("Input needs to be between 0 and 3\nPlease try again");
        x = int.Parse(Console.ReadLine());
    }
if(x!=0)
        ControlOptions(x);
}
