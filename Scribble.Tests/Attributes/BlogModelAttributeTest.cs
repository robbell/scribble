using Moq;
using NUnit.Framework;
using Scribble.Web.Attributes;
using Scribble.Web.Domain;
using Scribble.Web.ViewModels;
using System.Web.Mvc;

namespace Scribble.Tests.Attributes
{
    [TestFixture]
    public class BlogModelAttributeTest
    {
        [Test]
        public void AttributeAddsBlogInfoToBlogViewModel()
        {
            var model = new DummyBlogModel();
            var context = CreateContext(model);
            var expectedInfo = new BlogInfo();
            var provider = Mock.Of<IBlogInfoProvider>(f => f.GetBlogInfo() == expectedInfo);

            var attribute = new BlogModelAttribute(provider);

            attribute.OnResultExecuting(context);

            Assert.That(model.BlogInfo, Is.EqualTo(expectedInfo));
        }

        [Test]
        public void AttributeBypassesNonBlogViewModels()
        {
            var simpleModel = new SimpleModel();
            var context = CreateContext(simpleModel);

            var attribute = new BlogModelAttribute(null);

            attribute.OnResultExecuting(context);

            Assert.That(simpleModel, Is.Not.TypeOf<BlogViewModel>());
        }

        private static ResultExecutingContext CreateContext(object model)
        {
            var viewData = new ViewDataDictionary(model);

            return Mock.Of<ResultExecutingContext>(c => c.Controller.ViewData == viewData);
        }
    }

    public class DummyBlogModel : BlogViewModel
    {
    }

    public class SimpleModel
    {
    }
}
