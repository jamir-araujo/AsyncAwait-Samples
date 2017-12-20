using System.Linq;
using System.Collections.Generic;
using System.Windows;
using TimelineLibrary;
using System;
using System.Windows.Controls;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for OperationsDataDetails.xaml
    /// </summary>
    public partial class OperationsDataDetails : Window
    {
        public OperationsDataDetails(List<AsyncRequestData> operationsDatails)
        {
            InitializeComponent();
        }

        public OperationsDataDetails(List<SyncRequestData> operationsDatails)
        {
            InitializeComponent();

            var threadGroup = operationsDatails.GroupBy(p => p.ThreadId);

            foreach (var thread in threadGroup)
            {
                var threadId = thread.Key;
                var requests = thread.AsEnumerable()
                    .OrderBy(p => p.StartTime);

                var threadTimeline = new TimelineTray();
                
                threadTimeline.MinDateTime = requests.Min(p => p.StartTime);
                threadTimeline.MaxDateTime = requests.Max(p => p.EndTime);

                threadTimeline.RowDefinitions.Add(new RowDefinition());
                threadTimeline.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                var milliseconds10Band = new TimelineBand();
                milliseconds10Band.ItemSourceType = "milliseconds10";
                milliseconds10Band.IsMainBand = true;
                threadTimeline.Children.Add(milliseconds10Band);

                var secondsBand = new TimelineBand();
                secondsBand.ItemSourceType = "seconds";
                threadTimeline.Children.Add(secondsBand);
                

                foreach (var request in requests)
                {
                    threadTimeline.TimelineEvents.Add(new TimelineEvent
                    {
                        StartDate = request.StartTime,
                        EndDate = request.EndTime,
                        Id = request.RequestId.ToString(),
                        Title = request.RequestId.ToString(),
                        Description = GetRequestDescription(request)
                    });
                }

                threadTimeline.Run();
                ThreadItemsControl.Items.Add(threadTimeline);
            }

            //timeline.TimelineEvents.Add(new TimelineEvent { Title = "Teste", StartDate = DateTime.Now.AddMilliseconds(50), EndDate = DateTime.Now });
            //timeline.MaxDateTime = DateTime.Now.AddSeconds(1);
            //timeline.MinDateTime = DateTime.Now.AddSeconds(-1);
            //timeline.Run();
        }

        private string GetRequestDescription(SyncRequestData request)
        {
            return $"RequestId: {request.RequestId}\n" +
                $"Duration: {request.Duration}\n" +
                $"ParamValue: {request.ParamValue}\n" +
                $"ThreadId {request.ThreadId}";
        }
    }
}
