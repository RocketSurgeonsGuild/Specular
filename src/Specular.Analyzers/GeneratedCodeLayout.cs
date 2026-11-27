using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Specular.Analyzers;

/// <summary>
///     Writes the trees the generator builds with <see cref="SyntaxFactory" /> as text in one pass, in place of
///     <c>NormalizeWhitespace</c>. The whitespace before each token is chosen from the token before it: a line break and indentation,
///     a single space, or nothing. Only the syntax the generator emits needs a rule here, and a layout change belongs in this file,
///     not at the call sites that build the code.
/// </summary>
/// <remarks>
///     The whitespace goes straight to the output rather than onto the tree as trivia: setting trivia on every token rebuilds the
///     whole tree, which measured as more allocation than <c>NormalizeWhitespace</c> itself, and nothing reads the laid-out tree,
///     only its text. The rules reproduce <c>NormalizeWhitespace</c>'s layout (Allman braces, four-space indents, a blank line
///     between members and after a closing brace) so the provider and the build cache's text come out exactly as before, and a
///     cache written by an earlier version keeps its hash. The one addition is that a non-empty collection expression puts each
///     element on its own line (one item per line in GetTypes, GetAssemblies and the scan report), which used to take extra passes.
///     Comments and directives already on a token are kept; any other existing whitespace (from re-parsed cache text, or a user's
///     selector expression) is replaced.
/// </remarks>
internal sealed class GeneratedCodeLayout
{
    /// <summary>The line ending the generator has always written into its sources.</summary>
    public const string SourceEndOfLine = "\r\n";

    private const int IndentSize = 4;
    private const int CachedIndentDepth = 16;
    private static readonly string[] Indents = Enumerable.Range(0, CachedIndentDepth).Select(depth => new string(' ', depth * IndentSize)).ToArray();

    /// <summary>The laid-out source of <paramref name="node" /> with <see cref="SourceEndOfLine" /> line endings, for <c>AddSource</c>.</summary>
    public static SourceText ToSourceText(SyntaxNode node) => SourceText.From(Write(node, SourceEndOfLine), Encoding.UTF8);

    /// <summary>The laid-out text of <paramref name="node" /> with <c>\n</c> line endings, the form the build cache stores and hashes.</summary>
    public static string ToText(SyntaxNode node) => Write(node, "\n");

    /// <summary>The laid-out text of <paramref name="node" /> with the given line endings.</summary>
    public static string ToText(SyntaxNode node, string endOfLine) => Write(node, endOfLine);

    private readonly SyntaxNode? _outsideRoot;
    private readonly string _endOfLine;
    private readonly StringBuilder _text;

    private GeneratedCodeLayout(SyntaxNode root, string endOfLine)
    {
        _outsideRoot = root.Parent;
        _endOfLine = endOfLine;
        // Laid-out code runs about a third longer than its tokens alone.
        _text = new(root.Span.Length + ( root.Span.Length / 3 ));
    }

    private static string Write(SyntaxNode node, string endOfLine)
    {
        var layout = new GeneratedCodeLayout(node, endOfLine);
        var previous = default(SyntaxToken);
        foreach (var token in node.DescendantTokens())
        {
            layout.WriteToken(previous, token);
            previous = token;
        }

        return layout._text.ToString();
    }

