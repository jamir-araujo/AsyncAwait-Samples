using System.Web.Http;

namespace WebApi.Controllers
{
    public class SyncController : ApiController
    {
        public string Get(int id)
        {
            return "value";
        }
    }
}
