using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for MultipleSmallDelais.xaml
    /// </summary>
    public partial class MultipleSmallDelais : Window
    {
        public MultipleSmallDelais()
        {
            InitializeComponent();
        }

        private void ThreadSleepButton_Click(object sender, RoutedEventArgs e)
        {
            var number = NumberOfDelay.Value.Value;

            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < number; i++)
            {
                Thread.Sleep(1);
            }

            stopwatch.Stop();

            ThreadSleepElapsed.Text = stopwatch.Elapsed.ToString();
        }

        private async void TaskDelayButton_Click(object sender, RoutedEventArgs e)
        {
            var number = NumberOfDelay.Value.Value;

            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < number; i++)
            {
                await Task.Delay(1);
            }

            stopwatch.Stop();

            TaskDelayElapsed.Text = stopwatch.Elapsed.ToString();
        }
    }
}
