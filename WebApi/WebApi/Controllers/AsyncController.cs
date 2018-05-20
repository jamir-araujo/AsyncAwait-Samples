using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Async")]
    public class AsyncController : Controller
    {
        [HttpGet("{milliseconds}")]
        public async Task<AsyncRequestData> Get(int milliseconds)
        {
            var requestData = new AsyncRequestData();
            requestData.StartTime = DateTime.Now;
            requestData.ParamValue = milliseconds;

            requestData.ThreadIdBeforeAsync = Thread.CurrentThread.ManagedThreadId;
            requestData.TimeBeforeAsync = DateTime.Now;

            await Task.Delay(TimeSpan.FromMilliseconds(milliseconds));

            requestData.ThreadIdAfterAsync = Thread.CurrentThread.ManagedThreadId;
            requestData.TimeAfterAsync = DateTime.Now;
            requestData.AsyncDuration = requestData.TimeAfterAsync - requestData.TimeBeforeAsync;

            requestData.EndTime = DateTime.Now;
            requestData.Duration = requestData.EndTime - requestData.StartTime;

            return requestData;
        }
    }
}