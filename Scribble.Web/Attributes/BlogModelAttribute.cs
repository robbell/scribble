using System.Web.Mvc;
using Scribble.Web.Domain;
using Scribble.Web.ViewModels;

namespace Scribble.Web.Attributes
{
    public class BlogModelAttribute : ActionFilterAttribute
    {
        private readonly IBlogInfoProvider provider;

        public BlogModelAttribute(IBlogInfoProvider provider)
        {
            this.provider = provider;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model as BlogViewModel;

            if (model != null)
            {
                model.BlogInfo = provider.GetBlogInfo();
            }

            base.OnResultExecuting(filterContext);
        }
    }
}