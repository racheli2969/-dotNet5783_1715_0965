using DO;
namespace Dal;

public static class DataSource
{
    /// <summary>
    /// configures all indexs needed for the data arrays
    /// </summary>
    public static class Config
    {
        public static int ItemId = 100000;
        public static int OrderId = 1;
        public static int OrderItemId = 1;
      /*  public static int IndexItem = 0;
        public static int IndexOrder = 0;
        public static int IndexOrderItem = 0;*/
        public static int LastOrderId { get { return OrderId++; } }
        public static int LastOrderItemId { get { return OrderItemId++; } }
      /*  public static int LastIndexItem { get { return IndexItem++; } }
        public static int LastIndexOrder { get { return IndexOrder++; } }//last Index available
        public static int LastIndexOrderItem { get { return IndexOrderItem++; } }*/
        public static int LastItemId { get { return ItemId++; } }
    }

    public static List<Item> Items = new List<Item>();
    public static List<OrderItem>OrderItems = new List<OrderItem>();
    public static List<Order>Orders = new List<Order>();
    const int items = 10;
    const int orders = 20;
    const int orderItems = 40;
    /// <summary>
    /// data to randomize to initalize the data arrays
    /// </summary>
    static (string, BookCategory)[] bookNames = { ("Danny", BookCategory.Children), ("Ivory", BookCategory.Teens), ("Danger", BookCategory.Adults), ("Diverse", BookCategory.Teens), ("Rainy days", BookCategory.Children), ("Rachel", BookCategory.Adults), ("The janitor's daughter", BookCategory.Adults), ("Esther", BookCategory.Children), ("Cell 35", BookCategory.Teens) };
    static string[] customerNames = { "Ruth", "Esther", "Abigayil", "Leah", "Rachel", "Shlomo", "Meir", "Aharon", "Eliyahu", "Yehuda", "Iska", "Shoshana", "Ayala", "Shimon", "Chaim", "Yael", "Eliezer", "Moshe", "Dan" };
    static string[] emails = { "1234r@gmail.com", "rghf@gmail.com", "rsdjk@gmail.com", "rcvbn23245@gmail.com", "789rrrrr@gmail.com", "ythkjfr@gmail.com", "aaaa45r@gmail.com", "rlhjgj@gmail.com", "fgddgr@gmail.com", "rapoiqq@gmail.com", "51234r@gmail.com", "rjkl222@gmail.com", "r2023@gmail.com", "rstuv@gmail.com", "wxyz@gmail.com", "abc123@gmail.com" };
    static string[] streets = { "Zait", "Tamar", "Hertzl", "Menachem Begin", "Hahagana", "Lehi", "Palmach", "Rimon", "Yachad shivtei israel", "Ezra", "Binyamin", "Yaakov" };
    static string[] cities = { "Yerushalaim", "Rehovot", "Beit shemesh", "Beitar", "Rishon Letzion", "NesZiona" };
    public static readonly Random Number = new Random();
    /// <summary>
    /// creates product data
    /// </summary>
    static void CreateProductData()
    {
        for (int i = 0; i < items; i++)
        {
            Item item = new Item();
            int counter = 0;
            item.Name = bookNames[Number.NextInt64(0, bookNames.Length)].Item1;
            item.Price = Number.NextInt64(35, 140);
            item.ID = Config.LastItemId;
            item.Category = Convert.ToInt16(bookNames[Number.NextInt64(1, bookNames.Length)].Item2);
            Items.Add(item);

            for (int j = 0; j < items; j++)
            {
                if (Items[i].InStock == false)
                    counter++;
            }
            if (items / counter > (0.05 * items))
                item.InStock = true;
            else
                item.InStock = false;
        }
    }
    /// <summary>
    /// creates order data
    /// </summary>
    static void CreateOrderData()
    {
        Order order = new Order();
        for (int i = 0; i < orders; i++)
        {
            order.OrderId = Config.LastOrderId;
            order.Address = streets[Number.NextInt64(0, streets.Length)] +", "+ Number.Next(0, 100) + ", " + cities[Number.Next(0, cities.Length)];
            order.CustomerName = customerNames[Number.NextInt64(0, customerNames.Length)];
            order.Email = emails[Number.NextInt64(0, emails.Length)];
            order.DateOrdered = DateTime.Now;
            TimeSpan ts = new TimeSpan(Number.Next(20, 500), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));//time span of between 2-12 days 
            order.DateOrdered=order.DateOrdered.Subtract(ts);
             ts = new TimeSpan(Number.Next(2, 10), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));
            if (i < 0.8 * orders)//80% of orders
            {
                order.DateDelivered = order.DateOrdered;
                order.DateDelivered= order.DateDelivered.Add(ts);
            }
            ts = new TimeSpan(Number.Next(2, 10), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));
            if (i < 0.6 * orders)//60% of orders
            {
                order.DateReceived = order.DateDelivered;
                order.DateReceived=order.DateReceived.Add(ts);
            }

            Orders.Add(order);
    
        }
    }

    /// <summary>
    /// creates order item data
    /// </summary>
    static void CreateOrderItemData()
    {
        for (int i = 0; i < orders; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderItemId = Config.LastOrderItemId;
            orderItem.OrderID = Number.Next(0, Orders.Count);
            orderItem.OrderItemId = Number.Next(0, OrderItems.Count);
            if (i >= 20)
            {
                orderItem.OrderID = Orders[i - 20].OrderId;
            }
            else
            {
                orderItem.OrderID = Orders[i].OrderId;
            }
            orderItem.ItemId = Items[Number.Next(0, Items.Count)].ID;
            for (int j = 0; j < Items.Count; j++)
            {
                if (Items[j].ID == orderItem.ItemId)
                    orderItem.Price = Items[j].Price;
            }
            orderItem.Amount = Number.Next(1, 3);
            OrderItems.Add(orderItem);
        }
    }
    /// <summary>
    /// initalizes all data
    /// </summary>
    public static void S_Initalize()
    {
        CreateProductData();
        CreateOrderData();
        CreateOrderItemData();
    }
}



