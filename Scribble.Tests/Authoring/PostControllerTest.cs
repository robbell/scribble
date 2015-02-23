using AutoMapper;
using Moq;
using NUnit.Framework;
using Scribble.Web.Areas.Authoring.Controllers;
using Scribble.Web.Areas.Authoring.ViewModels;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;
using System.Web.Mvc;

namespace Scribble.Tests.Authoring
{
    [TestFixture]
    public class PostControllerTest
    {
        private PostController controller;
        private IMappingEngine mapper;
        private IPostRepository repository;

        [SetUp]
        public void SetUp()
        {
            repository = Mock.Of<IPostRepository>();
            mapper = Mock.Of<IMappingEngine>();
            controller = new PostController(repository, mapper);
        }

        [Test]
        public void CreatePostReturnsEmptyView()
        {
            var result = controller.Create();

            Assert.That(result.Model, Is.Null);
        }

        [Test]
        public void CreateSavesValidPostToRepository()
        {
            var viewModel = new CreatePostViewModel();
            var entity = new Post();

            Mock.Get(mapper).Setup(m => m.Map<Post>(It.IsAny<CreatePostViewModel>())).Returns(entity);

            var response = (RedirectToRouteResult)controller.Create(viewModel);

            Mock.Get(mapper).Verify(m => m.Map<Post>(viewModel));
            Mock.Get(repository).Verify(r => r.Save(entity));
            Assert.That(response.RouteValues["action"], Is.EqualTo("Create"));
        }

        [Test]
        public void CreateReturnsInvalidPostWithModelErrors()
        {
            var incompletePost = new CreatePostViewModel();
            controller.ModelState.AddModelError("", "mock error message");

            var response = (ViewResult)controller.Create(incompletePost);

            Assert.That(response.ViewData.ModelState.IsValid, Is.False);
        }
    }
}
