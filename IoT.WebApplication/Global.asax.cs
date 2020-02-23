using Microsoft.WindowsAzure.ServiceRuntime;
using Orleans.Host;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IoT.WebApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (RoleEnvironment.IsAvailable)
            {
                // azure
                OrleansAzureClient.Initialize(Server.MapPath("~/AzureConfiguration.xml"));
            }
            else
            {
                // not in azure
                Orleans.OrleansClient.Initialize(Server.MapPath("~/ClientConfiguration.xml"));
            }
        }
    }
}
