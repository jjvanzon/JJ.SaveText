using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace JJ.Demos.JavaScript
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}