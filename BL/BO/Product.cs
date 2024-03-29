﻿namespace BO;
public class Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public BookGenre Category { get; set; }
    public int AmountInStock { get; set; }
    
    public override string ToString() => $@"
---------------------------------------------
    Product ID: {ID} 
    name: {Name}
    category: {Category}
    Price: {Price}
    Amount in stock: {AmountInStock}
---------------------------------------------
";

}
