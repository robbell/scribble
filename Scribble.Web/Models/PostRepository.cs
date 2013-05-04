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

        public IList<Post> Recent()
        {
            return session.Query<Post>().OrderByDescending(p => p.Date).ToList();
        }

        public Post SinglePost(string url)
        {
            return session.Query<Post>().FirstOrDefault(p => p.Url == url);
        }
    }
}