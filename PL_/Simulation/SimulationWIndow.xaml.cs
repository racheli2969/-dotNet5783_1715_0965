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
            Simulator.Simulator.Stop();
            isTimerRunning = false;
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            resultLabel.Content = (progress + "%");
            progressBar.Value = e.ProgressPercentage;

            BO.Order? order = new BO.Order();
            order = Simulator.Simulator.Order;
            if (order?.OrderId != 0)
                switch (order?.OrderStatus)
                {
                    default: break;
                    case BO.EnumOrderStatus.Ordered:
                        txtBlockPrevState.Content = "";
                        txtCurrentState.Content = $"id= {order.OrderId}\nstatus={order.OrderStatus}";
                        txtBlockNextState.Content = $"id= {order.OrderId}\nstatus={BO.EnumOrderStatus.Shipped}";
                        break;
                    case BO.EnumOrderStatus.Shipped:
                        txtBlockPrevState.Content = $"id= {order.OrderId}\nstatus={BO.EnumOrderStatus.Delivered}";
                        txtCurrentState.Content = $"id= {order.OrderId}\nstatus={order.OrderStatus}";
                        txtBlockNextState.Content = $"id= {order.OrderId}\nstatus={BO.EnumOrderStatus.Delivered}";
                        break;

                }
            else
            {
                txtBlockPrevState.Content = "";
                txtCurrentState.Content = "";
                txtBlockNextState.Content = "";
                resultLabel.Content = (100 + "%");
                progressBar.Value = 100;
                worker.CancelAsync();
            }
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            timerThread.Start();
            Simulator.Simulator.Run();
            int i = 0;
            int? j;
            if (Simulator.Simulator.NumOfTimes == 0 || Simulator.Simulator.NumOfTimes == null)
                return;
            j = 100 / Simulator.Simulator.NumOfTimes;
            while (!worker.CancellationPending)
            {
                worker.ReportProgress((int)(i * j));
                if (i * j >= 100)
                    worker.CancelAsync();
                Thread.Sleep(1000);
                i++;
            }
        }

        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            worker.CancelAsync();
            this.Close();
        }
    }
}
