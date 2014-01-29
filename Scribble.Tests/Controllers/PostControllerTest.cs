using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Scribble.Web.Controllers;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

namespace Scribble.Tests.Controllers
{
    [TestFixture]
    public class PostControllerTest
    {
        private Mock<IPostRepository> repository;
        private PostController controller;
        private readonly List<Post> samplePosts = new List<Post> { new Post(), new Post() };

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IPostRepository>();
            controller = new PostController(repository.Object);
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

        [Test]
        public void AddCommentGetsPostFromRepositoryAndSavesComment()
        {
            var model = new PostViewModel
                {
                    UserComment = new Comment
                        {
                            Name = "Mr Man",
                            Email = "person@example.com",
                            Website = "http://rob-bell.net",
                            Body = "A comment."
                        },
                    Post = new Post { Url = "2011/02/some-post" }
                };

            var returnedPost = new Post();
            repository.Setup(r => r.SinglePost(model.Post.Url)).Returns(returnedPost);

            controller.AddComment(model);

            Assert.That(returnedPost.Comments, Contains.Item(model.UserComment));
            repository.Verify(r => r.Save(model.Post));
        }

        [Test]
        public void CreateReturnsInvalidPostWithModelErrors()
        {
            var incompleteComment = new PostViewModel
            {
                UserComment = new Comment
                {
                    Name = "Mr Man"
                },
                Post = new Post { Url = "2011/02/some-post" }
            };

            controller.ModelState.AddModelError("", "mock error message");

            var response = (ViewResult)controller.AddComment(incompleteComment);

            Assert.That(response.ViewData.ModelState.IsValid, Is.False);
        }
    }
}