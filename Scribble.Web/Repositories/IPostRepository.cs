using System.Collections.Generic;
using Scribble.Web.Entities;

namespace Scribble.Web.Repositories
{
    public interface IPostRepository
    {
        Post SinglePost(string url);
        IList<Post> Recent();
        IList<Post> ByTag(Tag tag);
        IList<Post> ByCategory(Category category);
        void Save(Post post);
    }
}