    using Scribble.Web.Entities;

namespace Scribble.Web.Repositories
{
    public interface IPageRepository
    {
        Page SinglePage(string url);
    }
}