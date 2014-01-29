using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Embedded;
using Scribble.Web.Repositories;

namespace Scribble.Web
{
    public class IocConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<PostRepository>().As<IPostRepository>();

            RegisterRavenDb(builder);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static void RegisterRavenDb(ContainerBuilder builder)
        {
            var documentStore = new EmbeddableDocumentStore
            {
                UseEmbeddedHttpServer = true,
                DataDirectory = "ScribbleData"
            };

            documentStore.Configuration.Port = 12013;

            builder.Register(c =>
                             documentStore.Initialize())
                   .As<IDocumentStore>()
                   .SingleInstance();

            builder.Register(c => c.Resolve<IDocumentStore>().OpenSession())
                   .As<IDocumentSession>()
                   .InstancePerHttpRequest();
        }
    }
}