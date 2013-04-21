using System;
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
            return View(repository.SinglePost(urlTitle, new DateTime(year, month, 1)));
        }
    }
}