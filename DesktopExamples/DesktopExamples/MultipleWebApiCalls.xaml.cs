using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for MultipleWebApiCalls.xaml
    /// </summary>
    public partial class MultipleWebApiCalls : Window
    {
        private List<OperationData> _lastSyncoperationsData;
        private List<OperationData> _lastAsyncOperationsData;

        public MultipleWebApiCalls()
        {
            InitializeComponent();
        }

        private async void SyncRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();

            var tasks = new List<Task<OperationData>>
            {
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 1000),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50),
                GetAsync("sync", 50)
            };

            await Task.WhenAll(tasks);
            stopWatch.Stop();

            var operationsData = tasks.Select(t => t.Result).ToList();
            DisplayResults(SyncResults, operationsData, stopWatch.Elapsed);

            _lastSyncoperationsData = operationsData;
            ViewSyncDetails.IsEnabled = true;
        }

        private async void AsyncRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();

            var tasks = new List<Task<OperationData>>
            {
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 1000),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50),
                GetAsync("async", 50)
            };

            await Task.WhenAll(tasks);

            stopWatch.Stop();

            var operationsData = tasks.Select(t => t.Result).ToList();
            DisplayResults(AsyncResults, tasks.Select(t => t.Result).ToList(), stopWatch.Elapsed);

            _lastAsyncOperationsData = operationsData;
            ViewAsyncDetails.IsEnabled = true;
        }

        private void DisplayResults(Panel panel, List<OperationData> operationResults, TimeSpan duration)
        {
            var requestResultsData = new RequestResultsData();

            requestResultsData.RequestCount = operationResults.Count;
            requestResultsData.Duration = duration.ToString();
            requestResultsData.LongestRequestTime = operationResults.Max(o => o.Duration).ToString();
            requestResultsData.ShortestRequestTime = operationResults.Min(o => o.Duration).ToString();

            requestResultsData.ThreadCount = operationResults.Aggregate(new List<int>(), (list, o) => 
            {
                list.Add(o.StartingThread);
                list.Add(o.EndThread);
                return list;
            })
            .Distinct()
            .Count();

            panel.DataContext = requestResultsData;
        }

        private async Task<OperationData> GetAsync(string controller, int milliseconds)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri("http://192.168.15.6/webapi-sample/api/");

                var response = await http.GetAsync($"{controller}/{milliseconds}");
                return await response.Content.ReadAsJsonAsync<OperationData>();
            }
        }

        public class RequestResultsData
        {
            public int RequestCount { get; set; }
            public int ThreadCount { get; set; }
            public string Duration { get; set; }
            public string LongestRequestTime { get; set; }
            public string ShortestRequestTime { get; set; }
        }

        private void ViewSyncDetails_Click(object sender, RoutedEventArgs e)
        {
            var window = new OperationsDataDetails(_lastSyncoperationsData);
            window.Show();
        }

        private void ViewAsyncDetails_Click(object sender, RoutedEventArgs e)
        {
            var window = new OperationsDataDetails(_lastAsyncOperationsData);
            window.Show();
        }
    }
}
