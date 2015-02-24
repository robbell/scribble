using System;
using System.ComponentModel.DataAnnotations;

namespace Scribble.Web.Areas.Authoring.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string UrlTitle { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }
    }
}