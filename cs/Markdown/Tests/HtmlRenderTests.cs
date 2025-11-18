using FluentAssertions;
using Markdown.Parsing;
using NUnit.Framework;

namespace Markdown.Tests;

[TestFixture]
public class HtmlRenderTests
{
    [Test]
    public void TextNode_RenderVerbatim()
    {
        var document = new DocumentNode([new TextNode("plain")]);
        HtmlRender.Render(document).Should().Be("plain");
    }

    [Test]
    public void EmphasisNode_WrapWithEmTags()
    {
        var document = new DocumentNode([new EmphasisNode([new TextNode("italic")])]);
        HtmlRender.Render(document).Should().Be("<em>italic</em>");
    }

    [Test]
    public void StrongNode_WrapWithStrongTags()
    {
        var document = new DocumentNode([new StrongNode([new TextNode("bold")])]);
        HtmlRender.Render(document).Should().Be("<strong>bold</strong>");
    }

    [Test]
    public void LinkNode_RenderAnchor()
    {
        var document = new DocumentNode([new LinkNode("https://example.com", [new TextNode("Example")])]);
        HtmlRender.Render(document).Should().Be("<a href=\"https://example.com\">Example</a>");
    }

    [Test]
    public void HeadingNode_ClampLevelToRange()
    {
        var document = new DocumentNode([
            new HeadingNode([new TextNode("zero")]),
            new HeadingNode([new TextNode("seven")])
        ]);

        HtmlRender.Render(document).Should().Be("<h1>zero</h1><h1>seven</h1>");
    }

    [Test]
    public void DocumentNode_RenderChildrenSequentially()
    {
        var document = new DocumentNode([
            new TextNode("Hello "),
            new StrongNode([new TextNode("world")]),
            new TextNode("!")
        ]);

        HtmlRender.Render(document).Should().Be("Hello <strong>world</strong>!");
    }
}

