using NUnit.Framework;
using Scribble.Web.Entities;

namespace Scribble.Tests.Entities
{
    [TestFixture]
    public class PostTest
    {
        [Test]
        public void UrlTitleIsLastSectionOfUrl()
        {
            var post = new Post { Url = "2012/03/expected-title" };
            Assert.That(post.UrlTitle, Is.EqualTo("expected-title"));
        }

        [Test]
        public void EmptyUrlReturnsEmptyUrlTitle()
        {
            var post = new Post { Url = string.Empty };
            Assert.That(post.UrlTitle, Is.Empty);
        }
    }
}
