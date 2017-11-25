using System.Diagnostics;
using System.Windows;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for MultipleSmallWCFAsyncOperations.xaml
    /// </summary>
    public partial class MultipleSmallWCFAsyncOperations : Window
    {
        private readonly AsyncDataServiceReference.DataServiceClient _taskService;
        private readonly DataServiceReference.DataServiceClient _eventService;

        private Stopwatch _eventServiceStopwatch;

        public MultipleSmallWCFAsyncOperations()
        {
            InitializeComponent();

            _eventService = new DataServiceReference.DataServiceClient();
            _eventService.GetDataCompleted += _syncService_GetDataCompleted;

            _taskService = new AsyncDataServiceReference.DataServiceClient();
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            var numberOfCalls = NumberOfCalls.Value.Value;

            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < numberOfCalls; i++)
            {
                _taskService.GetData();
            }

            stopwatch.Stop();

            SyncElapsed.Text = stopwatch.Elapsed.ToString();
        }

        private void AsyncEventButton_Click(object sender, RoutedEventArgs e)
        {
            var numberOfCalls = NumberOfCalls.Value.Value;

            _eventServiceStopwatch = Stopwatch.StartNew();

            _eventService.GetDataAsync(numberOfCalls);
        }

        private void _syncService_GetDataCompleted(object sender, DataServiceReference.GetDataCompletedEventArgs e)
        {
            var numberOfCalls = (int)e.UserState;

            numberOfCalls--;

            if (numberOfCalls == 0)
            {
                _eventServiceStopwatch.Stop();

                AsyncEventElapsed.Text = _eventServiceStopwatch.Elapsed.ToString();
            }
            else
            {
                _eventService.GetDataAsync(numberOfCalls);
            }
        }

        private async void AsyncAwaitButton_Click(object sender, RoutedEventArgs e)
        {
            var numberOfCalls = NumberOfCalls.Value.Value;

            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < numberOfCalls; i++)
            {
                await _taskService.GetDataAsync();
            }

            stopwatch.Stop();

            AsyncAwaitElapsed.Text = stopwatch.Elapsed.ToString();
        }
    }
}
