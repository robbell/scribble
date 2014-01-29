using System.ComponentModel.DataAnnotations;

namespace Scribble.Web.Entities
{
    public class Comment
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        [Required]
        public string Body { get; set; }
    }
}