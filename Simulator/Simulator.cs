using BlApi;
using BlImplementation;
using System;

namespace Simulator;

public delegate void ProgressUpdatedEventHandler(SimulatorEventArgs args);
public static class Simulator
{
    private static readonly IBl? Bl = Factory.Get();

    private static volatile bool isRunning = true;
   
    private static event ProgressUpdatedEventHandler? ProgressUpdated;
    public static event EventHandler? StopSimulator;
    public static event EventHandler? StatusChange;

    public static void Run()
    {
        Thread changeStatuses = new Thread(simulate);
        changeStatuses.Start();
    }
    public static void Stop(object? sender, EventArgs args)
    {
        isRunning = false;
        StopSimulator?.Invoke(null, EventArgs.Empty);
    }
    private static void simulate()
    {
        while (isRunning)
        {
            try
            {
                int? id = Bl?.Order.GetOldestOrderNumber();
                BO.Order? Order = Bl?.Order.GetOrderDetails(id ?? throw new BlApi.BlNOtImplementedException());
                Random rnd = new Random();
                int seconds = rnd.Next(1, 6);
                if (Order != null)
                    ProgressUpdated?.Invoke(new SimulatorEventArgs(Order.OrderId, Order.OrderStatus, Order.OrderStatus + 1, seconds));

                Thread.Sleep(seconds * 1000);

                if (Order?.DateShipped == DateTime.MinValue)
                    Order = Bl?.Order.UpdateOrderShipping(Order.OrderId);
                else if (Order?.DateDelivered == DateTime.MinValue)
                    Order = Bl?.Order.UpdateOrderDelivery(Order.OrderId);
                Num idOrder = new Num(Order.OrderId);
                if (StatusChange != null)
                    StatusChange(null, idOrder);
            }
            catch (BlNOtImplementedException)
            {
                Stop(null, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Registration function for StopSimulator event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerStopEvent(EventHandler func) => StopSimulator += func;
    /// <summary>
    /// Unregistration function for StopSimulator event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterStopEvent(EventHandler func) => StopSimulator -= func;
    /// <summary>
    /// Registration function for ProgressChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerChangeEvent(ProgressUpdatedEventHandler func) => ProgressUpdated += func;
    /// <summary>
    /// Unregistration function for ProgressChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterChangeEvent(ProgressUpdatedEventHandler func) => ProgressUpdated -= func;

    /// <summary>
    /// Registration function for StatusChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerChangeStatusEvent(EventHandler func) => StatusChange += func;

    /// <summary>
    /// Unregistration function for StatusChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterChangeStatusEvent(EventHandler func) => StatusChange -= func;


}

public class SimulatorEventArgs : EventArgs
{
    public int RandomTime;
    public int OrderId;
    public BO.EnumOrderStatus? PreviousStatus;
    public BO.EnumOrderStatus? NextStatus;
    public SimulatorEventArgs(int orderId, BO.EnumOrderStatus? prevstatus, BO.EnumOrderStatus? nextstatus, int time)
    {
        RandomTime = time;
        OrderId = orderId;
        PreviousStatus = prevstatus;
        NextStatus = nextstatus;
    }
}

public class Num : EventArgs
{
    public int id;
    /// <summary>
    /// constructor of Num Class.
    /// </summary>
    public Num(int i)
    {
        id = i;
    }
}