using System;
using System.Collections.Generic;

namespace Scribble.Web.Models
{
    public class Post
    {
        public DateTime Date { get; set; }
        public string UrlTitle { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IList<Comment> Comments { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}