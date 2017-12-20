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
        private List<SyncRequestData> _lastSyncoperationsData;
        private List<AsyncRequestData> _lastAsyncOperationsData;

        public MultipleWebApiCalls()
        {
            InitializeComponent();
        }

        private async void SyncRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();

            var tasks = new List<Task<SyncRequestData>>
            {
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 1000),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50),
                GetAsync<SyncRequestData>("sync", 50)
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

            var tasks = new List<Task<AsyncRequestData>>
            {
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 1000),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50),
                GetAsync<AsyncRequestData>("async", 50)
            };

            await Task.WhenAll(tasks);

            stopWatch.Stop();

            var operationsData = tasks.Select(t => t.Result).ToList();
            DisplayResults(AsyncResults, tasks.Select(t => t.Result).ToList(), stopWatch.Elapsed);

            _lastAsyncOperationsData = operationsData;
            ViewAsyncDetails.IsEnabled = true;
        }

        private void DisplayResults(Panel panel, List<SyncRequestData> operationResults, TimeSpan duration)
        {
            var requestResultsData = new RequestResultsData();

            requestResultsData.RequestCount = operationResults.Count;
            requestResultsData.Duration = duration.ToString();
            requestResultsData.LongestRequestTime = operationResults.Max(o => o.Duration).ToString();
            requestResultsData.ShortestRequestTime = operationResults.Min(o => o.Duration).ToString();

            requestResultsData.ThreadCount = operationResults
                .Select(p => p.ThreadId)
                .Distinct()
                .Count();

            panel.DataContext = requestResultsData;
        }

        private void DisplayResults(Panel panel, List<AsyncRequestData> operationResults, TimeSpan duration)
        {
            var requestResultsData = new RequestResultsData();

            requestResultsData.RequestCount = operationResults.Count;
            requestResultsData.Duration = duration.ToString();
            requestResultsData.LongestRequestTime = operationResults.Max(o => o.Duration).ToString();
            requestResultsData.ShortestRequestTime = operationResults.Min(o => o.Duration).ToString();

            requestResultsData.ThreadCount = operationResults.Aggregate(new List<int>(), (list, o) =>
            {
                list.Add(o.ThreadIdBeforeAsync);
                list.Add(o.ThreadIdAfterAsync);
                return list;
            })
            .Distinct()
            .Count();

            panel.DataContext = requestResultsData;
        }

        private async Task<T> GetAsync<T>(string controller, int milliseconds)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri("http://192.168.15.3/webapi-sample/api/");

                var response = await http.GetAsync($"{controller}/{milliseconds}");
                return await response.Content.ReadAsJsonAsync<T>();
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
