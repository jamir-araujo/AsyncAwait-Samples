using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class SyncRequestData
    {
        private static int _requestId = 0;

        public int RequestId { get; }
        public int ParamValue { get; set; }
        public TimeSpan Duration { get; set; }
        public int ThreadId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public SyncRequestData()
        {
            RequestId = GetId();
        }

        private int GetId()
        {
            return _requestId++;
        }
    }

    public class AsyncRequestData
    {
        private static int _requestId = 0;

        public int RequestId { get; }
        public int ParamValue { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int ThreadIdBeforeAsync { get; set; }
        public DateTime TimeBeforeAsync { get; set; }
        public int ThreadIdAfterAsync { get; set; }
        public DateTime TimeAfterAsync { get; set; }
        public TimeSpan AsyncDuration { get; set; }

        public AsyncRequestData()
        {
            RequestId = GetId();
        }

        private int GetId()
        {
            return _requestId++;
        }
    }
}
