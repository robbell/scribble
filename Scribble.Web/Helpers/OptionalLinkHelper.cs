using System;
using System.Web.Mvc;

namespace Scribble.Web.Helpers
{
    public static class OptionalLinkHelper
    {
        public static IDisposable WrapWithLink(this HtmlHelper helper, string url)
        {
            if (String.IsNullOrEmpty(url)) return null;

            var wrapper = new LinkWrapper(helper.ViewContext.Writer, url);

            wrapper.BeginWrap();

            return wrapper;
        }
    }
}