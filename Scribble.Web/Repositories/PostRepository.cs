using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Scribble.Web.Entities;

namespace Scribble.Web.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDocumentSession session;

        public PostRepository(IDocumentSession session)
        {
            this.session = session;
        }

        public Post SinglePost(string url)
        {
            return session.Query<Post>().FirstOrDefault(p => p.Url == url);
        }

        public IList<Post> Recent()
        {
            return OrderByDateDescending(session.Query<Post>()).ToList();
        }

        public IList<Post> ByTag(Tag tag)
        {
            return OrderByDateDescending(session.Query<Post>().Where(p => p.Tags.Any(t => t.UrlName == tag.UrlName))).ToList();
        }

        public IList<Post> ByCategory(Category category)
        {
            return OrderByDateDescending(session.Query<Post>().Where(p => p.Category.UrlName == category.UrlName)).ToList();
        }

        private IEnumerable<Post> OrderByDateDescending(IRavenQueryable<Post> query)
        {
            return query.OrderByDescending(p => p.Date);
        }

        public void Save(Post post)
        {
            session.Store(post);
            session.SaveChanges();
        }
    }
}