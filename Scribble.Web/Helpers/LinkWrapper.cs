using System;
using System.IO;

namespace Scribble.Web.Helpers
{
    public class LinkWrapper : IDisposable
    {
        private readonly TextWriter writer;
        private readonly string url;

        public LinkWrapper(TextWriter writer, string url)
        {
            this.writer = writer;
            this.url = url;
        }

        public void BeginWrap()
        {
            writer.Write("<a href=\"{0}\">", url);
        }

        public void Dispose()
        {
            writer.Write("</a>");
        }
    }
}