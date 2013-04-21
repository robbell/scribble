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
                defaults: new { controller = "Post", action = "Recent" }
                );

            routes.MapRoute(
                name: "Post",
                url: "{year}/{month}/{urlTitle}",
                defaults: new { controller = "Post", action = "Single" },
                constraints: new { year = @"\d{4}", month = @"\d{2}" }
                );
        }
    }
}