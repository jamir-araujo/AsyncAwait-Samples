using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Sync")]
    public class SyncController : Controller
    {
        [HttpGet("{milliseconds}")]
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