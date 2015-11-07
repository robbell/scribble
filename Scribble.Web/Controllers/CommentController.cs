using System.Web.Http;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

namespace Scribble.Web.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentRepository repository;

        public CommentController(ICommentRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("comments/add")]
        public IHttpActionResult AddComment(AddCommentViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            repository.Save(model);

            return Ok();
        }
    }
}