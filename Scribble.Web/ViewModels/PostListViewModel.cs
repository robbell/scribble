using System.Collections.Generic;

namespace Scribble.Web.ViewModels
{
    public class PostListViewModel : BlogViewModel
    {
        public string Title { get; set; }
        public IList<PostSummaryViewModel> Posts { get; set; }
    }
}