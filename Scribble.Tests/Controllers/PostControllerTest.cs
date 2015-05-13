using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
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
        private PostController controller;
        private IPostRepository repository;
        private IMappingEngine mapper;
        private readonly List<Post> sampleEntities = new List<Post> { new Post(), new Post() };
        private readonly List<PostSummaryViewModel> sampleSummaries = new List<PostSummaryViewModel> { new PostSummaryViewModel(), new PostSummaryViewModel() };

        [SetUp]
        public void SetUp()
        {
            repository = Mock.Of<IPostRepository>();
            mapper = Mock.Of<IMappingEngine>();
            controller = new PostController(repository, mapper);

            Mock.Get(mapper)
                .Setup(m => m.Map<IList<PostSummaryViewModel>>(sampleEntities))
                .Returns(sampleSummaries);
        }

        [Test]
        public void RecentActionGetsRecentPostsFromRepository()
        {
            Mock.Get(repository).Setup(p => p.Recent()).Returns(sampleEntities);

            var result = controller.Recent().Model;

            Mock.Get(repository).Verify(r => r.Recent());
            Assert.That(result, Is.EqualTo(sampleSummaries));
        }

        [Test]
        public void ByTagGetsPostsWithTagFromRepository()
        {
            var expectedTag = new Tag();

            Mock.Get(repository)
                .Setup(r => r.ByTag(It.IsAny<Tag>()))
                .Returns(sampleEntities);

            var result = controller.ByTag(expectedTag).Model;

            Mock.Get(repository).Verify(r => r.ByTag(expectedTag));
            Assert.That(result, Is.EqualTo(sampleSummaries));
        }

        [Test]
        public void ByCategoryGetsPostsInCategoryFromRepository()
        {
            var expectedCategory = new Category();

            Mock.Get(repository)
                .Setup(r => r.ByCategory(expectedCategory))
                .Returns(sampleEntities);

            var result = controller.ByCategory(expectedCategory).Model;

            Mock.Get(repository).Verify(r => r.ByCategory(expectedCategory));
            Assert.That(result, Is.EqualTo(sampleSummaries));
        }

        [Test]
        public void GetReturnsCorrectPostFromRepository()
        {
            var postUrl = new PostUrlViewModel { Year = 2013, Month = 4, UrlTitle = "a-test" };
            var post = new Post();
            var expectedModel = new PostViewModel();

            Mock.Get(repository)
                .Setup(r => r.SinglePost(It.IsAny<string>()))
                .Returns(post);

            Mock.Get(mapper)
                .Setup(m => m.Map<PostViewModel>(post))
                .Returns(expectedModel);

            var result = (ViewResult)controller.Single(postUrl);
            var model = (PostViewModel)result.Model;

            Mock.Get(repository).Verify(r => r.SinglePost(postUrl.Url));
            Assert.That(model, Is.EqualTo(expectedModel));
        }

        [Test]
        public void NonExistentPostUrlReturns404()
        {
            var badUrl = new PostUrlViewModel { Year = 2012, Month = 3, UrlTitle = "doesnt-exist" };

            Mock.Get(repository)
                .Setup(r => r.SinglePost(It.IsAny<string>()))
                .Returns((Post)null);

            var result = controller.Single(badUrl);

            Mock.Get(repository).Verify(r => r.SinglePost(badUrl.Url));
            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }

        [Test]
        public void AddCommentSavesCommentToPostRepositoryAndRedirects()
        {
            var postUrl = new PostUrlViewModel { Year = 2013, Month = 4, UrlTitle = "a-test" };
            var returnedPost = new Post();

            Mock.Get(repository).Setup(r => r.SinglePost(postUrl.Url)).Returns(returnedPost);

            var model = new PostViewModel
            {
                CommenterName = "George",
                CommenterEmail = "george@vandelay.com",
                CommenterWebsite = "vandelay.com",
                Comment = "A comment"
            };

            var result = controller.AddComment(postUrl, model) as RedirectToRouteResult;

            Assert.That(returnedPost.Comments,
                Has.Exactly(1).Matches<Comment>(c => c.Text == model.Comment
                                                     && c.Name == model.CommenterName
                                                     && c.Email == model.CommenterEmail
                                                     && c.Website == model.CommenterWebsite));
            Assert.NotNull(result);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Single"));
            Mock.Get(repository).Verify(r => r.Save(returnedPost));
        }

        [Test]
        public void CreateReturnsInvalidPostWithModelErrors()
        {
            var postUrl = new PostUrlViewModel { Year = 2013, Month = 4, UrlTitle = "a-test" };
            var incompleteComment = new PostViewModel();

            controller.ModelState.AddModelError("", "mock error message");

            var response = (ViewResult)controller.AddComment(postUrl, incompleteComment);

            Assert.That(response.ViewData.ModelState.IsValid, Is.False);
        }
    }
}