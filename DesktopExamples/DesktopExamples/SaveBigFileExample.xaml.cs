using System.ComponentModel;
using System.IO;
using System.Windows;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for SaveBigFileExample.xaml
    /// </summary>
    public partial class SaveBigFileExample : Window
    {
        public SaveBigFileExample()
        {
            InitializeComponent();
        }

        private void SaveFileSyncButton_Click(object sender, RoutedEventArgs e)
        {
            var bytes = new byte[500_000_000];

            File.Delete("D://temp.txt");

            using (var stream = new FileStream("D://temp.txt", FileMode.Create, FileAccess.ReadWrite))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

        private async void SaveFileAsyncButton_Click(object sender, RoutedEventArgs e)
        {
            var bytes = new byte[500_000_000];

            File.Delete("D://temp.txt");

            using (var stream = new FileStream("D://temp.txt", FileMode.Create, FileAccess.ReadWrite))
            {
                await stream.WriteAsync(bytes, 0, bytes.Length);
                await stream.FlushAsync();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            File.Delete("D://temp.txt");
        }
    }
}
