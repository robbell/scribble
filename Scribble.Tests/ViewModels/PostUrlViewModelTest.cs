using NUnit.Framework;
using Scribble.Web.ViewModels;

namespace Scribble.Tests.ViewModels
{
    [TestFixture]
    public class PostUrlViewModelTest
    {
        [TestCase(2010, 1, "a-test", Result = "2010/01/a-test")]
        [TestCase(1995, 12, "another-test", Result = "1995/12/another-test")]
        public string UrlReturnsCombinedPathParts(int year, int month, string urlTitle)
        {
            return new PostUrlViewModel { Year = year, Month = month, UrlTitle = urlTitle }.Url;
        }
    }
}
