using System.Web.Mvc;
using Scribble.Web.Attributes;
using Scribble.Web.Domain;

namespace Scribble.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BlogModelAttribute(DependencyResolver.Current.GetService<IBlogInfoProvider>()));
        }
    }
}