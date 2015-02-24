using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

namespace Scribble.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository repository;
        private readonly IMappingEngine mapper;

        public PostController(IPostRepository repository, IMappingEngine mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ViewResult Recent()
        {
            var posts = repository.Recent();

            return View(MapEntitiesToSummaries(posts));
        }

        public ViewResult ByTag(Tag tag)
        {
            var posts = repository.ByTag(tag);

            return View(MapEntitiesToSummaries(posts));
        }

        public ViewResult ByCategory(Category category)
        {
            var posts = repository.ByCategory(category);

            return View(MapEntitiesToSummaries(posts));
        }

        public ViewResult Single(int year, int month, string urlTitle)
        {
            var url = string.Format("{0}/{1:00}/{2}", year, month, urlTitle);

            var entity = repository.SinglePost(url);

            var model = mapper.Map<Post, PostViewModel>(entity);

            return View(model);
        }

        [HttpPost, ActionName("Single")]
        public ActionResult AddComment(int year, int month, string urlTitle, PostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var url = string.Format("{0}/{1:00}/{2}", year, month, urlTitle);

            var post = repository.SinglePost(url);

            post.Comments.Add(new Comment
            {
                Text = model.Comment,
                Email = model.CommenterEmail,
                Name = model.CommenterName,
                Website = model.CommenterWebsite
            });

            repository.Save(post);

            return RedirectToAction("Single", new { year, month = month.ToString("00"), urlTitle });
        }

        private IList<PostSummaryViewModel> MapEntitiesToSummaries(IList<Post> entities)
        {
            return mapper.Map<IList<Post>, IList<PostSummaryViewModel>>(entities);
        }
    }
}