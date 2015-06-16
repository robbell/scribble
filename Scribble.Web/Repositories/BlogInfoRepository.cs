using Raven.Client;
using Scribble.Web.Domain;

namespace Scribble.Web.Repositories
{
    public class BlogInfoRepository : IBlogInfoProvider
    {
        private readonly IDocumentSession session;

        public BlogInfoRepository(IDocumentSession session)
        {
            this.session = session;
        }

        public BlogInfo GetBlogInfo()
        {
            return session.Load<BlogInfo>(1);
        }
    }
}