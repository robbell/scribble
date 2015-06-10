using System.Web.Mvc;
using AutoMapper;
using Scribble.Web.Repositories;
using Scribble.Web.ViewModels;

namespace Scribble.Web.Controllers
{
    [OutputCache(CacheProfile = "Standard")]
    public class PageController : Controller
    {
        private readonly IPageRepository repository;
        private readonly IMappingEngine mapper;

        public PageController(IPageRepository repository, IMappingEngine mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ActionResult Single(PageUrlViewModel urlModel)
        {
            var page = repository.SinglePage(urlModel.UrlTitle);

            if (page == null) return HttpNotFound();

            var model = mapper.Map<PageViewModel>(page);

            return View(model);
        }
    }
}