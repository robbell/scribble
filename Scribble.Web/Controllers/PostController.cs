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

        public ActionResult Single(PostUrlViewModel postUrl)
        {
            var entity = repository.SinglePost(postUrl.Url);

            if (entity == null) return HttpNotFound();

            var model = mapper.Map<Post, PostViewModel>(entity);

            return View(model);
        }

        [HttpPost, ActionName("Single")]
        public ActionResult AddComment(PostUrlViewModel postUrl, PostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var post = repository.SinglePost(postUrl.Url);

            post.Comments.Add(new Comment
            {
                Text = model.Comment,
                Email = model.CommenterEmail,
                Name = model.CommenterName,
                Website = model.CommenterWebsite
            });

            repository.Save(post);

            return RedirectToAction("Single");
        }

        private IList<PostSummaryViewModel> MapEntitiesToSummaries(IList<Post> entities)
        {
            return mapper.Map<IList<Post>, IList<PostSummaryViewModel>>(entities);
        }
    }
}