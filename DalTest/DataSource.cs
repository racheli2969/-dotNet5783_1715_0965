using DO;

namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        S_Initalize(0);
    }
    private static void S_Initalize(int type)
    {
        switch (type)
        {
            case 0: Add_Items(); break;
            default: Add_Items(); break;
        }
    }

    /// <summary>
    /// adds items to the item array
    /// </summary>
    private static void Add_Items()
    {
      
    }
    private static void Add_Order()
    {

    }
    private static void Add_OrderItem()
    {

    }
    public static readonly Random Number =
       new Random();
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
        public static int LastItemId {get { return ItemId++; } };
        public static int LastItemOrderId { get { return OrderId++; } };
        public static int LastOrderItemId { get { return OrderItemId++; } };
        /// <summary>
        /// indexs for the first available place in the data arrays
        /// </summary>

    }
    
}

