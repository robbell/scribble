using System.Web.Mvc;
using System.Web.Routing;

namespace Scribble.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Root",
                url: string.Empty,
                defaults: new { controller = "Post", action = "Recent" },
                namespaces: new[] { "Scribble.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Post",
                url: "{year}/{month}/{urlTitle}",
                defaults: new { controller = "Post", action = "Single" },
                constraints: new { year = @"\d{4}", month = @"\d{2}" },
                namespaces: new[] { "Scribble.Web.Controllers" }
                );

            routes.MapRoute(
                name: "ByTag",
                url: "tags/{urlName}",
                defaults: new { controller = "Post", action = "ByTag" },
                namespaces: new[] { "Scribble.Web.Controllers" }
                );

            routes.MapRoute(
                name: "ByCategory",
                url: "categories/{urlName}",
                defaults: new { controller = "Post", action = "ByCategory" },
                namespaces: new[] { "Scribble.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Page",
                url: "{urlTitle}",
                defaults: new { controller = "Page", action = "Single" },
                namespaces: new[] { "Scribble.Web.Controllers" }
                );
        }
    }
}