    private void WriteToken(SyntaxToken previous, SyntaxToken current)
    {
        var hasPrevious = !previous.IsKind(SyntaxKind.None);
        var lineBreaks = hasPrevious ? LineBreaksBetween(previous, current) : 0;

        // A comment trailing the previous token (an empty statement explaining a type the generator could not name) stays on its line.
        if (hasPrevious)
        {
            foreach (var trivia in previous.TrailingTrivia)
            {
                if (!IsComment(trivia)) continue;
                _text.Append(' ').Append(trivia.ToFullString());
                if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia)) lineBreaks = Math.Max(lineBreaks, 1);
            }
        }

        var hasLeadingLine = false;
        foreach (var trivia in current.LeadingTrivia)
        {
            if (!trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) && !trivia.IsDirective) continue;
            hasLeadingLine = true;
            break;
        }

        if (hasLeadingLine && hasPrevious) lineBreaks = Math.Max(lineBreaks, 1);

        if (lineBreaks == 0 && !hasLeadingLine)
        {
            if (hasPrevious && NeedsSpace(previous, current)) _text.Append(' ');
            // A /* comment */ inside a line stays where it was, before its token.
            foreach (var trivia in current.LeadingTrivia)
            {
                if (trivia.IsKind(SyntaxKind.MultiLineCommentTrivia)) _text.Append(trivia.ToFullString()).Append(' ');
            }
        }
        else
        {
            for (var i = 0; i < lineBreaks; i++) _text.Append(_endOfLine);

            var indent = current.IsKind(SyntaxKind.EndOfFileToken) ? "" : Indent(Depth(current));
            foreach (var trivia in current.LeadingTrivia)
            {
                if (trivia.IsDirective)
                {
                    // A directive's text ends with its own line break.
                    _text.Append(trivia.ToFullString());
                }
                else if (IsComment(trivia))
                {
                    _text.Append(indent).Append(trivia.ToFullString()).Append(_endOfLine);
                }
            }

            _text.Append(indent);
        }

        _text.Append(current.Text);
        if (!current.IsKind(SyntaxKind.EndOfFileToken)) return;

        foreach (var trivia in current.TrailingTrivia)
        {
            if (trivia.IsDirective) _text.Append(trivia.ToFullString());
        }
    }

    private static string Indent(int depth) => depth < CachedIndentDepth ? Indents[depth] : new(' ', depth * IndentSize);

    private static bool IsComment(SyntaxTrivia trivia) => trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) || trivia.IsKind(SyntaxKind.MultiLineCommentTrivia);

    // The decisions below test token kinds with if and pattern checks rather than a switch over SyntaxKind: the repository's
    // formatter "populates" such a switch with every SyntaxKind, including ones the oldest supported Roslyn does not have.
    private static int LineBreaksBetween(SyntaxToken previous, SyntaxToken current)
    {
        if (current.IsKind(SyntaxKind.EndOfFileToken)) return 1;
        if (current.Kind() is SyntaxKind.OpenBraceToken or SyntaxKind.CloseBraceToken && IsLineBrace(current)) return 1;
        if (IsMultilineCollectionBracket(current)) return 1;

        var previousKind = previous.Kind();
        if (previousKind is SyntaxKind.OpenBraceToken && IsLineBrace(previous)) return 1;
        if (previousKind is SyntaxKind.CloseBraceToken && IsLineBrace(previous)) return LineBreaksAfterCloseBrace(current);
        if (previousKind is SyntaxKind.SemicolonToken) return LineBreaksAfterSemicolon(previous, current);
        if (previousKind is SyntaxKind.OpenBracketToken && IsMultilineCollectionBracket(previous)) return 1;
        if (previousKind is SyntaxKind.CommaToken && previous.Parent is CollectionExpressionSyntax collection && IsMultiline(collection)) return 1;
        var endsAttributeOrLabel = ( previousKind is SyntaxKind.CloseBracketToken && previous.Parent is AttributeListSyntax { Parent: not ParameterSyntax and not TypeParameterSyntax } )
         || ( previousKind is SyntaxKind.ColonToken && previous.Parent is SwitchLabelSyntax );
        return endsAttributeOrLabel ? 1 : 0;
    }

    // A blank line follows a closing brace, as it does after a member, unless the brace closes its parent too or the statement
    // continues.
    private static int LineBreaksAfterCloseBrace(SyntaxToken current) =>
        current.Kind() is SyntaxKind.SemicolonToken or SyntaxKind.CommaToken or SyntaxKind.CloseParenToken ? 0
        : current.Kind() is SyntaxKind.CloseBraceToken or SyntaxKind.ElseKeyword or SyntaxKind.CatchKeyword or SyntaxKind.FinallyKeyword ? 1
        : 2;

    private static int LineBreaksAfterSemicolon(SyntaxToken previous, SyntaxToken current)
    {
        if (previous.Parent is AccessorDeclarationSyntax { Parent: AccessorListSyntax accessors } && IsInline(accessors)) return 0;
        if (previous.Parent is ForStatementSyntax) return 0;
        if (previous.Parent is UsingDirectiveSyntax) return current.Parent is UsingDirectiveSyntax ? 1 : 2;
        var startsSiblingMember = !current.IsKind(SyntaxKind.CloseBraceToken)
         && previous.Parent is MemberDeclarationSyntax member
         && IsFirstTokenOfSiblingMember(current, member);
        // Fields sit together; any other member is followed by a blank line.
        return startsSiblingMember && previous.Parent is not BaseFieldDeclarationSyntax ? 2 : 1;
    }

    private static bool IsFirstTokenOfSiblingMember(SyntaxToken token, MemberDeclarationSyntax previousMember)
    {
        for (var node = token.Parent; node is { }; node = node.Parent)
        {
            if (node is not MemberDeclarationSyntax member) continue;
            return member.Parent == previousMember.Parent && member.GetFirstToken() == token;
        }

        return false;
    }

    private static bool NeedsSpace(SyntaxToken previous, SyntaxToken current)
    {
        if (IsSpacedOperator(previous) || IsSpacedOperator(current)) return true;

        // NormalizeWhitespace writes typeof(T<,,>) as T<,, >; keeping that keeps the build cache's text, and so its hash, unchanged.
        if (current.IsKind(SyntaxKind.OmittedTypeArgumentToken)) return false;
        if (previous.IsKind(SyntaxKind.OmittedTypeArgumentToken))
        {
            return current.IsKind(SyntaxKind.GreaterThanToken) && previous.GetPreviousToken().IsKind(SyntaxKind.CommaToken);
        }

        if (HasNoSpaceBefore(current)) return false;
        if (HasNoSpaceAfter(previous)) return false;

        var previousKind = previous.Kind();
        if (previousKind is SyntaxKind.CommaToken
                         or SyntaxKind.SemicolonToken
                         or SyntaxKind.ColonToken
                         or SyntaxKind.OpenBraceToken
                         or SyntaxKind.CloseBraceToken)
        {
            return true;
        }

        if (previousKind is SyntaxKind.CloseParenToken)
        {
            return previous.Parent is not CastExpressionSyntax && ( IsWordLike(current) || current.IsKind(SyntaxKind.OpenBraceToken) );
        }

        if (previousKind is SyntaxKind.CloseBracketToken or SyntaxKind.GreaterThanToken or SyntaxKind.ExclamationToken)
        {
            return IsWordLike(current) || current.IsKind(SyntaxKind.OpenBraceToken);
        }

        if (current.IsKind(SyntaxKind.OpenParenToken)) return IsKeyword(previous) && !IsInvokedLikeAFunction(previous);
        var separatesWords = IsWordLike(previous)
         && ( IsWordLike(current) || current.IsKind(SyntaxKind.OpenBraceToken) || current.Parent is PrefixUnaryExpressionSyntax );
        return current.IsKind(SyntaxKind.OpenBracketToken) ? previous.Kind() is SyntaxKind.ReturnKeyword or SyntaxKind.InKeyword : separatesWords;
    }

    private static bool HasNoSpaceBefore(SyntaxToken token) =>
        token.Kind() is SyntaxKind.CommaToken
                     or SyntaxKind.SemicolonToken
                     or SyntaxKind.CloseParenToken
                     or SyntaxKind.CloseBracketToken
                     or SyntaxKind.DotToken
                     or SyntaxKind.ColonColonToken
                     or SyntaxKind.ColonToken
                     or SyntaxKind.EndOfFileToken
     || ( token.IsKind(SyntaxKind.ExclamationToken) && token.Parent is PostfixUnaryExpressionSyntax )
     || ( token.IsKind(SyntaxKind.QuestionToken) && token.Parent is NullableTypeSyntax )
     || IsTypeArgumentBracket(token);

    private static bool HasNoSpaceAfter(SyntaxToken token) =>
        token.Kind() is SyntaxKind.OpenParenToken or SyntaxKind.OpenBracketToken or SyntaxKind.DotToken or SyntaxKind.ColonColonToken
     || ( token.IsKind(SyntaxKind.LessThanToken) && IsTypeArgumentBracket(token) )
     || ( token.Kind() is SyntaxKind.ExclamationToken or SyntaxKind.MinusToken or SyntaxKind.TildeToken && token.Parent is PrefixUnaryExpressionSyntax );

    private static bool IsTypeArgumentBracket(SyntaxToken token) =>
        token.Kind() is SyntaxKind.LessThanToken or SyntaxKind.GreaterThanToken && token.Parent is TypeArgumentListSyntax or TypeParameterListSyntax;

    // Operators written with a space on each side.
    private static bool IsSpacedOperator(SyntaxToken token) =>
        token.Parent switch
        {
            BinaryExpressionSyntax binary => binary.OperatorToken == token,
            AssignmentExpressionSyntax assignment => assignment.OperatorToken == token,
            EqualsValueClauseSyntax equalsValue => equalsValue.EqualsToken == token,
            NameEqualsSyntax nameEquals => nameEquals.EqualsToken == token,
            ArrowExpressionClauseSyntax arrow => arrow.ArrowToken == token,
            LambdaExpressionSyntax lambda => lambda.ArrowToken == token,
            ConditionalExpressionSyntax conditional => conditional.QuestionToken == token || conditional.ColonToken == token,
            BaseListSyntax baseList => baseList.ColonToken == token,
            TypeParameterConstraintClauseSyntax clause => clause.ColonToken == token,
            IsPatternExpressionSyntax isPattern => isPattern.IsKeyword == token,
            SwitchExpressionArmSyntax arm => arm.EqualsGreaterThanToken == token,
            _ => false,
        };

    // Keywords that take their parentheses without a space, as a call would: typeof(T), default(T), new(...).
    private static bool IsInvokedLikeAFunction(SyntaxToken keyword) =>
        keyword.Kind() is SyntaxKind.TypeOfKeyword
                       or SyntaxKind.DefaultKeyword
                       or SyntaxKind.SizeOfKeyword
                       or SyntaxKind.NewKeyword
                       or SyntaxKind.CheckedKeyword
                       or SyntaxKind.UncheckedKeyword
                       or SyntaxKind.ThisKeyword
                       or SyntaxKind.BaseKeyword
                       or SyntaxKind.MakeRefKeyword
                       or SyntaxKind.RefTypeKeyword
                       or SyntaxKind.RefValueKeyword
     || SyntaxFacts.IsPredefinedType(keyword.Kind());

    private static bool IsKeyword(SyntaxToken token) => SyntaxFacts.IsKeywordKind(token.Kind());

    private static bool IsWordLike(SyntaxToken token) =>
        token.Kind() is SyntaxKind.IdentifierToken
                     or SyntaxKind.NumericLiteralToken
                     or SyntaxKind.StringLiteralToken
                     or SyntaxKind.CharacterLiteralToken
                     or SyntaxKind.Utf8StringLiteralToken
                     or SyntaxKind.SingleLineRawStringLiteralToken
                     or SyntaxKind.MultiLineRawStringLiteralToken
     || SyntaxFacts.IsKeywordKind(token.Kind());

    // Braces that sit on their own line, as opposed to an auto-property's { get; } or an initializer's.
    private static bool IsLineBrace(SyntaxToken brace) =>
        brace.Parent switch
        {
            BlockSyntax => true,
            BaseTypeDeclarationSyntax => true,
            NamespaceDeclarationSyntax => true,
            SwitchStatementSyntax => true,
            AccessorListSyntax accessors => !IsInline(accessors),
            _ => false,
        };

    private static bool IsInline(AccessorListSyntax accessors)
    {
        foreach (var accessor in accessors.Accessors)
        {
            if (accessor.Body is { } || accessor.ExpressionBody is { }) return false;
        }

        return true;
    }

    private static bool IsMultiline(CollectionExpressionSyntax collection) => collection.Elements.Count > 0;

    private static bool IsMultilineCollectionBracket(SyntaxToken token) =>
        token.Kind() is SyntaxKind.OpenBracketToken or SyntaxKind.CloseBracketToken
     && token.Parent is CollectionExpressionSyntax collection
     && IsMultiline(collection);

    // The indentation of a token that starts a line: one level for each enclosing pair of braces, the statements under a case label,
    // and the elements of a multi-line collection. A collection that is not returned (an argument, as in the scan report) also
    // indents its brackets, so they stand apart from the call around them.
    private int Depth(SyntaxToken token)
    {
        var depth = 0;
        var position = token.SpanStart;
        for (var node = token.Parent; node is { } && node != _outsideRoot; node = node.Parent)
        {
            if (node is CollectionExpressionSyntax collection && IsMultiline(collection))
            {
                var isBracket = token == collection.OpenBracketToken || token == collection.CloseBracketToken;
                var isReturned = collection.Parent is ReturnStatementSyntax;
                depth += ( isBracket ? 0 : 1 ) + ( isReturned ? 0 : 1 );
            }
            else if (IsIndentedWithin(node, position))
            {
                depth++;
            }
        }

        return depth;
    }

    private static bool IsIndentedWithin(SyntaxNode node, int position) =>
        node switch
        {
            BlockSyntax block => IsInside(position, block.OpenBraceToken, block.CloseBraceToken),
            BaseTypeDeclarationSyntax type => IsInside(position, type.OpenBraceToken, type.CloseBraceToken),
            NamespaceDeclarationSyntax ns => IsInside(position, ns.OpenBraceToken, ns.CloseBraceToken),
            SwitchStatementSyntax switchStatement => IsInside(position, switchStatement.OpenBraceToken, switchStatement.CloseBraceToken),
            AccessorListSyntax accessors => !IsInline(accessors) && IsInside(position, accessors.OpenBraceToken, accessors.CloseBraceToken),
            SwitchSectionSyntax { Statements: [var firstStatement, ..] } => position >= firstStatement.SpanStart,
            _ => false,
        };

    private static bool IsInside(int position, SyntaxToken open, SyntaxToken close) => position > open.SpanStart && position < close.SpanStart;
}
