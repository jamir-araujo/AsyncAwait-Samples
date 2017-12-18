using System.Linq;
using System.Collections.Generic;
using System.Windows;
using TimelineLibrary;

namespace DesktopExamples
{
    /// <summary>
    /// Interaction logic for OperationsDataDetails.xaml
    /// </summary>
    public partial class OperationsDataDetails : Window
    {
        public OperationsDataDetails(List<OperationData> operationsDatails)
        {
            InitializeComponent();

            timeline.MinDateTime = operationsDatails.Min(p => p.StartTime);
            timeline.MaxDateTime = operationsDatails.Max(p => p.EndTime);

            var startThread = operationsDatails
                .GroupBy(p => p.StartingThread)
                .ToDictionary(p => p.Key, p => p.ToList());

            var endThread = operationsDatails
                .GroupBy(p => p.EndThread)
                .ToDictionary(p => p.Key, p => p.ToList());

            var threads = operationsDatails
                .Aggregate(new List<int>(), AggregateThreads)
                .Distinct()
                .ToDictionary(threadId => threadId, threadId => new List<OperationData>())
                .Select(thread =>
                {
                    thread.Value.AddRange(startThread[thread.Key]);
                    thread.Value.AddRange(endThread[thread.Key]);

                    return thread.Value;
                })
                .Distinct()
                .ToList();
        }

        private static List<int> AggregateThreads(List<int> list, OperationData operationData)
        {
            list.Add(operationData.StartingThread);
            list.Add(operationData.EndThread);

            return list;
        }
    }
}
