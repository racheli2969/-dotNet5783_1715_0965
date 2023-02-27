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

    public static event EventHandler? StopSimulator;
    public static event EventHandler? ProgressChange;
    public static event EventHandler? StatusChange;
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
    //public static void Stop()
    //{
    //    isRunning = false;
    //    SimulationStopCompleted?.Invoke(null, EventArgs.Empty);
    //}
    private static void simulate()
    {
        int? id = Bl?.Order.GetOldestOrderNumber();
        if (id == null)
        {
            Stop(null, EventArgs.Empty);
           
        }
        BO.Order crrntOrder = Bl.Order.GetOrderDetails((int)id);
        Random rnd = new Random();
        int seconds = rnd.Next(1, 6);
        SimulatorEventArgs details = new SimulatorEventArgs(crrntOrder.Id, (eOrderStatus)crrntOrder.status, (eOrderStatus)((int)crrntOrder.status + 1), seconds);
        if (ProgressChange != null)
            ProgressChange(null, details);
        Thread.Sleep(seconds * 1000);
        if (crrntOrder.status == eOrderStatus.confirmed)
        {
            bl.Order.UpdateOrdShipping(crrntOrder.Id);
        }
        else
        {
            bl.Order.UpdateOrdDelivery(crrntOrder.Id);
        }

      
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
            ProgressUpdated?.Invoke(new SimulatorEventArgs(order.OrderId, (BO.EnumOrderStatus)order.OrderStatus, (BO.EnumOrderStatus)order.OrderStatus + 1, RandomNum));
            Thread.Sleep(randomNum * 1000);
        }
    }

    /// <summary>
    /// Registration function for StopSimulator event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerStopEvent(EventHandler func)
    {
        StopSimulator += func;
    }
    /// <summary>
    /// Unregistration function for StopSimulator event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterStopEvent(EventHandler func)
    {
        StopSimulator -= func;
    }
    /// <summary>
    /// Registration function for ProgressChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerChangeEvent(EventHandler func)
    {
        ProgressChange += func;
    }
    /// <summary>
    /// Unregistration function for ProgressChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterChangeEvent(EventHandler func)
    {
        ProgressChange -= func;
    }
    /// <summary>
    /// Registration function for StatusChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void registerChangeStatusEvent(EventHandler func)
    {
        StatusChange += func;
    }
    /// <summary>
    /// Unregistration function for StatusChange event.
    /// </summary>
    /// <param name="func"></param>
    public static void unregisterChangeStatusEvent(EventHandler func)
    {
        StatusChange -= func;
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
    public BO.EnumOrderStatus PreviousStatus;
    public BO.EnumOrderStatus NextStatus;
    public SimulatorEventArgs(int orderId, BO.EnumOrderStatus prevstatus, BO.EnumOrderStatus nextstatus, int time)
    {
        RandomTime = time;
        OrderId = orderId;
        PreviousStatus = prevstatus;
        NextStatus = nextstatus;
    }
}

//public static class Simulator
//{
//    private static BlApi.IBl bl = BlApi.Factory.Get();
//    private static bool continuing = true;
//    public static event EventHandler StopSimulator;
//    public static event EventHandler ProgressChange;
//    public static event EventHandler StatusChange;

//    /// <summary>
//    /// A function that update statuses of orders and call the ProgressChange event for each updating.
//    /// </summary>
//    //private static void ChangeStatuses()
//    //{
//    //    while (continuing)
//    //    {
//    //        try
//    //        {
//    //            continuing = true;
//    //            int? id = bl.Order.GetOldestOrder();
//    //            if (id == null)
//    //            {
//    //                Stop();
//    //                break;
//    //            }
//    //            Order crrntOrder = bl.Order.ReadOrd((int)id);
//    //            Random rnd = new Random();
//    //            int seconds = rnd.Next(1, 6);
//    //            Details details = new Details(crrntOrder.Id, (eOrderStatus)crrntOrder.status, (eOrderStatus)((int)crrntOrder.status + 1), seconds);
//    //            if (ProgressChange != null)
//    //                ProgressChange(null, details);
//    //            Thread.Sleep(seconds * 1000);
//    //            if (crrntOrder.status == eOrderStatus.confirmed)
//    //            {
//    //                bl.Order.UpdateOrdShipping(crrntOrder.Id);
//    //            }
//    //            else
//    //            {
//    //                bl.Order.UpdateOrdDelivery(crrntOrder.Id);
//    //            }
//    //            Num idOrder = new Num(crrntOrder.Id);
//    //            if (StatusChange != null)
//    //                StatusChange(null, idOrder);
//    //        }
//    //        catch (InvalidValueException err)
//    //        {
//    //            throw err;
//    //        }
//    //        catch (DataErrorException dataError)
//    //        {
//    //            throw dataError;
//    //        }
//    //        catch (Exception err)
//    //        {
//    //            throw err;
//    //        }
//    //    }
//    //}

//    /// <summary>
//    /// A function that start the thread.
//    /// </summary>
//    public static void Run()
//    {
//        Thread changeStatuses = new Thread(ChangeStatuses);
//        changeStatuses.Start();
//    }

//    /// <summary>
//    /// A function that stop the thread and call the StopSimulator event.
//    /// </summary>
//    public static void Stop()
//    {
//        continuing = false;
//        if (StopSimulator != null)
//            StopSimulator(null, EventArgs.Empty);
//    }
//    /// <summary>
//    /// Registration function for StopSimulator event.
//    /// </summary>
//    /// <param name="func"></param>
//    public static void registerStopEvent(EventHandler func)
//    {
//        StopSimulator += func;
//    }
//    /// <summary>
//    /// Unregistration function for StopSimulator event.
//    /// </summary>
//    /// <param name="func"></param>
//    public static void unregisterStopEvent(EventHandler func)
//    {
//        StopSimulator -= func;
//    }
//    /// <summary>
//    /// Registration function for ProgressChange event.
//    /// </summary>
//    /// <param name="func"></param>
//    public static void registerChangeEvent(EventHandler func)
//    {
//        ProgressChange += func;
//    }
//    /// <summary>
//    /// Unregistration function for ProgressChange event.
//    /// </summary>
//    /// <param name="func"></param>
//    public static void unregisterChangeEvent(EventHandler func)
//    {
//        ProgressChange -= func;
//    }
//    /// <summary>
//    /// Registration function for StatusChange event.
//    /// </summary>
//    /// <param name="func"></param>
//    public static void registerChangeStatusEvent(EventHandler func)
//    {
//        StatusChange += func;
//    }
//    /// <summary>
//    /// Unregistration function for StatusChange event.
//    /// </summary>
//    /// <param name="func"></param>
//    public static void unregisterChangeStatusEvent(EventHandler func)
//    {
//        StatusChange -= func;
//    }
//}


//public class Details : EventArgs
//{
//    public int id;
//    public eOrderStatus PreviousStatus;
//    public eOrderStatus NextStatus;
//    public int EstimatedTime;
//    /// <summary>
//    /// constractor of Details Class.
//    /// </summary>
//    public Details(int i, eOrderStatus PStatus, eOrderStatus NStatus, int Time)
//    {
//        id = i;
//        PreviousStatus = PStatus;
//        NextStatus = NStatus;
//        EstimatedTime = Time;
//    }
//}

//public class Num : EventArgs
//{
//    public int id;
//    /// <summary>
//    /// constractor of Num Class.
//    /// </summary>
//    public Num(int i)
//    {
//        id = i;
//    }
//}