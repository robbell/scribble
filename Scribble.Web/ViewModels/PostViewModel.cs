using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Scribble.Web.Entities;

namespace Scribble.Web.ViewModels
{
    public class PostViewModel : BlogViewModel
    {
        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        [Required] 
        [DisplayName("Name")]
        public string CommenterName { get; set; }

        [Required]
        [DisplayName("Email")]
        public string CommenterEmail { get; set; }

        [DisplayName("Website")]
        public string CommenterWebsite { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
