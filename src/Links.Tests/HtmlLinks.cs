using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;


namespace Links.Tests
{
    public class HtmlLinks
    {
        [Fact]
        public void WhenInput_IsNotValid_ExceptionIsNotThrown()
        {
            var html = @"invalid html";

            var links = MailLinks.ExtractLinks(html);
            links.Should().HaveCount(0);
        }

        [Fact]
        public void WhenInput_DoesNoHaveLinks_LinksAreExtracted()
        {
            var html = @"<!DOCTYPE html>
<html>
<body>
	<h1>This is <b>bold</b> heading</h1>
	<p>This is <u>underlined</u> paragraph</p>
	<h2>This is <i>italic</i> heading</h2>
</body>
</html> ";

            var links = MailLinks.ExtractLinks(html);
            links.Should().HaveCount(0);
        }

        [Fact]
        public void WhenInput_HasLinks_LinksAreExtracted()
        {
            var html = @"<!DOCTYPE html>
<html>
<body>
	<a href='#'></a>
</body>
</html> ";

            var links = MailLinks.ExtractLinks(html);
            links.Should().HaveCount(1);

            links[0].Should().Be("#");
        }
    }
}
