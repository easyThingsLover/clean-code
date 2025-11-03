namespace Markdown;

public class Node
{
    public NodeType Type { get; protected set; }
    public List<Node> Children { get; } = new();
    public string Value { get;}

    public Node(NodeType type, string value)
    {
        Type = type;
        Value = value;
    }

    public void AddChild(Node child) => Children.Add(child);
}