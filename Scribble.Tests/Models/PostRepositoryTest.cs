using System;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;
using Scribble.Web.Models;

namespace Scribble.Tests.Models
{
    [TestFixture]
    public class PostRepositoryTest
    {
        [Test]
        public void ReturnsCorrectSinglePostFromRavenDb()
        {
            const string expectedUrlTitle = "a-post-title";
            var expectedDate = new DateTime(2001, 02, 01);
            var expectedPost = new Post
                {
                    UrlTitle = expectedUrlTitle,
                    Date = expectedDate
                };

            var repository = new PostRepository(WithDummySessionContainingPost(expectedPost));

            var post = repository.SinglePost(expectedUrlTitle, expectedDate);

            Assert.That(post, Is.EqualTo(expectedPost));
        }

        private static IDocumentSession WithDummySessionContainingPost(Post expectedPost)
        {
            var store = new EmbeddableDocumentStore { RunInMemory = true }.Initialize();
            var session = store.OpenSession();

            session.Store(new Post { UrlTitle = "not-the-correct-post", Date = DateTime.Now });
            session.Store(expectedPost);
            session.Store(new Post { UrlTitle = "also-not-the-correct-post", Date = DateTime.Now });

            session.SaveChanges();
            return session;
        }
    }
}