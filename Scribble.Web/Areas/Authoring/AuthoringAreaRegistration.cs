using System.Web.Mvc;

namespace Scribble.Web.Areas.Authoring
{
    public class AuthoringAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Authoring";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Authoring_default",
                "Authoring/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
