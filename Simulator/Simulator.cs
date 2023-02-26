using BlApi;
using BlImplementation;
using System;

namespace Simulator;

public delegate void ProgressUpdatedEventHandler(SimulatorEventArgs args);
public static class Simulator
{
    private static readonly IBl? Bl = Factory.Get();
    private static Thread? thread;
    private static volatile bool isRunning = true;

    private static BO.Order? order;
    public static BO.Order? Order { get => order; set => order = value; }
    private static event EventHandler? SimulationStopCompleted;
    private static event ProgressUpdatedEventHandler? ProgressUpdated;
    private static readonly Random randomer = new();
    private static int randomNum;
    public static int RandomNum { get => randomNum; set => randomNum = value; }

    public static void Run()
    {
        thread = new(() =>
        {
            while (isRunning)
            {
                simulate();
            }
        }
       );
        thread.Start();
    }
    public static void Stop(object? sender, EventArgs args)
    {
        isRunning = false;
        SimulationStopCompleted?.Invoke(null, EventArgs.Empty);
    }
    public static void Stop()
    {
        isRunning = false;
        SimulationStopCompleted?.Invoke(null, EventArgs.Empty);
    }
    private static void simulate()
    {
        int? id = Bl?.Order.GetOldestOrderNumber();
        if (id == null)
        {
            Stop(null, EventArgs.Empty);
        }
        else
        {
            BO.Order? Order = Bl?.Order.GetOrderDetails((int)id);
            if (Order?.DateShipped == DateTime.MinValue)
                Order = Bl?.Order.UpdateOrderShipping((int)id);
            else if (Order?.DateDelivered == DateTime.MinValue)
                Order = Bl?.Order.UpdateOrderDelivery((int)id);
            int random = randomer.Next(5, 15);
            randomNum = randomer.Next(5, 15);
            ProgressUpdated?.Invoke(new SimulatorEventArgs(RandomNum, order.OrderId, (BO.EnumOrderStatus)order.OrderStatus));
            Thread.Sleep(randomNum * 1000);
        }
    }
    //private delegate BO.Order OrderInProgress(BO.Order order);
    //public delegate string getDetails();
    //private static event delegate<Volatile,bool> stopSimulation;
    public static void SimulationStopCompletedRegister(EventHandler eh) => SimulationStopCompleted += eh;

    public static void SimulationStopCompletedUnregister(EventHandler eh) => SimulationStopCompleted -= eh;

    public static void ProgressUpdatedRegister(ProgressUpdatedEventHandler eh) => ProgressUpdated += eh;

    public static void ProgressUpdatedUnregister(ProgressUpdatedEventHandler eh) => ProgressUpdated -= eh;
}

public class SimulatorEventArgs : EventArgs
{
    public int RandomTime;
    public int OrderId;
    public BO.EnumOrderStatus Status;
    public SimulatorEventArgs(int randomTime, int orderId, BO.EnumOrderStatus status)
    {
        RandomTime = randomTime;
        OrderId = orderId;
        Status = status;
    }
}