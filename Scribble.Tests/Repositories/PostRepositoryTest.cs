using System;
using System.Collections.Generic;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;
using Scribble.Web.Entities;
using System.Linq;
using Scribble.Web.Repositories;

namespace Scribble.Tests.Repositories
{
    [TestFixture]
    public class PostRepositoryTest : RepositoryTestBase
    {
        [Test]
        public void ReturnsCorrectSinglePostFromRavenDb()
        {
            const string url = "a-post-url";
            var expectedPost = new Post
                {
                    Url = url,
                };

            var session = WithSessionContainingPosts(new Post { Url = "not-the-correct-post" },
                                                     expectedPost,
                                                     new Post { Url = "also-not-the-correct-post" });

            var repository = new PostRepository(session);

            var post = repository.SinglePost(url);

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

        [Test]
        public void ByTagReturnsAllPostsContainingTag()
        {
            const string expectedTitle = "Expected";
            var expectedTag = new Tag { UrlName = "Expected Tag" };
            var otherTag = new Tag { UrlName = "Other Tag" };

            var session = WithSessionContainingPosts(
                new Post { Title = expectedTitle, Tags = new List<Tag> { expectedTag, otherTag } },
                new Post { Title = expectedTitle, Tags = new List<Tag> { expectedTag } },
                new Post { Title = "Not Expected", Tags = new List<Tag> { otherTag } },
                new Post { Title = "Not Expected" });

            var repository = new PostRepository(session);

            var result = repository.ByTag(expectedTag);

            Assert.That(result.All(p => p.Title == expectedTitle));
        }

        [Test]
        public void ByCategoryReturnsAllPostsContainingCategory()
        {
            const string expectedTitle = "Expected";
            var expectedCategory = new Category { UrlName = "Expected Category" };
            var otherCategory = new Category { UrlName = "Other Category" };

            var session = WithSessionContainingPosts(
                new Post { Title = expectedTitle, Category = expectedCategory },
                new Post { Title = "Not Expected", Category = otherCategory },
                new Post { Title = "Not Expected" });

            var repository = new PostRepository(session);

            var result = repository.ByCategory(expectedCategory);

            Assert.That(result.All(p => p.Title == expectedTitle));
        }

        [Test]
        public void SavePersistsPost()
        {
            const string expectedUrl = "1984/07/some-post";
            var post = new Post { Url = expectedUrl };

            var session = WithEmptySession();
            var repository = new PostRepository(session);

            repository.Save(post);

            Assert.That(session.Query<Post>().Count(p => p.Url == expectedUrl), Is.EqualTo(1));
        }

        private static IDocumentSession WithSessionContainingPosts(params Post[] posts)
        {
            var session = WithEmptySession();

            foreach (var post in posts)
            {
                session.Store(post);
            }

            session.SaveChanges();
            return session;
        }
    }
}