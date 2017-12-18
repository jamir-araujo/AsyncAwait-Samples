using System;

namespace WebApi.Models
{
    public class OperationData
    {
        public Guid OperationId { get; set; }
        public int ParamValue { get; set; }
        public TimeSpan Duration { get; set; }
        public int StartingThread { get; set; }
        public DateTime StartTime { get; set; }
        public int EndThread { get; set; }
        public DateTime EndTime { get; set; }
    }
}