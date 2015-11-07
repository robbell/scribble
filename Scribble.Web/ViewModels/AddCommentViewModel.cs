using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Scribble.Web.ViewModels
{
    public class AddCommentViewModel
    {
        public string PostUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Website { get; set; }

        [Required]
        [DisplayName("Comment")]
        public string Text { get; set; }
    }
}