using System.Web.Mvc;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Scribble.Web.Areas.Authoring.Controllers;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

namespace Scribble.Tests.Areas.Authoring
{
    [TestFixture]
    public class PostControllerTest
    {
        private PostController controller;
        private IPostRepository repository;
        private IMappingEngine mapper;

        [SetUp]
        public void SetUp()
        {
            repository = Mock.Of<IPostRepository>();
            mapper = Mock.Of<IMappingEngine>();
            controller = new PostController(repository, mapper);
        }

        [Test]
        public void CreatePostInitialLoadReturnsEmptyView()
        {
            var result = controller.Create();

            Assert.That(result.Model, Is.Null);
        }

        [Test]
        public void CreateSavesValidPostToRepository()
        {
            // Arrange
            var model = new CreatePostViewModel();
            var entity = new Post();

            Mock.Get(mapper)
                .Setup(m => m.Map<CreatePostViewModel, Post>(It.IsAny<CreatePostViewModel>()))
                .Returns(entity);

            // Act
            var response = (RedirectToRouteResult)controller.Create(model);

            // Assert
            Mock.Get(repository).Verify(r => r.Save(entity));
            Assert.That(response.RouteValues["action"], Is.EqualTo("Create"));
        }

        [Test]
        public void CreateReturnsInvalidPostWithModelErrors()
        {
            var incompleteModel = new CreatePostViewModel();

            controller.ModelState.AddModelError("", "mock error message");

            var response = (ViewResult)controller.Create(incompleteModel);

            Assert.That(response.ViewData.ModelState.IsValid, Is.False);
        }
    }
}
