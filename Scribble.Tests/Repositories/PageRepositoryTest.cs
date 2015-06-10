using NUnit.Framework;
using Raven.Client;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;

namespace Scribble.Tests.Repositories
{
    [TestFixture]
    public class PageRepositoryTest : RepositoryTestBase
    {
        [Test]
        public void ReturnsCorrectSinglePageFromRavenDb()
        {
            const string url = "a-page-url";

            var expectedPage = new Page { Url = url };

            var session = WithSessionContainingPosts(new Page { Url = "not-the-correct-page" },
                                                     expectedPage,
                                                     new Page { Url = "also-not-the-correct-page" });

            var repository = new PageRepository(session);

            var post = repository.SinglePage(url);

            Assert.That(post, Is.EqualTo(expectedPage));
        }

        private static IDocumentSession WithSessionContainingPosts(params Page[] pages)
        {
            var session = WithEmptySession();

            foreach (var page in pages)
            {
                session.Store(page);
            }

            session.SaveChanges();

            return session;
        }
    }
}
