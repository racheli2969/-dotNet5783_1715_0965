using DO;
using DalApi;
using Dal;
IDal dalxml = new DalXml();
void main()
{
    double doubleTemp;
    int intTemp;
    Item item = new Item();
    Console.WriteLine("enter details for new book: Name, Category, Price, Is In Stock");
    item.Name = Console.ReadLine();
    BookGenre bookg;// = new BookCategory();
    BookGenre.TryParse(Console.ReadLine(), out bookg);
    item.Category = bookg;
    double.TryParse(Console.ReadLine(), out doubleTemp);
    item.Price = doubleTemp;
    int.TryParse(Console.ReadLine(), out intTemp);
    item.AmountInStock = intTemp;
    dalxml.Item.Add(item);
}

main();