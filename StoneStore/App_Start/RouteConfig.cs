using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StoneStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}",                           // URL with parameters
                new { controller = "Home", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(null, "{controller}/{action}", new
            {
                controller = "Home",
                action = "Index",
                category = (string)null,
                page = 1
            });

            routes.MapRoute(null, "{controller}/{action}/Page{page}", new
            {
                controller = "Home",
                action = "Index",
                category = (string)null
            },
            new { page = @"\d+" });

            routes.MapRoute(null, "{controller}/{action}/{category}", new
            {
                controller = "Home",
                action = "Index",
                page = 1
            });

            routes.MapRoute(null, "{controller}/{action}/{category}/Page{page}", new
            {
                controller = "Home",
                action = "Index"
            },
            new { page = @"\d+" });

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
