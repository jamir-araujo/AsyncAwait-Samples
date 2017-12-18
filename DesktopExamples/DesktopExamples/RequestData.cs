using System;

namespace DesktopExamples
{
    public class SyncRequestData
    {
        public int RequestId { get; set; }
        public int ParamValue { get; set; }
        public int ThreadId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class AsyncRequestData
    {
        public int RequestId { get; set; }
        public int ParamValue { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int ThreadIdBeforeAsync { get; set; }
        public DateTime TimeBeforeAsync { get; set; }
        public int ThreadIdAfterAsync { get; set; }
        public DateTime TimeAfterAsync { get; set; }
        public TimeSpan AsyncDuration { get; set; }
    }
}
