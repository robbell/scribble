using System.Collections.Generic;
using Scribble.Web.ViewModels;

namespace Scribble.Web.Controllers : BlogViewModel
{
    public class PostListViewModel
    {
        public string Title { get; set; }
        public IList<PostSummaryViewModel> Posts { get; set; }
    }
}