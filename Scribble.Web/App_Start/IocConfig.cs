using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Scribble.Web
{
    public class IocConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}