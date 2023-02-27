using Simulator;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace PL_
{
    /// <summary>
    /// Interaction logic for SimulationWIndow.xaml
    /// </summary>
    public partial class SimulationWIndow : Window
    {
        private BackgroundWorker worker { get; set; }
        private MainWindow mainWindow;

        //timer variables
        private Stopwatch stopwatch;
        private Thread timerThread;
        private volatile bool isTimerRunning = true;
        private string ?message;

        //Prevent close button variables

        private const int GWL_STYLE = -16;

        private const int WS_SYSMENU = 0x80000;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public SimulationWIndow(MainWindow mw)
        {
            InitializeComponent();

            mainWindow = mw;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();

            stopwatch = new();
            stopwatch.Restart();
            timerThread = new Thread(run_timer);
            Loaded += RemoveCloseButton;
        }

        void RemoveCloseButton(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        private void run_timer(object? obj)
        {
            while (isTimerRunning)
            {
                string timerText = stopwatch.Elapsed.ToString();
                timerText = timerText.Substring(3, 5);

                setTextInvoke(timerText);
                Thread.Sleep(1000);
            }
        }

        private void setTextInvoke(string timerText)
        {
            if (!CheckAccess())
            {
                Action<string> d = setTextInvoke;
                Dispatcher.BeginInvoke(d, new object[] { timerText });
            }
            else
                lblTimer.Content = timerText;
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.unregisterChangeEvent(changeOrder);
            Simulator.Simulator.unregisterStopEvent(finishSimulator);
            //  Simulator.Simulator.Stop();
            isTimerRunning = false;
        }
        /// <summary>
        /// A function that is called when there is event of StopSimulator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finishSimulator(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }
        /// <summary>
        /// A function that is called when there is event of ProgressChange, the function receved the details whose sent to the event
        /// and the function send them to the Worker_ProgressChanged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is Simulator.SimulatorEventArgs))
                return;
            SimulatorEventArgs? details = e as SimulatorEventArgs;
            Tuple<int, BO.EnumOrderStatus, BO.EnumOrderStatus,  int> orderDetails = new(details.OrderId, details.PreviousStatus, details.NextStatus, details.RandomTime);
            worker.ReportProgress(2, orderDetails);
        }


        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Tuple<int, BO.EnumOrderStatus, BO.EnumOrderStatus,  int> data = (Tuple<int, BO.EnumOrderStatus, BO.EnumOrderStatus, int>)e.UserState;
            DataContext = data;

            //BO.Order? order = Simulator.Simulator.Order;
            //if (order == null)
            //    return;

            //if (order?.OrderId != 0)
            //    switch (order?.OrderStatus)
            //    {
            //        default: break;
            //        case BO.EnumOrderStatus.Ordered:
            //            txtCurrentState.Content = $"id= {order.OrderId}\nstatus={order.OrderStatus}\nnext status={BO.EnumOrderStatus.Shipped}";
            //            break;
            //        case BO.EnumOrderStatus.Shipped:
            //            txtCurrentState.Content = $"id= {order.OrderId}\nstatus={order.OrderStatus}\nnext status={BO.EnumOrderStatus.Delivered}";
            //            break;
            //    }
            //else
            //{

            //    txtCurrentState.Content = "simulation done";
            //    //resultLabel.Content = (100 + "%");
            //    //progressBar.Value = 100;
            //    worker.CancelAsync();
            //    return;
            //}
            // int progress =0;

            //progressBar.Value = 0;
            //int? numoftimesleft = Simulator.Simulator.RandomNum;
            //for (int i = 0; i < Simulator.Simulator.RandomNum * 1000; i++)
            //{
            //    resultLabel.Content = (i + "%");
            //    progressBar.Value = i/100;
            //    if (i % 1000 == 0)
            //    {
            //        numoftimesleft--;
            //    }
            //   // Thread.Sleep(100); 
            //}
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            timerThread.Start();
            Simulator.Simulator.registerChangeEvent(changeOrder);
            Simulator.Simulator.registerStopEvent(finishSimulator);
            Simulator.Simulator.Run();
            int i = 0;
            //int? j;
            //if (Simulator.Simulator.NumOfTimes == 0 || Simulator.Simulator.NumOfTimes == null)
            //    return;

            while (!worker.CancellationPending)
            {
                //i = Simulator.Simulator.RandomNum;
                //if (i != 0)
                //{
                //    j = 100 / i;

                //while (j * i < 100)
                //{
                worker.ReportProgress(++i);



                Thread.Sleep(100);
            }
        }

        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {

            mainWindow.Show();
            worker.CancelAsync();
            this.Close();
        }

        /// <summary>
        /// A function for finish Simulator that called by user click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finishSimulator_Click(object sender, RoutedEventArgs e)
        {
            message = "stopped by user";
            Simulator.Simulator.Stop(null,EventArgs.Empty);
        }

    }
}
