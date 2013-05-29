using System.Web.Mvc;
using Scribble.Web.Models;

namespace Scribble.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository repository;

        public PostController(IPostRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Recent()
        {
            return View(repository.Recent());
        }

        public ViewResult Single(int year, int month, string urlTitle)
        {
            var url = string.Format("{0}/{1:00}/{2}", year, month, urlTitle);

            return View(repository.SinglePost(url));
        }

        public ViewResult ByTag(Tag tag)
        {
            return View(repository.ByTag(tag));
        }

        public ViewResult ByCategory(Category category)
        {
            return View(repository.ByCategory(category));
        }
    }
}