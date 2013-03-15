using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

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
            throw new System.NotImplementedException();
        }

        public Post SinglePost(string urlTitle, DateTime date)
        {
            return session.Query<Post>().FirstOrDefault(p => p.UrlTitle == urlTitle && p.Date == date);
        }
    }
}