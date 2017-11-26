using StackExchange.Redis;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AsyncController : ApiController
    {
        private readonly IDatabase _database;

        public AsyncController()
        {
            _database = WebApiApplication.RedisConnection.GetDatabase(0);
        }

        public async Task<string> Get(int id)
        {
            var stopwatch = Stopwatch.StartNew();

            await _database.ExecuteOperationAsync(id);

            return stopwatch.Elapsed.ToString();
        }
    }
}