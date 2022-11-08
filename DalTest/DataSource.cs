
using DO;
namespace Dal;

public static class DataSource
{
    public static class Config
    {
        public static int IndexItem = 0;
        public static int IndexOrder = 0;
        public static int IndexOrderItem = 0;
        private static int ItemId = 100000;
        public static int LastIndexItem { get { return IndexItem++; } }
        public static int LastIndexOrder { get { return IndexOrder++; } }//last Index available
        public static int LastIndexOrderItem { get { return IndexOrderItem++; } }
        public static int LastItemId { get { return ItemId++; } }
    }

    public static Item[] Items = new Item[50];
    public static OrderItem[] OrderItems = new OrderItem[200];
    public static Order[] Orders = new Order[100];
    const int items = 10;
    const int orders = 20;
    const int orderItems = 40;
    static (string, BookCategory)[] bookNames = { ("Danny", BookCategory.Children),("Ivory",BookCategory.Teens),("Danger",BookCategory.Adults),("Diverse",BookCategory.Teens),("Rainy days",BookCategory.Children),("Rachel",BookCategory.Adults),("The janitor's daughter",BookCategory.Adults),("Esther",BookCategory.Children),("Cell 35",BookCategory.Teens)};
    static string[] customerNames = { "Ruth", "Esther", "Abigayil", "Leah", "Rachel", "Shlomo", "Meir", "Aharon", "Eliyahu", "Yehuda", "Iska", "Shoshana", "Ayala", "Shimon", "Chaim", "Yael", "Eliezer", "Moshe", "Dan" };
    static string[] emails = { "1234r@gmail.com", "rghf@gmail.com", "rsdjk@gmail.com", "rcvbn23245@gmail.com", "789rrrrr@gmail.com", "ythkjfr@gmail.com", "aaaa45r@gmail.com", "rlhjgj@gmail.com", "fgddgr@gmail.com", "rapoiqq@gmail.com", "51234r@gmail.com", "rjkl222@gmail.com", "r2023@gmail.com", "rstuv@gmail.com", "wxyz@gmail.com", "abc123@gmail.com" };
    static string[] streets = { "Zait", "Tamar", "Hertzl", "Menachem Begin", "Hagana", "Lehi", "Palmach", "Rimon", "Yachad shivtei israel", "Ezra", "Binyamin", "Yaakov" };
    static string[] cities = { "Yerushalaim", "Rehovot", "Beit shemesh", "Beitar", "Rishon Letzion", "NesZiona" };
    public static readonly Random Number = new Random();

    private static void CreateProductData()
    {
        for (int i = 0; i < items; i++)
        {
            Item item = new Item();
            int counter = 0;
            item.Name = bookNames[Number.NextInt64(0, bookNames.Length)].Item1;
            for (int j = 0; j < items; j++)
            {
                if (Items[i].InStock == false)
                    counter++;
            }
            if (items / counter > (0.05 * items))
                item.InStock = true;
            else
                item.InStock = false;
            item.Price = Number.NextInt64(35, 140);
            item.ID = Config.LastItemId;
            item.Category = Convert.ToInt16(bookNames[Number.NextInt64(0, bookNames.Length)].Item2);
            Items[Config.LastIndexItem] = item;
        }
    }

    private static void CreateOrderData()
    {
        
        for (int i = 0; i < orders; i++)
        {
            Order order = new Order();
            order.OrderId = Config.LastIndexOrder;
            order.Address = streets[Number.NextInt64(0, streets.Length)] + cities[Number.Next(0, cities.Length)] + Number.Next(0, cities.Length);
            order.CustomerName = customerNames[Number.NextInt64(0, customerNames.Length)];
            order.Email = emails[Number.NextInt64(0, emails.Length)];
            int v = Number.Next(0, 5);
            TimeSpan ts = new TimeSpan(Number.Next(2, 12), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));//time span of between 2-12 days 
            DateTime randomDate=new DateTime(Number.Next(0, 5), Number.Next(0, 12), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));
           order.DateOrdered.Subtract(randomDate);
            for (int j = 0; j <= 0.8 * 20; j++)
            {
                order.DateDelivered.Add(ts);
                ts = new TimeSpan(Number.Next(0, 12), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60)) ;
            }
            for (int j = 0; j <= 0.6 * 20; j++)
            {
                order.DateReceived.Add(ts);
                ts = new TimeSpan(Number.Next(0, 12), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60)); ;
            }
            Orders[Config.LastIndexOrder] = order;
        }
    }

    private static void CreateOrderItemData()
    {
        for (int i = 0; i < orderItems; i++)
        {
            OrderItem orderItem = new OrderItem();
            int count = 0;
            orderItem.OrderItemId = Config.LastIndexOrderItem;
            orderItem.OrderID = Number.Next(0, Orders.Length);
            orderItem.OrderItemId = Number.Next(0, OrderItems.Length);
            if (i >=20)
            {
                orderItem.OrderID = Orders[i-20].OrderId;
            }
            else
            {
                orderItem.OrderID = Orders[i].OrderId;
            }
            orderItem.ItemId = Items[Number.NextInt64(0, Items.Length)].ID;
            for (int j = 0; j < Items.Length; j++)
            {
                if (Items[i].ID == orderItem.ItemId)
                    orderItem.Price = Items[i].Price;
            }
            orderItem.Amount = Number.Next(1, 3);
            OrderItems[Config.LastIndexOrderItem] = orderItem;
        }

    }

    private static void S_Initalize()
    {
        CreateProductData();
        CreateOrderData();
        CreateOrderItemData();
    }
    static DataSource()
    {
        S_Initalize();
    }

}



