﻿using DO;
using System;
using System.Threading;

namespace Dal;

public static class DataSource
{
    /// <summary>
    /// configures all indexs needed for ids
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// id of product length of 6 digits
        /// </summary>
        public static int ItemId = 100000;
        /// <summary>
        /// id of order
        /// </summary>
        public static int OrderId = 1;
        /// <summary>
        /// id of order item
        /// </summary>
        public static int OrderItemId = 1;
        /// <summary>
        /// forwards the order id in 1
        /// </summary>
        public static int LastOrderId { get { return OrderId++; } }
        /// <summary>
        /// forwards the order item id by 1
        /// </summary>
        public static int LastOrderItemId { get { return OrderItemId++; } }
        /// <summary>
        /// forwards the product id by 1
        /// </summary>
        public static int LastItemId { get { return ItemId++; } }
    }
    /// <summary>
    /// product list
    /// </summary>
    public static List<Item?> Items = new List<Item?>();
    /// <summary>
    /// order items list
    /// </summary>
    public static List<OrderItem?> OrderItems = new List<OrderItem?>();
    /// <summary>
    /// orders list
    /// </summary>
    public static List<Order?> Orders = new List<Order?>();
    const int items = 10;
    const int orders = 20;
    const int orderItems = 40;
    public static bool wasInitalized = false;
    /// <summary>
    /// data to randomize to initalize the data lists
    /// </summary>
    static (string, BookGenre)[] bookNames = { ("Danny", BookGenre.Fiction), ("Ivory", BookGenre.Thriller), ("Danger", BookGenre.Mystery), ("Diverse", BookGenre.History), ("Rainy days", BookGenre.Fantasy), ("Rachel", BookGenre.Biography), ("The janitor's daughter", BookGenre.Fantasy), ("Esther", BookGenre.Mystery), ("Cell 35", BookGenre.SciFi) };
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
            item.Name = bookNames[Number.Next(0, bookNames.Length)].Item1;
            item.Price = Number.Next(35, 140);
            item.ID = Config.LastItemId;
            item.Category = bookNames[Number.Next(1, bookNames.Length)].Item2;

            for (int j = 0; j < items; j++)
            {
                if (item.AmountInStock == 0)
                    counter++;
            }
            if (items / counter > (0.05 * items))
                item.AmountInStock = Number.Next(1, 140);
            else
                item.AmountInStock = 0;
            Items?.Add(item);
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
            order.Address = streets[Number.NextInt64(0, streets.Length)] + ", " + Number.Next(0, 100) + ", " + cities[Number.Next(0, cities.Length)];
            order.CustomerName = customerNames[Number.NextInt64(0, customerNames.Length)];
            order.Email = emails[Number.NextInt64(0, emails.Length)];
            order.DateOrdered = DateTime.Now;
            //all dates who are not started contain DateTime.MinValue
            order.DateShipped = DateTime.MinValue;
            order.DateDelivered = DateTime.MinValue;
            TimeSpan ts = new TimeSpan(Number.Next(20, 500), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));//time span of between 2-12 days 
            order.DateOrdered = ((DateTime)(order).DateOrdered).Subtract(ts);
            ts = new TimeSpan(Number.Next(2, 10), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));
            if (i < 0.8 * orders)//80% of orders date of delivery started
            {
                order.DateShipped = order.DateOrdered;
                order.DateShipped = ((DateTime)order.DateShipped).Add(ts);
            }
            ts = new TimeSpan(Number.Next(2, 10), Number.Next(0, 30), Number.Next(0, 24), Number.Next(0, 60), Number.Next(0, 60));
            if (i < 0.6 * orders)//60% of orders
            {
                order.DateDelivered = order.DateShipped;
                order.DateDelivered = ((DateTime)(order).DateDelivered).Add(ts);
            }

            Orders.Add(order);

        }
    }

    /// <summary>
    /// creates order item data
    /// </summary>
    static void CreateOrderItemData()
    {
        for (int i = 0; i < 2 * orders; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderItemId = Config.LastOrderItemId;
            if (i < 20)
            {
                if (Orders[i].HasValue)
                    orderItem.OrderID = Orders[i].Value.OrderId;

            }
            else
            {
                if (Orders[i - 20].HasValue)
                    orderItem.OrderID = Orders[i - 20].Value.OrderId;
            }
            if (Items[Number.Next(0, Items.Count)].HasValue)
                orderItem.ItemId = Items[Number.Next(0, Items.Count)].Value.ID;
            for (int j = 0; j < Items.Count; j++)
            {
                if (Items[j].Value.ID == orderItem.ItemId)
                    orderItem.Price = Items[j].Value.Price;
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
        wasInitalized = true;
    }

}



