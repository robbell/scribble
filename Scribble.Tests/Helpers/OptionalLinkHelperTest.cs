using System;
using NUnit.Framework;
using Scribble.Web.Helpers;

namespace Scribble.Tests.Helpers
{
    [TestFixture]
    public class OptionalLinkHelperTest
    {
        [Test, Ignore]
        public void WrapReturnsWrapperInstanceWhenUrlProvided()
        {
            Assert.That(OptionalLinkHelper.WrapWithLink(null, "http://someurl.com"), Is.InstanceOf<LinkWrapper>());
        }

        [Test]
        public void WrapReturnsNullWhenNoUrlProvided()
        {
            Assert.That(OptionalLinkHelper.WrapWithLink(null, null), Is.EqualTo(null));
            Assert.That(OptionalLinkHelper.WrapWithLink(null, String.Empty), Is.EqualTo(null));
        }
    }
}