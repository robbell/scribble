using System.IO;
using NUnit.Framework;
using Scribble.Web.Helpers;

namespace Scribble.Tests.Helpers
{
    [TestFixture]
    public class LinkWrapperTest
    {
        [Test]
        public void ContentIsWrappedWithLinkOnceDisposed()
        {
            const string url = "http://someurl.com";
            const string content = "Text to wrap";

            var writer = new StringWriter();

            using (var wrapper = new LinkWrapper(writer, url))
            {
                wrapper.BeginWrap();
                writer.Write(content);
            }

            Assert.That(writer.ToString(), Is.EqualTo(string.Format("<a href=\"{0}\">{1}</a>", url, content)));
        }
    }
}