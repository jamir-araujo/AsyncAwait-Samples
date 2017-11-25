using DesktopExamples.AsyncDataServiceReference;
using System.Windows;
using System;
using System.Threading.Tasks;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataServiceClient _service;
        private string _altTitle;

        public MainWindow()
        {
            InitializeComponent();

            _service = new DataServiceClient();
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

        private void MultipleSmallWCFAsyncOperationButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MultipleSmallWCFAsyncOperations();
            window.ShowDialog();
        }

        private void MultipleSmallDelais_Click(object sender, RoutedEventArgs e)
        {
            var window = new MultipleSmallDelais();
            window.ShowDialog();
        }

        private async void HowItWorksButton_Click(object sender, RoutedEventArgs e)
        {
            await DoSomethingAsync();

            Title = _altTitle;

            DoSomethingAsyncVoid();

            Title = _altTitle;
        }

        private async Task DoSomethingAsync()
        {
            var data = await _service.GetDataAsync();

            _altTitle = "DoSomethingAsync";
        }

        private async void DoSomethingAsyncVoid()
        {
            var data = await _service.GetDataAsync();

            _altTitle = "DoSomethingAsyncVoid";
        }
    }
}
