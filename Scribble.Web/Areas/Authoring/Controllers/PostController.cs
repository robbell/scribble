using AutoMapper;
using Scribble.Web.Areas.Authoring.ViewModels;
using Scribble.Web.Entities;
using Scribble.Web.Repositories;
using System.Web.Mvc;

namespace Scribble.Web.Areas.Authoring.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository repository;
        private readonly IMappingEngine mapper;

        public PostController(IPostRepository repository, IMappingEngine mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = mapper.Map<Post>(model);

            repository.Save(entity);

            return RedirectToAction("Create");
        }
    }
}
