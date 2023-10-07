using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
///using System.Web.Mvc;
//using System.Web.Optimization;
using System.Web.Routing;

namespace FMCGAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );
            /*AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);*/
        }
    }
}
