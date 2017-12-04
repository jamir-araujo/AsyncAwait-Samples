using System.Web.Http;
using StackExchange.Redis;
using System.Diagnostics;
using WebApi.Models;
using System.Threading;
using System;

namespace WebApi.Controllers
{
    public class SyncController : ApiController
    {
        private readonly IDatabase _database;

        public SyncController()
        {
            _database = WebApiApplication.RedisConnection.GetDatabase(0);
        }

        public OperationData Get(int milliseconds)
        {
            var operationData = new OperationData();
            operationData.ParamValue = milliseconds;
            operationData.StartingThread = Thread.CurrentThread.ManagedThreadId;
            operationData.StartTime = DateTime.Now;

            var stopwatch = Stopwatch.StartNew();

            _database.ExecuteOperation(milliseconds);

            stopwatch.Stop();

            operationData.Duration = stopwatch.Elapsed;
            operationData.EndThread = Thread.CurrentThread.ManagedThreadId;
            operationData.EndTime = DateTime.Now;

            return operationData;
        }
    }
}
