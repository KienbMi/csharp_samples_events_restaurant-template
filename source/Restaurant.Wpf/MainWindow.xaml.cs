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

        private Waiter _waiter;
        
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, EventArgs e)
        {
            FastClock.Instance.Time = DateTime.Parse("01.01.2000 12:00");
            FastClock.Instance.IsRunning = true;
            FastClock.Instance.OneMinuteIsOver += OnOneMinuteIsOver;
            _waiter = new Waiter(OnTaskReady);
        }

        private void OnOneMinuteIsOver(object source, DateTime time)
        {
            Title = $"Restaurantsimulator, {FastClock.Instance.Time.ToShortTimeString()}";
        }

        private void OnTaskReady(object source, string text)
        {
            StringBuilder sb = new StringBuilder(TextBlockLog.Text);
            sb.AppendLine($"{FastClock.Instance.Time.ToShortTimeString()} \t {text}");
            TextBlockLog.Text = sb.ToString();
        }

    }
}
