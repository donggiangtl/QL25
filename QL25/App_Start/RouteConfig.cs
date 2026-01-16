using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QL25
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Admin",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Account", action = "AdminLogin", id = UrlParameter.Optional }
            //);

            // Your custom route for Account/Login
            routes.MapRoute(
                name: "Login",
                url: "Account/Login",
                defaults: new { controller = "Account", action = "Login" }
            );

            // Default route (should be last)
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
