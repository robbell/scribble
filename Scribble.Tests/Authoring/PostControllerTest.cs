using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Scribble.Web.Areas.Authoring.Controllers;
using Scribble.Web.Models;

namespace Scribble.Tests.Authoring
{
    [TestFixture]
    public class PostControllerTest
    {
        private Mock<IPostRepository> repository;
        private PostController controller;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IPostRepository>();
            controller = new PostController(repository.Object);
        }

        [Test]
        public void CreatePostReturnsEmptyView()
        {
            var result = controller.Create();

            Assert.That(result.Model, Is.Null);
        }

        [Test]
        public void CreatePostSavesValidPostToRepository()
        {
            var post = new Post { Body = "Body Text", Title = "A Title" };

            var response = controller.Create(post) as RedirectToRouteResult;

            repository.Verify(r => r.Save(post));
            Assert.That(response.RouteValues["action"], Is.EqualTo("Create"));
        }
    }
}
