using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AsyncController : ApiController
    {
        [OutputCache]
        public async Task<OperationData> Get(int milliseconds)
        {
            var operationData = new OperationData();
            operationData.OperationId = Guid.NewGuid();
            operationData.ParamValue = milliseconds;
            operationData.StartingThread = Thread.CurrentThread.ManagedThreadId;
            operationData.StartTime = DateTime.Now;

            var stopwatch = Stopwatch.StartNew();

            await Task.Delay(TimeSpan.FromMilliseconds(milliseconds));

            stopwatch.Stop();

            operationData.Duration = stopwatch.Elapsed;
            operationData.EndThread = Thread.CurrentThread.ManagedThreadId;
            operationData.EndTime = DateTime.Now;

            return operationData;
        }
    }
}