using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

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

        private async void AsyncCallsButton_Click(object sender, RoutedEventArgs e)
        {
            var tasks = new List<Task<OperationData>>
            {
                GetAsync(1000),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(1000),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(1000),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(1000),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(1000),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50),
                GetAsync(50)
            };


            await Task.WhenAll(tasks.ToArray());

            async Task<OperationData> GetAsync(int milliseconds)
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri("http://localhost:54142/api/");

                    var response = await http.GetAsync($"async/{milliseconds}");
                    return await response.Content.ReadAsJsonAsync<OperationData>();
                }
            }
        }

        private void SyncCallsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
