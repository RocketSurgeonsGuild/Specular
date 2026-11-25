using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers;

/// <summary>
///     Lays out the collection expressions GetTypes and GetAssemblies return, after <c>NormalizeWhitespace</c>, which writes them as
///     <c>return[a, b, ...]</c> on one line: <c>return []</c> when empty, otherwise one element per line, so the generated code (and
///     its snapshots) reads like the <c>items.Add</c> lines it replaced.
/// </summary>
internal sealed class CollectionReturnLayout : CSharpSyntaxRewriter
{
    private const string IndentUnit = "    ";

    public override SyntaxNode? VisitReturnStatement(ReturnStatementSyntax node)
    {
        if (node.Expression is not CollectionExpressionSyntax collection) return base.VisitReturnStatement(node);

        if (collection.Elements.Count == 0)
        {
            return node.WithReturnKeyword(node.ReturnKeyword.WithTrailingTrivia(Space));
        }

        var indent = GetIndentation(node);
        var elements = collection.Elements.Select(element => element.WithLeadingTrivia(Whitespace(indent + IndentUnit)));
        var separators = collection.Elements.GetSeparators().Select(separator => separator.WithTrailingTrivia(CarriageReturnLineFeed));

        var laidOut = collection
                     .WithOpenBracketToken(collection.OpenBracketToken.WithLeadingTrivia(Whitespace(indent)).WithTrailingTrivia(CarriageReturnLineFeed))
                     .WithElements(SeparatedList(elements, separators))
                     .WithCloseBracketToken(collection.CloseBracketToken.WithLeadingTrivia(CarriageReturnLineFeed, Whitespace(indent)));
        return node.WithReturnKeyword(node.ReturnKeyword.WithTrailingTrivia(CarriageReturnLineFeed)).WithExpression(laidOut);
    }

    // The whitespace that starts the statement's own line: whatever follows the last line break in its leading trivia (comments
    // above the statement come before it).
    private static string GetIndentation(SyntaxNode node)
    {
        var indent = "";
        foreach (var trivia in node.GetLeadingTrivia())
        {
            if (trivia.IsKind(SyntaxKind.EndOfLineTrivia)) indent = "";
            else if (trivia.IsKind(SyntaxKind.WhitespaceTrivia)) indent = trivia.ToString();
        }

        return indent;
    }
}
