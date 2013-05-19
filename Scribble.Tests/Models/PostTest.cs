using NUnit.Framework;
using Scribble.Web.Models;

namespace Scribble.Tests.Models
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
    }
}
