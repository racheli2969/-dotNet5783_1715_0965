using BlApi;
using System;
using System.Diagnostics;
using System.Threading;
namespace Simulator;

public static class Simulator
{
    private static readonly IBl? Bl = Factory.Get();
    private static Thread thread = new Thread(simulate);
    private static volatile bool isRunning = true;
    private static BO.Order? order;
    public static BO.Order? Order { get => order; }
    public static void Run()
    {
        thread.Start();
    }
    public static void Stop()
    {
        isRunning = false;
    }
    private static void simulate()
    {
        int? id = Bl?.Order.GetOldestOrderNumber();
        while (order != default && isRunning)
        {
            order = new();
            order = Bl?.Order.GetOrderDetails((int)id);

            if (order?.DateShipped == DateTime.MinValue)
                order = Bl?.Order.UpdateOrderShipping((int)id);
            else if (order?.DateDelivered == DateTime.MinValue)
                order = Bl?.Order.UpdateOrderDelivery((int)id);
            Thread.Sleep(1000);
            id = Bl?.Order.GetOldestOrderNumber();
        }
    }
    private delegate BO.Order OrderInProgress(BO.Order order);

    //private static event delegate<Volatile,bool> stopSimulation;
}
