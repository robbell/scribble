using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Embedded;
using Scribble.Web.Models;

namespace Scribble.Web
{
    public class IocConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ConcreteRepo>().As<IPostRepository>();

            RegisterRavenDb(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterRavenDb(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var documentStore = new EmbeddableDocumentStore();
                documentStore.DataDirectory = "Data";
                documentStore.Initialize();

                return documentStore;
            })
            .As<IDocumentStore>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IDocumentStore>().OpenSession())
                .As<IDocumentSession>()
                .InstancePerHttpRequest();
        }
    }
}