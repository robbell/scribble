using System.Linq;
using Raven.Client;
using Scribble.Web.Entities;

namespace Scribble.Web.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly IDocumentSession session;

        public PageRepository(IDocumentSession session)
        {
            this.session = session;
        }

        public Page SinglePage(string url)
        {
            return session.Query<Page>().FirstOrDefault(p => p.Url == url);
        }
    }
}
