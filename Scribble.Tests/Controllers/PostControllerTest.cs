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
            const int expectedYear = 2012;
            const int expectedMonth = 1;
            const string expectedUrlTitle = "this-is-a-test";

            var expectedPost = new Post
                {
                    Date = new DateTime(expectedYear, expectedMonth, 1),
                    UrlTitle = expectedUrlTitle
                };

            repository.Setup(r => r.SinglePost(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                      .Returns(expectedPost);

            var result = (Post)controller.Get(expectedYear, expectedMonth, expectedUrlTitle).Model;

            Assert.That(result.Date.Year, Is.EqualTo(expectedYear));
            Assert.That(result.Date.Month, Is.EqualTo(expectedMonth));
            Assert.That(result.UrlTitle, Is.EqualTo(expectedUrlTitle));
            repository.Verify(r => r.SinglePost(expectedYear, expectedMonth, expectedUrlTitle));
        }
    }
}