
using DO;
using System.Diagnostics.Metrics;

namespace Dal;
internal static class DataSource
{

    static DataSource()
    {
        S_Initalize();
    }
    internal static Item[] Items = new Item[50];
    internal static OrderItem[] OrderItems = new OrderItem[200];
    internal static Order[] Orders = new Order[100];

    internal static class Config
    {
        public static int IndexItem = 0;
        public static int IndexOrder = 0;
        public static int IndexOrderItem = 0;
        private static int ItemId = 100000;
        private static int OrderId = 0;
        private static int OrderItemId = 0;
        public static int LastIndexItem { get { return IndexItem++; } }
        public static int LastIndexOrder { get { return IndexOrder++; } }
        public static int LastIndexOrderItem { get { return IndexOrderItem++; } }
        public static int LastItemId { get { return ItemId++; } }
        public static int LastItemOrderId { get { return OrderId++; } }
        public static int LastOrderItemId { get { return OrderItemId++; } }
        /// <summary>
        /// indexs for the first available place in the data arrays
        /// </summary>

    }
    static (string, BookCategory)[] bookNames = { ("Danny", BookCategory.Children),("Ivory",BookCategory.Teens),("Danger",BookCategory.Adults),("Diverse",BookCategory.Teens),("Rainy days",BookCategory.Children),("Rachel",BookCategory.Adults),("The janitor's daughter",BookCategory.Adults),("Esther",BookCategory.Children),("Cell 35",BookCategory.Teens)};
    static string[] customerNames = { "Ruth", "Esther", "Abigayil", "Leah", "Rachel", "Shlomo", "Meir", "Aharon", "Eliyahu", "Yehuda", "Iska", "Shoshana", "Ayala", "Shimon", "Chaim", "Yael", "Eliezer", "Moshe", "Dan" };
    static string[] emails = { "1234r@gmail.com", "rghf@gmail.com", "rsdjk@gmail.com", "rcvbn23245@gmail.com", "789rrrrr@gmail.com", "ythkjfr@gmail.com", "aaaa45r@gmail.com", "rlhjgj@gmail.com", "fgddgr@gmail.com", "rapoiqq@gmail.com", "51234r@gmail.com", "rjkl222@gmail.com", "r2023@gmail.com", "rstuv@gmail.com", "wxyz@gmail.com", "abc123@gmail.com" };
    static string[] streets = { "Zait", "Tamar", "Hertzl", "Menachem Begin", "Hagana", "Lehi", "Palmach", "Rimon", "Yachad shivtei israel", "Ezra", "Binyamin", "Yaakov" };
    static string[] cities = { "Yerushalaim", "Rehovot", "Beit shemesh", "Beitar", "Rishon Letzion", "NesZiona" };
    public static readonly Random Number = new Random();

    private static Item CreateProductData()
    {
        for(int i = 0; i < 10; i++)
        { 
        int counter = 0;
        Item item= new Item();
        item.Name = bookNames[Number.NextInt64(0, bookNames.Length)].Item1;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].InStock == false)
              counter++;
        }
        if (Items.Length / counter > (0.05 * Items.Length))
            item.InStock = true;
        else
            item.InStock = false;
        item.Price = Number.NextInt64(35,140);
        item.ID = Config.LastItemId;
        item.Category = Convert.ToInt16(bookNames[Number.NextInt64(0, bookNames.Length)].Item2);
        Items[LastIndexItem]=item;
        }
    }
    private static Order CreateOrderData()
    {
        for(int i=0; i<20; i++)
        { 
        Order order=new Order(); 
        order.OrderId=Number.Next(0, Orders.Length);
        order.Address = streets[Number.NextInt64(0, streets.Length)] + cities[Number.Next(0,cities.Length)]+Number.Next(0,cities.Length);
        order.CustomerName = customerNames[Number.NextInt64(0,customerNames.Length)];
        order.Email = emails[Number.NextInt64(0, emails.Length)];
        order.DateDelivered=
        order.DateOrdered=
        order.DateReceived=
        Orders[LastIndexOrder]=order;
        }
    }
    private static OrderItem CreateOrderItemData()
    {
        for(int i = 0; i < 40; i++)
        { 
        OrderItem orderItem =new OrderItem(); 
        orderItem.OrderItemId=Number.Next(0, OrderItems.Length);
        orderItem.OrderID = Number.Next(0, Orders.Length);
        orderItem.ItemId = Items[Number.NextInt64(0, Items.Length)].ID;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].ID == orderItem.ItemId)
                orderItem.Price = Items[i].Price;
        }
        orderItem.Amount = Number.Next(1, 3);
        OrderItems[LastIndexOrderItem]=orderItem;
    }
    private static void Add_Item()
    {
      CreateProductData();
    }
    private static void Add_Order()
    {
        CreateOrderData();
    }
    private static void Add_OrderItem()
    {
        CreateOrderItemData();
    }
    private static void S_Initalize()
    {
        Add_Item();
        Add_Order();
        Add_OrderItem();
    }
}



