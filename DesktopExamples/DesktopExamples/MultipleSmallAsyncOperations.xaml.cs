using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for MultipleSmallAsyncOperations.xaml
    /// </summary>
    public partial class MultipleSmallAsyncOperations : Window
    {
        public MultipleSmallAsyncOperations()
        {
            InitializeComponent();
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            var stream = File.Create("D:/temp.text");

            var stopwatch = Stopwatch.StartNew();

            var bytesToWrite = new byte[NumberOfBytesToWrite.Value ?? 0];
            var iterations = NumberOfWrites.Value ?? 0;

            SyncElapsed.Text = $"Escrevendo {bytesToWrite.Length} bytes, {iterations} vezes";

            for (int i = 0; i < iterations; i++)
            {
                stream.Write(bytesToWrite, 0, bytesToWrite.Length);
                stream.Flush();
            }

            stopwatch.Stop();

            SyncElapsed.Text = stopwatch.Elapsed.ToString();

            stream.Dispose();
        }

        private async void AsyncButton_Click(object sender, RoutedEventArgs e)
        {
            var stream = File.Create("D:/temp.text");

            var stopwatch = Stopwatch.StartNew();

            var bytesToWrite = new byte[NumberOfBytesToWrite.Value ?? 0];
            var iterations = NumberOfWrites.Value ?? 0;

            AsyncElapsed.Text = $"Escrevendo {bytesToWrite.Length} bytes, {iterations} vezes";

            for (int i = 0; i < iterations; i++)
            {
                await stream.WriteAsync(bytesToWrite, 0, bytesToWrite.Length);
                await stream.FlushAsync();
            }

            stopwatch.Stop();

            AsyncElapsed.Text = stopwatch.Elapsed.ToString();

            stream.Dispose();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            File.Delete("D:/temp.text");
        }
    }
}
