using System.Web.Http;
using System.Diagnostics;
using WebApi.Models;
using System.Threading;
using System;

namespace WebApi.Controllers
{
    public class SyncController : ApiController
    {
        public OperationData Get(int milliseconds)
        {
            var operationData = new OperationData();
            operationData.ParamValue = milliseconds;
            operationData.StartingThread = Thread.CurrentThread.ManagedThreadId;
            operationData.StartTime = DateTime.Now;

            var stopwatch = Stopwatch.StartNew();

            Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));

            stopwatch.Stop();

            operationData.Duration = stopwatch.Elapsed;
            operationData.EndThread = Thread.CurrentThread.ManagedThreadId;
            operationData.EndTime = DateTime.Now;

            return operationData;
        }
    }
}
