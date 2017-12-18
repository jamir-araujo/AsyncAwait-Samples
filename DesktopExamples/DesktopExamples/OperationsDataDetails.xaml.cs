using System.Linq;
using System.Collections.Generic;
using System.Windows;

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

            var threads = operationsDatails
                .Aggregate(new List<int>(), AggregateThread)
                .Distinct()
                .ToDictionary(threadId => new List<OperationData>());

            var startThread = operationsDatails.OrderBy(p => p.StartingThread);
            var endThread = operationsDatails.OrderBy(p => p.EndThread);


        }

        private static List<int> AggregateThread(List<int> list, OperationData operationData)
        {
            list.Add(operationData.StartingThread);
            list.Add(operationData.EndThread);

            return list;
        }
    }
}
