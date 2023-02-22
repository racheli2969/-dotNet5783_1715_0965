using BlApi;
using System;
using System.Diagnostics;
using System.Threading;
namespace Simulator;

public static class Simulator
{
    private static readonly IBl? Bl = Factory.Get();
    private static Thread thread = new(Run);
    private static volatile bool isRunning = true;

    public static void Run()
    {
        int? id = Bl?.Order.GetOldestOrderNumber();
        while (id != null && isRunning)
        {
            BO.Order? order = new();
            order = Bl?.Order.GetOrderDetails((int)id);
           
            if (order?.DateShipped == DateTime.MinValue)
                order = Bl?.Order.UpdateOrderShipping((int)id);
            else if (order?.DateDelivered == DateTime.MinValue)
                order = Bl?.Order.UpdateOrderDelivery((int)id);
           

            id = Bl?.Order.GetOldestOrderNumber();
        }
    }
    public static void Stop()
    {
        isRunning = false;
    }
    private delegate BO.Order OrderInProgress(BO.Order order);

    //static BO.Order getCurrentOrder(BO.Order order)
    //{
    //    return order;
    //}

    //private static event delegate<Volatile,bool> stopSimulation;
}
