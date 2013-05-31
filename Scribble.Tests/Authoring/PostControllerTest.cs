using NUnit.Framework;
using Scribble.Web.Areas.Authoring.Controllers;

namespace Scribble.Tests.Authoring
{
    [TestFixture]
    public class PostControllerTest
    {
        [Test]
        public void CreatePostReturnsEmptyView()
        {
            var controller = new PostController();

            var result = controller.Create();

            Assert.That(result.Model, Is.Null);
        }
    }
}
