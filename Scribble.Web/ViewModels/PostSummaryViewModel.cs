using System;

namespace Scribble.Web.ViewModels
{
    public class PostSummaryViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string UrlTitle { get; set; }
        public string Body { get; set; }
        public string Teaser { get; set; }
    }
}