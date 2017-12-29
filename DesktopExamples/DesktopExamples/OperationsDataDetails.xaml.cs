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
        public OperationsDataDetails(List<AsyncRequestData> requests)
        {
            InitializeComponent();

            var threadIds = requests
                .Aggregate(new List<int>(), AggregateThreads)
                .Distinct();

            foreach (var threadId in threadIds)
            {
                var threadTimeline = new TimelineTray();

                threadTimeline.MinDateTime = requests.Min(p => p.StartTime);
                threadTimeline.MaxDateTime = requests.Max(p => p.EndTime);

                threadTimeline.RowDefinitions.Add(new RowDefinition());
                threadTimeline.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                var milliseconds10Band = new TimelineBand();
                milliseconds10Band.ItemSourceType = "milliseconds100";
                milliseconds10Band.IsMainBand = true;
                threadTimeline.Children.Add(milliseconds10Band);

                var secondsBand = new TimelineBand();
                secondsBand.ItemSourceType = "seconds";
                threadTimeline.Children.Add(secondsBand);

                foreach (var request in requests.Where(p => p.ThreadIdBeforeAsync == threadId))
                {
                    threadTimeline.TimelineEvents.Add(new TimelineEvent
                    {
                        StartDate = request.StartTime,
                        EndDate = request.TimeBeforeAsync,
                        Id = request.RequestId.ToString(),
                        Title = request.RequestId.ToString(),
                        Description = GetBeginOfRequestDescription(request),
                        EventColor = "Green",
                        RowOverride = 0,
                        HeightOverride = 30
                    });
                }

                foreach (var request in requests.Where(p => p.ThreadIdAfterAsync == threadId))
                {
                    threadTimeline.TimelineEvents.Add(new TimelineEvent
                    {
                        StartDate = request.TimeAfterAsync,
                        EndDate = request.EndTime,
                        Id = request.RequestId.ToString(),
                        Title = request.RequestId.ToString(),
                        Description = GetEndOfRequestDescription(request),
                        EventColor = "Red",
                        RowOverride = 1,
                        HeightOverride = 30
                    });
                }

                threadTimeline.Run();
                ThreadItemsControl.Items.Add(threadTimeline);
            }
        }

        private string GetBeginOfRequestDescription(AsyncRequestData request)
        {
            return $"Begin of Async Request\n" +
                $"RequestId: {request.RequestId}\n" +
                $"Thread worked for: {(request.TimeBeforeAsync - request.StartTime).ToString()}\n" +
                $"ParamValue: {request.ParamValue}\n" +
                $"ThreadId: {request.ThreadIdBeforeAsync}\n" +
                $"Start: {request.StartTime}\n" +
                $"Entering async: {request.TimeBeforeAsync}";
        }

        private string GetEndOfRequestDescription(AsyncRequestData request)
        {
            return $"End of Async Request\n" +
                $"RequestId: {request.RequestId}\n" +
                $"Thread worked for: {(request.EndTime - request.TimeAfterAsync).ToString()}\n" +
                $"ParamValue: {request.ParamValue}\n" +
                $"ThreadId: {request.ThreadIdAfterAsync}\n" +
                $"Leaving async: {request.TimeAfterAsync}\n" +
                $"Request total duration: {request.EndTime - request.StartTime}";
        }

        private List<int> AggregateThreads(List<int> list, AsyncRequestData asyncRequestData)
        {
            list.Add(asyncRequestData.ThreadIdBeforeAsync);
            list.Add(asyncRequestData.ThreadIdAfterAsync);

            return list;
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
                milliseconds10Band.ItemSourceType = "milliseconds100";
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
                        Description = GetRequestDescription(request),
                        RowOverride = 0,
                        HeightOverride = 30
                    });
                }

                threadTimeline.Run();
                ThreadItemsControl.Items.Add(threadTimeline);
            }
        }

        private string GetRequestDescription(SyncRequestData request)
        {
            return $"RequestId: {request.RequestId}\n" +
                $"Duration: {request.Duration}\n" +
                $"ParamValue: {request.ParamValue}\n" +
                $"ThreadId: {request.ThreadId}\n" +
                $"Start: {request.StartTime}\n" +
                $"End: {request.EndTime}";
        }
    }
}
