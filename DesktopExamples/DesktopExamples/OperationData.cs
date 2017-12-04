using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopExamples
{
    public class OperationData
    {
        public TimeSpan Duration { get; set; }
        public int StartingThread { get; set; }
        public DateTime StartTime { get; set; }
        public int EndThread { get; set; }
        public DateTime EndTime { get; set; }
    }
}
