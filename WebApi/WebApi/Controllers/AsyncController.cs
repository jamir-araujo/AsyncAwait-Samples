using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AsyncController : ApiController
    {
        private readonly IDatabase _database;

        public AsyncController()
        {
            _database = WebApiApplication.RedisConnection.GetDatabase(0);
        }

        public async Task<OperationData> Get(int milliseconds)
        {
            var operationData = new OperationData();
            operationData.ParamValue = milliseconds;
            operationData.StartingThread = Thread.CurrentThread.ManagedThreadId;
            operationData.StartTime = DateTime.Now;

            var stopwatch = Stopwatch.StartNew();

            await _database.ExecuteOperationAsync(milliseconds).ConfigureAwait(false);

            stopwatch.Stop();

            operationData.Duration = stopwatch.Elapsed;
            operationData.EndThread = Thread.CurrentThread.ManagedThreadId;
            operationData.EndTime = DateTime.Now;

            return operationData;
        }
    }
}