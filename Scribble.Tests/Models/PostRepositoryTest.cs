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

            var session = WithSessionContainingPosts(new Post { UrlTitle = "not-the-correct-post" },
                                                     expectedPost,
                                                     new Post { UrlTitle = "also-not-the-correct-post" });

            var repository = new PostRepository(session);

            var post = repository.SinglePost(expectedUrlTitle, expectedDate);

            Assert.That(post, Is.EqualTo(expectedPost));
        }

        [Test]
        public void RecentReturnsAllPostsOrderedByDateDescending()
        {
            var newestPost = new Post { Date = new DateTime(2012, 06, 01) };
            var secondNewestPost = new Post { Date = new DateTime(2012, 03, 01) };
            var thirdNewestPost = new Post { Date = new DateTime(2011, 06, 01) };

            var repository = new PostRepository(WithSessionContainingPosts(secondNewestPost, thirdNewestPost, newestPost));

            var recentPosts = repository.Recent();

            Assert.That(recentPosts.Count, Is.EqualTo(3));
            Assert.That(recentPosts[0], Is.EqualTo(newestPost));
            Assert.That(recentPosts[1], Is.EqualTo(secondNewestPost));
            Assert.That(recentPosts[2], Is.EqualTo(thirdNewestPost));
        }

        private static IDocumentSession WithSessionContainingPosts(params Post[] posts)
        {
            var store = new EmbeddableDocumentStore { RunInMemory = true }.Initialize();
            var session = store.OpenSession();

            foreach (var post in posts)
            {
                session.Store(post);
            }

            session.SaveChanges();
            return session;
        }
    }
}