using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// defines the item details
/// </summary>
public struct Item
{
    public string Name { get; set; }
    public int Category { get; set; }
    public float Price { get; set; }
    public bool InStock { get; set; }
    public int ID { get; set; }

    public override string ToString() => $@"
Product ID={ID}: {Name}, 
category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";


}
