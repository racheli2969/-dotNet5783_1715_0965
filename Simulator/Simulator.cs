using BlApi;
using System;
using System.Diagnostics;
using System.Threading;
namespace Simulator;

public static class Simulator
{
    private static readonly IBl? Bl = Factory.Get();
    private static Thread? thread;
    private static volatile bool isRunning = true;

    private static BO.Order? order;
    public static BO.Order? Order { get => order; set => order = value; }

    private static int? numOfTimes;
    public static int? NumOfTimes { get => numOfTimes; set => numOfTimes = value; }

    public static void Run()
    {
        NumOfTimes = Bl?.Order?.TimeToUpdateAllDatesForSimulation();
        thread= new Thread(simulate);
        thread.Start();
    }
    public static void Stop()
    {
        isRunning = false;
    }
    private static void simulate()
    {
        int? id = Bl?.Order.GetOldestOrderNumber();
        Order = new();
        Order = Bl?.Order.GetOrderDetails((int)id);
        while (Order?.OrderId!=0 && isRunning)
        {
            if (Order?.DateShipped == DateTime.MinValue)
                Order = Bl?.Order.UpdateOrderShipping((int)id);
            else if (Order?.DateDelivered == DateTime.MinValue)
                Order = Bl?.Order.UpdateOrderDelivery((int)id);
            Thread.Sleep(1000);

            id = Bl?.Order.GetOldestOrderNumber();
            Order = new();
            Order = Bl?.Order.GetOrderDetails((int)id);
        }
    }
    private delegate BO.Order OrderInProgress(BO.Order order);
    public delegate string getDetails();
    //private static event delegate<Volatile,bool> stopSimulation;
}
