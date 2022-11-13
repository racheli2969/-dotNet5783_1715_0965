﻿
namespace DO;
/// <summary>
/// defines the item details
/// </summary>
public struct Item
{
    public string Name { get; set; }
    public int Category { get; set; }
    public double Price { get; set; }
    public bool InStock { get; set; }
    public int ID { get; set; }

    public override string ToString() => $@"
Product ID={ID}: {Name}, 
category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";

    public static implicit operator int(Item v)
    {
        throw new NotImplementedException();
    }
}
