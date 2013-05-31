using System.Web.Mvc;

namespace Scribble.Web.Areas.Authoring.Controllers
{
    public class PostController : Controller
    {
        public ViewResult Create()
        {
            return View();
        }
    }
}
