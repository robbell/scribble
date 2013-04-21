using System;
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
            const string expectedUrlTitle = "this-is-a-test";
            var expectedDate = new DateTime(2012, 3, 1);
            var expectedPost = new Post
                {
                    Date = expectedDate,
                    UrlTitle = expectedUrlTitle
                };

            repository.Setup(r => r.SinglePost(It.IsAny<string>(), It.IsAny<DateTime>()))
                      .Returns(expectedPost);

            var result = (Post)controller.Single(expectedDate.Year, expectedDate.Month, expectedUrlTitle).Model;

            Assert.That(result.Date, Is.EqualTo(expectedDate));
            Assert.That(result.UrlTitle, Is.EqualTo(expectedUrlTitle));
            repository.Verify(r => r.SinglePost(expectedUrlTitle, expectedDate));
        }
    }
}