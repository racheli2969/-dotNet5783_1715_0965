﻿/// <summary>
///defines all the enums for BL layer
/// </summary>
namespace BO;


/// <summary>
/// enum for delivery state
/// </summary>
public enum EnumOrderStatus
{
    Ordered,
    Shipped,
    Delivered
}
public enum BookGenre
{
    Fiction,
    History,
    Biography,
    Fantasy,
    SciFi,
    Mystery,
    Thriller
}

public enum OptionsForMain
{
    Exit,
    Cart,
    Order,
    Product
}
public enum OptionsForOrder
{
    Exit,
    GetOrderList,
    GetOrderDetails,
    UpdateOrderShipping,
    UpdateOrderDelivery,
    UpdateOrderDetails
}
public enum OptionsForProduct
{
    Exit,
    GetProductList,
    GetProductForManager,
    GetProductForCustomer,
    AddProduct,
    RemoveProduct,
    UpdateProduct
}
public enum OptionsForCart
{
    Exit,
    AddToCart,
    UpdateProductQuantity,
    OrderConfirmation
}