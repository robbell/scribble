using Scribble.Web.Entities;

namespace Scribble.Web.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public Comment UserComment { get; set; }
    }
}
