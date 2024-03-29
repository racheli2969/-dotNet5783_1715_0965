﻿namespace DO;
/// <summary>
/// defines the item details
/// </summary>
public struct Item
{
    public string? Name { get; set; }
    public BookGenre Category { get; set; }
    public double Price { get; set; }
    public int AmountInStock { get; set; }
    public int ID { get; set; }

    public override string ToString() => $@"
ID: {ID} 
Name: {Name}
Category: {Category}
Price: {Price}
AmountInStock: {AmountInStock}
";

    public static explicit operator Item(double v)
    {
        throw new NotImplementedException();
    }
}
