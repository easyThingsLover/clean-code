using System.Net;
using System.Text;
using Markdown.Parsing;

namespace Markdown;

public class HtmlRender
{
    public static string Render(DocumentNode document)
    {
        var sb = new StringBuilder();
        RenderNodes(document.Children, sb);
        return sb.ToString();
    }

    private static void RenderNodes(IEnumerable<Node> nodes, StringBuilder sb)
    {
        foreach (var node in nodes)
            RenderNode(node, sb);
    }

    private static void RenderNode(Node node, StringBuilder sb)
    {
        switch (node)
        {
            case TextNode text:
            {
                sb.Append(text.Text);
                break;
            }
            case EmphasisNode em:
            {
                sb.Append("<em>");
                RenderNodes(em.Children, sb);
                sb.Append("</em>");
                break;
            }
            case StrongNode strong:
            {
                sb.Append("<strong>");
                RenderNodes(strong.Children, sb);
                sb.Append("</strong>");
                break;
            }
            case LinkNode link:
            {
                sb.Append("<a href=\"");
                sb.Append(WebUtility.HtmlEncode(link.Href));
                sb.Append("\">");
                RenderNodes(link.Children, sb);
                sb.Append("</a>");
                break;
            }
            case HeadingNode heading:
            {
                sb.Append("<h1");
                sb.Append(">");

                RenderNodes(heading.Children, sb);

                sb.Append("</h1");
                sb.Append(">");
                break;
            }
            case DocumentNode doc:
            {
                RenderNodes(doc.Children, sb);
                break;
            }
        }
    }
}