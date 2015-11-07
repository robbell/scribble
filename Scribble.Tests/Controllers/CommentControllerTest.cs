using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Scribble.Web.Controllers;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

namespace Scribble.Tests.Controllers
{
    [TestFixture]
    public class CommentControllerTest
    {
        private ICommentRepository repository;
        private CommentController controller;
        private AddCommentViewModel model;

        [SetUp]
        public void Setup()
        {
            repository = Mock.Of<ICommentRepository>();
            controller = new CommentController(repository);
            model = new AddCommentViewModel();
        }

        [Test]
        public void CommentIsAddedToRepository()
        {
            var result = controller.AddComment(model);

            Mock.Get(repository).Verify(r => r.Save(model));

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void InvalidCommentReturnsError()
        {
            controller.ModelState.AddModelError(string.Empty, "Some error!");

            var result = controller.AddComment(model) as InvalidModelStateResult;

            Mock.Get(repository).Verify(r => r.Save(It.IsAny<AddCommentViewModel>()), Times.Never);

            Assert.IsNotNull(result);
        }
    }
}
