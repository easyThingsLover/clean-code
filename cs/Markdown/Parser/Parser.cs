namespace Markdown.Parsing;

public class Parser
{
    private readonly TokenIndexer indexer;

    private Parser(List<Token> tokens)
    {
        indexer = new TokenIndexer(tokens);
    }

    public static DocumentNode Parse(List<Token> tokens)
    {
        var parser = new Parser(tokens);
        var nodes = parser.ParseDocument();
        return new DocumentNode(nodes);
    }

    private List<Node> ParseDocument()
    {
        var nodes = new List<Node>();

        while (!IsEndOfFile())
        {
            if (HeadingParser.IsStartOfHeadingLine(indexer))
            {
                var heading = HeadingParser.ParseHeading(indexer);
                nodes.Add(heading);
                continue;
            }

            var lineNodes = InlineParser.ParseUntilEndOfLineOrEof(indexer);
            nodes.AddRange(lineNodes);

            if (Is(TokenType.EndOfLine))
                Consume();
        }

        return nodes;
    }

    private bool Is(TokenType type) => indexer.Is(type);

    private void Consume() => indexer.Consume();

    private bool IsEndOfFile() => indexer.End;
}
