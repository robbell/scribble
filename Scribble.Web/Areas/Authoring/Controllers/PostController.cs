using System.Web.Mvc;
using Scribble.Web.Models;

namespace Scribble.Web.Areas.Authoring.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository repository;

        public PostController(IPostRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid) return View(post);

            repository.Save(post);

            return RedirectToAction("Create");
        }
    }
}
