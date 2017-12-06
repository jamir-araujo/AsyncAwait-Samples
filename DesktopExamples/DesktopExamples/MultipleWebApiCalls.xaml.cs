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

            DisplayResults(SyncResults, tasks.Select(t => t.Result).ToList(), stopWatch.Elapsed);
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

            DisplayResults(AsyncResults, tasks.Select(t => t.Result).ToList(), stopWatch.Elapsed);
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
                http.BaseAddress = new Uri("http://192.168.15.5/webapi-sample/api/");

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
    }
}
