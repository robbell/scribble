using System.Collections.Generic;

namespace Scribble.Web.Models
{
    public interface IPostRepository
    {
        IList<Post> Recent();
        Post SinglePost(int year, int month, string urlTitle);
    }
}