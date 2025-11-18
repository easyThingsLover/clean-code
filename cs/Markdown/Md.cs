using System;
using System.Text;
using Markdown.Parsing;
using Markdown.Tokenizing;

namespace Markdown;

public class Md
{
    public string Render(string input)
    {
        var tokens = Lexer.Tokenize(input);
        var document = Parser.Parse(tokens);
        return HtmlRender.Render(document);
    }
}