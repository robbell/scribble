using NUnit.Framework;
using Scribble.Web.Domain;
using Scribble.Web.Repositories;

namespace Scribble.Tests.Repositories
{
    [TestFixture]
    public class BlogInfoRepositoryTest : RepositoryTestBase
    {
        [Test]
        public void ReturnsCorrectSinglePostFromRavenDb()
        {
            var expectedInfo = new BlogInfo();

            var session = WithEmptySession();
            session.Store(expectedInfo);
            session.SaveChanges();

            var repository = new BlogInfoRepository(session);

            var info = repository.GetBlogInfo();

            Assert.That(info, Is.EqualTo(expectedInfo));
        }
    }
}
