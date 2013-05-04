using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Scribble.Web.Controllers;
using Scribble.Web.Models;

namespace Scribble.Tests.Controllers
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

        [Test]
        public void GetReturnsCorrectPostFromRepository()
        {
            const int year = 2013;
            const int month = 4;
            const string title = "this-is-a-test";
            const string expectedUrl = "2013/04/this-is-a-test";

            var expectedPost = new Post
                {
                    Url = expectedUrl
                };

            repository.Setup(r => r.SinglePost(expectedUrl))
                      .Returns(expectedPost);

            var result = (Post)controller.Single(year, month, title).Model;

            repository.Verify(r => r.SinglePost(expectedUrl));
            Assert.That(result, Is.EqualTo(expectedPost));
        }
    }
}