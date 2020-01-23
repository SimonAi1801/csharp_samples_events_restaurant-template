using Restaurant.Core;
using System;
using System.Text;

namespace Restaurant.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            FastClock.Instance.Factor = 60;
            FastClock.Instance.Time = DateTime.Parse("12:00");
            FastClock.Instance.IsRunning = true;
            Waiter waiter = new Waiter(OnReadyTask);
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            Title = $"RESTAURANTSIMULATION, {FastClock.Instance.Time.ToShortTimeString()}";
        }

        protected void OnReadyTask(object sender, Order order)
        {
            string line = String.Empty;
            AddLineToTextBox(line, order);
        }

        void AddLineToTextBox(string line, Order order)
        {
            StringBuilder text = new StringBuilder(TextBlockLog.Text);
            //text.Append("\n");
            //text.Append(FastClock.Instance.Time.ToShortTimeString() + " \t ");
            //text.Append(line + " \t ");
            //text.Append(FastClock.Instance.Time.ToString() + " \t ");
            TextBlockLog.Text = text.ToString();
        }
    }
}
