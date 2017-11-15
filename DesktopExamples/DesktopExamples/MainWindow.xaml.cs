using System.Windows;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveBigFileExampleButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new SaveBigFileExample();
            window.ShowDialog();
        }

        private void MultipleSmallAsyncOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MultipleSmallAsyncOperations();
            window.ShowDialog();
        }
    }
}
