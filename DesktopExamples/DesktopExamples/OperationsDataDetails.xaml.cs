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
        public OperationsDataDetails(List<AsyncRequestData> operationsDatails)
        {
            InitializeComponent();


        }

        public OperationsDataDetails(List<SyncRequestData> operationsDatails)
        {
            InitializeComponent();


        }
    }
}
