namespace Markdown.Parsing;

public abstract class Node
{
}

public class DocumentNode(List<Node> children) : Node
{
    public List<Node> Children { get; } = children;
}

public class TextNode(string text) : Node
{
    public string Text { get; } = text;
}

public class EmphasisNode(List<Node> children) : Node
{
    public List<Node> Children { get; } = children;
}

public class StrongNode(List<Node> children) : Node
{
    public List<Node> Children { get; } = children;
}

public class LinkNode(string href, List<Node> children) : Node
{
    public string Href { get; } = href;
    public List<Node> Children { get; } = children;
}

public class HeadingNode(List<Node> children) : Node
{
    public List<Node> Children { get; } = children;
}