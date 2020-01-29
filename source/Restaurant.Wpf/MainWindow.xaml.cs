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
            Waiter waiter = new Waiter(OnTaskReady);
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            Title = $"RESTAURANTSIMULATION, {FastClock.Instance.Time.ToShortTimeString()}";
        }

        protected virtual void OnTaskReady(object sender, string text)
        {
            StringBuilder output = new StringBuilder(TextBlockLog.Text);
            output.Append("\n");
            output.Append($"{FastClock.Instance.Time.ToShortTimeString()}" + "\t" + text);
            TextBlockLog.Text = output.ToString();
        }
    }
}
