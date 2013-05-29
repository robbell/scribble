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
        private List<Post> samplePosts = new List<Post> { new Post(), new Post() };

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
            repository.Setup(r => r.Recent())
                      .Returns(samplePosts);

            var result = controller.Recent().Model;

            Assert.That(result, Is.EqualTo(samplePosts));
            repository.Verify(r => r.Recent());
        }

        [Test]
        public void ByTagGetsPostsWithTagFromRepository()
        {
            var expectedTag = new Tag { Name = "Expected Tag" };

            repository.Setup(r => r.ByTag(expectedTag))
                      .Returns(samplePosts);

            var result = controller.ByTag(expectedTag).Model;

            Assert.That(result, Is.EqualTo(samplePosts));
            repository.Verify(r => r.ByTag(expectedTag));
        }

        [Test]
        public void ByCategoryGetsPostsInCategoryFromRepository()
        {
            var expectedCategory = new Category { Name = "Expected Category" };

            repository.Setup(r => r.ByCategory(expectedCategory))
                      .Returns(samplePosts);

            var result = controller.ByCategory(expectedCategory).Model;

            Assert.That(result, Is.EqualTo(samplePosts));
            repository.Verify(r => r.ByCategory(expectedCategory));
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