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
    public class PageControllerTest
    {
        private PageController controller;
        private IPageRepository repository;
        private IMappingEngine mapper;

        [SetUp]
        public void SetUp()
        {
            repository = Mock.Of<IPageRepository>();
            mapper = Mock.Of<IMappingEngine>();
            controller = new PageController(repository, mapper);
        }

        [Test]
        public void GetReturnsCorrectPageFromRepository()
        {
            var urlModel = new PageUrlViewModel();
            var page = new Page();
            var expectedModel = new PageViewModel();

            var repository = Mock.Of<IPageRepository>();
            var mapper = Mock.Of<IMappingEngine>();
            var controller = new PageController(repository, mapper);

            Mock.Get(repository).Setup(r => r.SinglePage(It.IsAny<string>())).Returns(page);
            Mock.Get(mapper).Setup(m => m.Map<PageViewModel>(page)).Returns(expectedModel);

            var result = controller.Single(urlModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.That(result.Model, Is.EqualTo(expectedModel));
            Mock.Get(repository).Verify(r => r.SinglePage(urlModel.Url));
        }

        [Test]
        public void NonExistentPageUrlReturns404()
        {
            var badUrl = new PageUrlViewModel { Url = "doesnt-exist" };

            Mock.Get(repository)
                .Setup(r => r.SinglePage(It.IsAny<string>()))
                .Returns((Page)null);

            var result = controller.Single(badUrl);

            Mock.Get(repository).Verify(r => r.SinglePage(badUrl.Url));
            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }
    }
}