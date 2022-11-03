
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
        private static int ItemId = 0;
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
    static (string, BookCategory)[] bookNames = { ("Danny", BookCategory.Children),("Ivory",) };
  // static string[] bookNames = { "Danny", "Ivory", "Danger", "Diverse", "Rainy days", "Rachel", "The janitor's daughter", "Esther", "Cell 35" };
    static string[] customerNames = { "Ruth", "Esther", "Abigayil", "Leah", "Rachel", "Shlomo", "Meir", "Aharon", "Eliyahu", "Yehuda", "Iska", "Shoshana", "Ayala", "Shimon", "Chaim", "Yael", "Eliezer", "Moshe", "Dan" };
    static string[] emails = { "1234r@gmail.com", "rghf@gmail.com", "rsdjk@gmail.com", "rcvbn23245@gmail.com", "789rrrrr@gmail.com", "ythkjfr@gmail.com", "aaaa45r@gmail.com", "rlhjgj@gmail.com", "fgddgr@gmail.com", "rapoiqq@gmail.com", "51234r@gmail.com", "rjkl222@gmail.com", "r2023@gmail.com", "rstuv@gmail.com", "wxyz@gmail.com", "abc123@gmail.com" };
    static string[] streets = { "Zait", "Tamar", "Hertzl", "Menachem Begin", "Hagana", "Lehi", "Palmach", "Rimon", "Yachad shivtei israel", "Ezra", "Binyamin", "Yaakov" };
    static string[] cities = { "Yerushalaim", "Rehovot", "Beit shemesh", "Beitar", "Rishon Letzion", "NesZiona" };
    public static readonly Random Number = new Random();

    private static Item CreateProductData()
    {
        Item item= new Item();
        item.Name = 
        item.InStock = Convert.ToBoolean(Number.Next(0, 1));
        item.Price = Number.Next(35,140);
        item.ID = Number.Next(0, Config.LastItemId); 
        item.Price = Number.Next(0, 3);
        // item.Category = eBookCategory.GetName(typeof(MicrosoftOffice)Number.Next(0, 3))
        return item;
    }
    private static Order CreateOrderData()
    {
        Order order=new Order(); 
        order.OrderId=Number.Next(0, Config.LastIndexOrder);
        order.Address = streets[Number.Next(0, streets.Length)] + cities[Number.Next(0,cities.Length)]+Number.Next(0,cities.Length);
        order.CustomerName = customerNames[Number.Next(0,customerNames.Length)];
        order.Email = emails[Number.Next(0, emails.Length)];
        order.DateDelivered=
        order.DateOrdered=
        order.DateReceived= DateTime.Now;
        return order;
    }
    private static OrderItem CreateOrderItemData()
    {
        OrderItem orderItem =new OrderItem(); 
        orderItem.OrderItemId=Number.Next(0, Config.LastIndexOrderItem); 
        orderItem.OrderID=Number.Next(0, Config.LastIndexOrder);
        orderItem.ItemId = Number.Next(0, Config.IndexItem);
        orderItem.Price = Number.Next(35, 140);
        orderItem.Amount = Number.Next(1, 3);
        return orderItem;
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
        OrderItem orderItem;
        Order order;
        for (int i = 0; i < 10; i++)
        {
            item = CreateProductData();
            Add_Item(item);
        }
        for (int i = 0; i < 10; i++)
        {
            order = CreateOrderData();
            Add_Order(order);
        }
        for (int i = 0; i < 10; i++)
        {
            orderItem = CreateOrderItemData();
            Add_OrderItem(orderItem);
        }

    }
}



