using System;
using System.Collections.Generic;
using System.Linq;

namespace Scribble.Web.Entities
{
    public class Post
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Teaser { get; set; }
        public DateTime Date { get; set; }
        public IList<Comment> Comments { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Category Category { get; set; }

        public string UrlTitle
        {
            get { return Url != null ? Url.Split('/').Last() : string.Empty; }
        }

        public Post()
        {
            Comments = new List<Comment>();
            Tags = new List<Tag>();
        }
    }
}