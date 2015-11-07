using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Raven.Client;
using Raven.Client.Document;
using Scribble.Web.Areas.Authoring.ViewModels;
using Scribble.Web.Domain;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

namespace Scribble.Web
{
    public class IocConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof (MvcApplication).Assembly);

            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<PageRepository>().As<IPageRepository>();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();
            builder.RegisterType<BlogInfoRepository>().As<IBlogInfoProvider>();

            RegisterRavenDb(builder);
            RegisterMapper(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterMapper(ContainerBuilder builder)
        {
            Mapper.CreateMap<CreatePostViewModel, Post>();
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<Post, PostSummaryViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();

            builder.Register(context => Mapper.Engine).As<IMappingEngine>();
        }

        private static void RegisterRavenDb(ContainerBuilder builder)
        {
            var documentStore = new DocumentStore
            {
                ConnectionStringName = "ScribbleData"
            };

            builder.Register(c =>
                documentStore.Initialize())
                .As<IDocumentStore>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IDocumentStore>().OpenSession())
                .As<IDocumentSession>()
                .InstancePerRequest();
        }
    }
}