using System.Collections.Generic;

namespace Scribble.Web.Models
{
    public interface IPostRepository
    {
        Post SinglePost(string url);
        IList<Post> Recent();
        IList<Post> ByTag(Tag tag);
    }
}