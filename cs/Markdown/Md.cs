namespace Markdown;

public class Md
{
    private readonly Lexer lexer;
    private readonly Parser parser;
    private readonly HtmlRender htmlRender;

    public Md(Lexer lexer, Parser parser, HtmlRender htmlRender)
    {
        this.lexer = lexer;
        this.parser = parser;
        this.htmlRender = htmlRender;
    }

    public string Render(string markdown)
    {
        throw new NotImplementedException();
    }
}