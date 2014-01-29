using System.Web.Mvc;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

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

            return View(new PostViewModel { Post = repository.SinglePost(url) });
        }

        public ViewResult ByTag(Tag tag)
        {
            return View(repository.ByTag(tag));
        }

        public ViewResult ByCategory(Category category)
        {
            return View(repository.ByCategory(category));
        }

        [HttpPost, ActionName("Single")]
        public ActionResult AddComment(PostViewModel model)
        {
            model.Post = repository.SinglePost(model.Post.Url);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Post.Comments.Add(model.UserComment);
            repository.Save(model.Post);

            return RedirectToAction("Single",
                                    new
                                        {
                                            model.Post.Date.Year,
                                            month = model.Post.Date.Month.ToString("00"),
                                            model.Post.UrlTitle
                                        });
        }
    }
}