using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AsyncController : ApiController
    {
        public Task<string> Get()
        {
            return Task.FromResult("");
        }
    }
}