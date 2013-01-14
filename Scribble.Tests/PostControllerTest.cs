using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Scribble.Web.Controllers;
using Scribble.Web.Models;

namespace Scribble.Tests
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
        public void RecentIsDefaultAction()
        {
            var result = controller.Recent();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void RecentActionGetsRecentPostsFromRepository()
        {
            var expectedPosts = new List<Post> { new Post(), new Post() };

            repository.Setup(r => r.Recent())
                      .Returns(expectedPosts);

            var result = controller.Recent().Model;

            Assert.That(result, Is.EqualTo(expectedPosts));
            repository.Verify(r => r.Recent());
        }
    }
}
