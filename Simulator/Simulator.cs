using BlApi;
using System;
using System.Diagnostics;
using System.Threading;
namespace Simulator;

public static class Simulator
{
    private static readonly IBl? Bl = Factory.Get();
    private static Thread thread = new(Run);
    private static volatile bool isRunning=true;

    public static void Run()
    {
        while (isRunning)
        {
            int? id = Bl?.Order.GetOldestOrderNumber();
            while (id != null)
            {
                BO.Order? order = new();
                order = Bl?.Order.GetOrderDetails((int)id);
                if (order?.DateShipped == DateTime.MinValue)
                    order = Bl?.Order.UpdateOrderShipping((int)id);
                else if (order?.DateDelivered == DateTime.MinValue) 
                    order = Bl?.Order.UpdateOrderDelivery((int)id);
                // ((int)id).Invoke(OrderInProgress);

                id = Bl?.Order.GetOldestOrderNumber();
            } 
        }
    }
    public static void Stop()
    {
        isRunning = false;
    }
    private delegate int OrderInProgress();

    //private static event delegate<Volatile,bool> stopSimulation;
}
