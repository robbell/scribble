using Raven.Client;
using Scribble.Web.ViewModels;

namespace Scribble.Web.Repositories
{
    public interface ICommentRepository
    {
        void Save(AddCommentViewModel model);
    }

    public class CommentRepository : ICommentRepository
    {
        private readonly IDocumentSession session;

        public CommentRepository(IDocumentSession session)
        {
            this.session = session;
        }

        public void Save(AddCommentViewModel model)
        {
            session.Store(model);
            session.SaveChanges();
        }
    }
}