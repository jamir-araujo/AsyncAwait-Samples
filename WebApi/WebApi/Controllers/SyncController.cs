using System.Web.Http;
using System.Diagnostics;
using WebApi.Models;
using System.Threading;
using System;

namespace WebApi.Controllers
{
    public class SyncController : ApiController
    {
        public SyncRequestData Get(int milliseconds)
        {
            var requestData = new SyncRequestData();
            requestData.StartTime = DateTime.Now;
            requestData.ParamValue = milliseconds;
            requestData.ThreadId = Thread.CurrentThread.ManagedThreadId;

            Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));

            requestData.EndTime = DateTime.Now;
            requestData.Duration = requestData.EndTime - requestData.StartTime;

            return requestData;
        }
    }
}
