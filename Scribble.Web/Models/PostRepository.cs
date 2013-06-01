using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;

namespace Scribble.Web.Models
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
            return session.Query<Post>().OrderByDescending(p => p.Date).ToList();
        }

        public IList<Post> ByTag(Tag tag)
        {
            return session.Query<Post>().Where(p => p.Tags.Any(t => t.UrlName == tag.UrlName)).ToList();
        }

        public IList<Post> ByCategory(Category category)
        {
            return session.Query<Post>().Where(p => p.Category.UrlName == category.UrlName).ToList();
        }

        public void Save(Post post)
        {
            session.Store(post);
            session.SaveChanges();
        }
    }
}