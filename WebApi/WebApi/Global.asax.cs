using StackExchange.Redis;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IConnectionMultiplexer RedisConnection { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var configurationOptions = ConfigurationOptions.Parse("127.0.0.1:6379");
            configurationOptions.SyncTimeout = 10000;
            RedisConnection = ConnectionMultiplexer.Connect(configurationOptions);

            ThreadPool.SetMaxThreads(5, 5);
        }
    }
}
