using DO;

namespace Dal;

internal static class DataSource
{

    static DataSource()
    {
        S_Initalize();
    }
    string [] bookNames = {"Danny","Ivory","Danger","Diverse","Rainy days","Rachel","The janitor's daughter","Esther","Cell 35"}
    //price and in stock randomly
    string [] customerNames = {"Ruth","Esther","Abigayil","Leah","Rachel","Shlomo","Meir","Aharon","Eliyahu","Yehuda","Iska","Shoshana","Ayala","Shimon","Chaim","Yael","Eliezer","Moshe","Dan"}
    string[] emails = { "1234r@gmail.com", "rghf@gmail.com", "rsdjk@gmail.com", "rcvbn23245@gmail.com", "789rrrrr@gmail.com", "ythkjfr@gmail.com", "aaaa45r@gmail.com", "rlhjgj@gmail.com", "fgddgr@gmail.com", "rapoiqq@gmail.com", "51234r@gmail.com", "rjkl222@gmail.com", "r2023@gmail.com", "rstuv@gmail.com", "wxyz@gmail.com", "abc123@gmail.com" };
    string[] streets = { "Zait", "Tamar", "Hertzl", "Menachem Begin", "Hagana", "Lehi", "Palmach", "Rimon", "Yachad shivtei israel", "Ezra", "Binyamin", "Yaakov" };
    string[] cities = { "Yerushalaim", "Rehovot", "Beit shemesh", "Beitar", "Rishon Letzion", "NesZiona" };

    private static Item CreateProductData()
    {
        Item
        return Item;
    }
    private static Order CreateOrderData()
    {
        return Order;
    }
    private static OrderItem CreateProductData()
    {
        return OrderItem;
    }
    private static void Add_Item(Item item)
    {

        Items[Config.LastIndexItem] = item;

    }

    private static void Add_Order(Order order)
    {
        Orders[Config.LastIndexOrder] = order;
    }
    private static void Add_OrderItem(OrderItem orderItem)
    {
        OrderItems[Config.LastIndexOrderItem] = orderItem;
    }
    private static void S_Initalize()
    {
        Item item;
        for (int i = 0; i < 10; i++)
        {
            item = createProductdata();
            Add_Item(item);
        }

        Add_Order();
        Add_OrderItem();

    }
}

/// <summary>
/// adds items to the item array
/// </summary>

public static readonly Random Number = new Random();
internal static Item[] Items = new Item[50];
internal static OrderItem[] OrderItems = new OrderItem[200];
internal static Order[] Orders = new Order[100];

internal static class Config
{
    public static int IndexItem = 0;
    public static int IndexOrder = 0;
    public static int IndexOrderItem = 0;
    private static int ItemId = 0;
    private static int OrderId = 0;
    private static int OrderItemId = 0;
    public static int LastIndexItem { get { return IndexItem++; } };
    public static int LastIndexOrder { get { return IndexOrder++; } };
    public static int LastIndexOrderItem { get { return IndexOrderItem++; } };
    public static int LastItemId { get { return ItemId++; } };
    public static int LastItemOrderId { get { return OrderId++; } };
    public static int LastOrderItemId { get { return OrderItemId++; } };
        /// <summary>
        /// indexs for the first available place in the data arrays
        /// </summary>
       `
    }
}

