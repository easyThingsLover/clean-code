using System.Text;

namespace Markdown.Tokenizing;

public class Lexer
{
    public static List<Token> Tokenize(string text)
    {
        var cursor = new CharIndexer(text);
        var tokens = new List<Token>();

        while (!cursor.End)
        {
            var c = cursor.Current;

            switch (c)
            {
                case '\n':
                    tokens.Add(new Token(TokenType.EndOfLine, "\n"));
                    cursor.MoveNext();
                    break;

                case ' ':
                case '\t':
                    tokens.Add(new Token(TokenType.Whitespace, c.ToString()));
                    cursor.MoveNext();
                    break;

                case '\\':
                    tokens.Add(new Token(TokenType.Escape, "\\"));
                    cursor.MoveNext();
                    break;

                case '_':
                    if (cursor.MatchNext('_'))
                    {
                        tokens.Add(new Token(TokenType.DoubleUnderscore, "__"));
                        cursor.MoveNext();
                        cursor.MoveNext();
                    }
                    else
                    {
                        tokens.Add(new Token(TokenType.Underscore, "_"));
                        cursor.MoveNext();
                    }

                    break;

                case '#':
                    tokens.Add(new Token(TokenType.Hash, "#"));
                    cursor.MoveNext();
                    break;

                case '[':
                    tokens.Add(new Token(TokenType.OpenBracket, "["));
                    cursor.MoveNext();
                    break;

                case ']':
                    tokens.Add(new Token(TokenType.CloseBracket, "]"));
                    cursor.MoveNext();
                    break;

                case '(':
                    tokens.Add(new Token(TokenType.OpenParen, "("));
                    cursor.MoveNext();
                    break;

                case ')':
                    tokens.Add(new Token(TokenType.CloseParen, ")"));
                    cursor.MoveNext();
                    break;

                case '<':
                    tokens.Add(new Token(TokenType.AutoLinkOpen, "<"));
                    cursor.MoveNext();
                    break;

                case '>':
                    tokens.Add(new Token(TokenType.AutoLinkClose, ">"));
                    cursor.MoveNext();
                    break;

                default:
                    ReadText(tokens, cursor);
                    break;
            }
        }

        tokens.Add(new Token(TokenType.EndOfFile, ""));
        return tokens;
    }

    private static void ReadText(List<Token> tokens, CharIndexer indexer)
    {
        var sb = new StringBuilder();

        while (!indexer.End && IsTextChar(indexer.Current))
        {
            sb.Append(indexer.Current);
            indexer.MoveNext();
        }

        if (sb.Length > 0)
            tokens.Add(new Token(TokenType.Text, sb.ToString()));
    }

    private static bool IsTextChar(char c)
    {
        return c != '_' &&
               c != '\\' &&
               c != '[' &&
               c != ']' &&
               c != '(' &&
               c != ')' &&
               c != '<' &&
               c != '>' &&
               c != '#' &&
               c != '\n' &&
               !char.IsWhiteSpace(c);
    }
}