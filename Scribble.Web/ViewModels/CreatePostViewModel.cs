using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Scribble.Web.Entities;

namespace Scribble.Web.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public string UrlName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Title { get; set; }

        public string Body { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}