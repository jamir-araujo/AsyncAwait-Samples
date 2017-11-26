using System.Web.Http;
using StackExchange.Redis;
using System.Diagnostics;

namespace WebApi.Controllers
{
    public class SyncController : ApiController
    {
        private readonly IDatabase _database;

        public SyncController()
        {
            _database = WebApiApplication.RedisConnection.GetDatabase(0);
        }

        public string Get(int id)
        {
            var stopwatch = Stopwatch.StartNew();

            _database.ExecuteOperation(id);

            return stopwatch.Elapsed.ToString();
        }
    }
}
