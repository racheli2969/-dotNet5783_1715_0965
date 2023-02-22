using Simulator;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;

namespace PL_
{
    /// <summary>
    /// Interaction logic for SimulationWIndow.xaml
    /// </summary>
    public partial class SimulationWIndow : Window
    {
        private BackgroundWorker worker { get; set; }
        private MainWindow mainWindow;
        private Stopwatch stopwatch;
        private Thread timerThread;
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
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Start();
        }

        private void run_timer(object? obj)
        {
            while (true)
            {
                string timerText = stopwatch.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);

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
            if (e.Cancelled == true)
            {
                //e.Result throw new System.InvalidOperationException;
                //resultLabel.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                // resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (result < 1000) ;
                // resultLabel.Content = "Done after " + result + " ms.";
                else;
                // resultLabel.Content = "Done after " + result / 1000 + " sec.";

            }
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            //resultLabel.Content = (progress + "%");
            //resultProgressBar.Value = progress;
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {

            timerThread.Start();
            //
            //i.	רשמו מתודות משקיפות (ראו בהמשך) לאירועי הסימולטור
            Simulator.Simulator.Run();
            int i = 0;
            while (!worker.CancellationPending)//worker.CancellationPending
            {
                System.Threading.Thread.Sleep(1000);
                worker.ReportProgress(++i * 20);
            }
        }

        private void SimulationBtn_Click(object sender, RoutedEventArgs e)
        {
            worker = new BackgroundWorker();

            worker.DoWork += (object? sender, DoWorkEventArgs e) =>
            {
                BLObject.StartSimulation(
                   droneBL,
                   worker,
                   (drone) => { BLObject.UpdateDataDrone(droneBL); worker.ReportProgress(1); },
                   () => worker.CancellationPending);

            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) =>
            {
                DronePL.updateDrone(droneBL);
                PLLists.UpdateDrone(droneBL);
                createButtons(droneBL);
            };

            worker.RunWorkerCompleted += (object? sender, RunWorkerCompletedEventArgs e) =>
            {
                SimulationBtn.Content = "start simulation";
                worker.CancelAsync();
                createButtons(droneBL);
            };
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }
    }
}

    }
}
