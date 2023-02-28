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

        private string? message;
        private bool progress = true;

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
        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            timerThread.Start();
            Simulator.Simulator.registerChangeEvent(changeOrder);
            Simulator.Simulator.registerStopEvent(finishSimulator);
            Simulator.Simulator.Run();
            while (!worker.CancellationPending)
            {
                worker.ReportProgress(1, EventArgs.Empty);
                Thread.Sleep(1000);
            }
        }
        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 2)
            {
                Tuple<int, BO.EnumOrderStatus?, BO.EnumOrderStatus?, int>? data = e.UserState as Tuple<int, BO.EnumOrderStatus?, BO.EnumOrderStatus?, int>;
                DataContext = data;

                BackgroundWorker progressBarWorker = new BackgroundWorker();
                progressBarWorker.DoWork += progressBarWorker_DoWork;
                progressBarWorker.ProgressChanged += progressBarWorker_ProgressChanged;
                progressBarWorker.WorkerReportsProgress = true;
                progressBarWorker.RunWorkerAsync(data.Item4);
                progress = true;

            }
        }
        private void progressBarWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (progress)
            {
                int times = int.Parse(e.Argument.ToString());
                for (int i = 1; i <= times; i++)
                {
                    (sender as BackgroundWorker).ReportProgress(100 / times * i);
                    Thread.Sleep(1000);
                }
                progress = false;
            }
        }
        private void progressBarWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.unregisterChangeEvent(changeOrder);
            Simulator.Simulator.unregisterStopEvent(finishSimulator);
        }

        /// <summary>
        /// A function that is called when there is event of StopSimulator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finishSimulator(object? sender, EventArgs e)
        {
            isTimerRunning = false;

            worker.CancelAsync();
        }
        /// <summary>
        /// A function that is called when there is event of ProgressChange, the function receved the details whose sent to the event
        /// and the function send them to the Worker_ProgressChanged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeOrder(SimulatorEventArgs e)
        {
            if (!(e is Simulator.SimulatorEventArgs))
                return;
            SimulatorEventArgs? details = e;
            Tuple<int, BO.EnumOrderStatus?, BO.EnumOrderStatus?, int> orderDetails = new(details.OrderId, details.PreviousStatus, details.NextStatus, details.RandomTime);
            worker.ReportProgress(2, orderDetails);
        }
        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {

            mainWindow.Show();
            worker.CancelAsync();
            this.Close();
        }
    }
}
