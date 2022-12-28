﻿namespace Dal;
using DalApi;
sealed internal class DalXml : IDal
{
    public IItem Item { get; } = new Dal.Item();

    public IOrder Order { get; } = new Dal.Order();

    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}