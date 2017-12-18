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
            requestData.Duration = requestData.StartTime - requestData.EndTime;

            return requestData;
        }
    }
}