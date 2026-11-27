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

    private static int LineBreaksBetween(SyntaxToken previous, SyntaxToken current)
    {
        if (current.IsKind(SyntaxKind.EndOfFileToken)) return 1;

        if (current.IsKind(SyntaxKind.OpenBraceToken) || current.IsKind(SyntaxKind.CloseBraceToken))
        {
            if (IsLineBrace(current)) return 1;
        }

        return  IsMultilineCollectionBracket(current) 
            ?  1 
            : previous.Kind() switch
        {
            SyntaxKind.OpenBraceToken when IsLineBrace(previous) => 1,
            SyntaxKind.CloseBraceToken when IsLineBrace(previous) => current.Kind() switch
            {
                SyntaxKind.CloseBraceToken => 1,
                SyntaxKind.SemicolonToken or SyntaxKind.CommaToken or SyntaxKind.CloseParenToken => 0,
                SyntaxKind.ElseKeyword or SyntaxKind.CatchKeyword or SyntaxKind.FinallyKeyword => 1,
                _ => 2,
            },
            SyntaxKind.SemicolonToken => LineBreaksAfterSemicolon(previous, current),
            SyntaxKind.OpenBracketToken when IsMultilineCollectionBracket(previous) => 1,
            SyntaxKind.CommaToken when previous.Parent is CollectionExpressionSyntax collection && IsMultiline(collection) => 1,
            SyntaxKind.CloseBracketToken when previous.Parent is AttributeListSyntax { Parent: not ParameterSyntax and not TypeParameterSyntax } => 1,
            SyntaxKind.ColonToken when previous.Parent is SwitchLabelSyntax => 1,
            _ => 0,
        };
    }

    private static int LineBreaksAfterSemicolon(SyntaxToken previous, SyntaxToken current)
    {
        switch (previous.Parent)
        {
            case AccessorDeclarationSyntax { Parent: AccessorListSyntax accessors } when IsInline(accessors):
            case ForStatementSyntax:
                return 0;
            case UsingDirectiveSyntax:
                return current.Parent is UsingDirectiveSyntax ? 1 : 2;
            default:
                break;
        }

        if (current.IsKind(SyntaxKind.CloseBraceToken)) return 1;
        return  previous.Parent is MemberDeclarationSyntax member && IsFirstTokenOfSiblingMember(current, member) 
            ?  member is BaseFieldDeclarationSyntax ? 1 : 2 
            :  1;
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

        switch (current.Kind())
        {
            case SyntaxKind.CommaToken:
            case SyntaxKind.SemicolonToken:
            case SyntaxKind.CloseParenToken:
            case SyntaxKind.CloseBracketToken:
            case SyntaxKind.DotToken:
            case SyntaxKind.ColonColonToken:
            case SyntaxKind.ColonToken:
            case SyntaxKind.EndOfFileToken:
                return false;
            case SyntaxKind.ExclamationToken when current.Parent is PostfixUnaryExpressionSyntax:
            case SyntaxKind.QuestionToken when current.Parent is NullableTypeSyntax:
            case SyntaxKind.LessThanToken or SyntaxKind.GreaterThanToken when current.Parent is TypeArgumentListSyntax or TypeParameterListSyntax:
                return false;
            case SyntaxKind.None:
                break;
            case SyntaxKind.List:
                break;
            case SyntaxKind.TildeToken:
                break;
            case SyntaxKind.ExclamationToken:
                break;
            case SyntaxKind.DollarToken:
                break;
            case SyntaxKind.PercentToken:
                break;
            case SyntaxKind.CaretToken:
                break;
            case SyntaxKind.AmpersandToken:
                break;
            case SyntaxKind.AsteriskToken:
                break;
            case SyntaxKind.OpenParenToken:
                break;
            case SyntaxKind.MinusToken:
                break;
            case SyntaxKind.PlusToken:
                break;
            case SyntaxKind.EqualsToken:
                break;
            case SyntaxKind.OpenBraceToken:
                break;
            case SyntaxKind.CloseBraceToken:
                break;
            case SyntaxKind.OpenBracketToken:
                break;
            case SyntaxKind.BarToken:
                break;
            case SyntaxKind.BackslashToken:
                break;
            case SyntaxKind.DoubleQuoteToken:
                break;
            case SyntaxKind.SingleQuoteToken:
                break;
            case SyntaxKind.QuestionToken:
                break;
            case SyntaxKind.HashToken:
                break;
            case SyntaxKind.SlashToken:
                break;
            case SyntaxKind.DotDotToken:
                break;
            case SyntaxKind.SlashGreaterThanToken:
                break;
            case SyntaxKind.LessThanSlashToken:
                break;
            case SyntaxKind.XmlCommentStartToken:
                break;
            case SyntaxKind.XmlCommentEndToken:
                break;
            case SyntaxKind.XmlCDataStartToken:
                break;
            case SyntaxKind.XmlCDataEndToken:
                break;
            case SyntaxKind.XmlProcessingInstructionStartToken:
                break;
            case SyntaxKind.XmlProcessingInstructionEndToken:
                break;
            case SyntaxKind.BarBarToken:
                break;
            case SyntaxKind.AmpersandAmpersandToken:
                break;
            case SyntaxKind.MinusMinusToken:
                break;
            case SyntaxKind.PlusPlusToken:
                break;
            case SyntaxKind.QuestionQuestionToken:
                break;
            case SyntaxKind.MinusGreaterThanToken:
                break;
            case SyntaxKind.ExclamationEqualsToken:
                break;
            case SyntaxKind.EqualsEqualsToken:
                break;
            case SyntaxKind.EqualsGreaterThanToken:
                break;
            case SyntaxKind.LessThanEqualsToken:
                break;
            case SyntaxKind.LessThanLessThanToken:
                break;
            case SyntaxKind.LessThanLessThanEqualsToken:
                break;
            case SyntaxKind.GreaterThanEqualsToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanEqualsToken:
                break;
            case SyntaxKind.SlashEqualsToken:
                break;
            case SyntaxKind.AsteriskEqualsToken:
                break;
            case SyntaxKind.BarEqualsToken:
                break;
            case SyntaxKind.AmpersandEqualsToken:
                break;
            case SyntaxKind.PlusEqualsToken:
                break;
            case SyntaxKind.MinusEqualsToken:
                break;
            case SyntaxKind.CaretEqualsToken:
                break;
            case SyntaxKind.PercentEqualsToken:
                break;
            case SyntaxKind.QuestionQuestionEqualsToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanGreaterThanToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanGreaterThanEqualsToken:
                break;
            case SyntaxKind.BoolKeyword:
                break;
            case SyntaxKind.ByteKeyword:
                break;
            case SyntaxKind.SByteKeyword:
                break;
            case SyntaxKind.ShortKeyword:
                break;
            case SyntaxKind.UShortKeyword:
                break;
            case SyntaxKind.IntKeyword:
                break;
            case SyntaxKind.UIntKeyword:
                break;
            case SyntaxKind.LongKeyword:
                break;
            case SyntaxKind.ULongKeyword:
                break;
            case SyntaxKind.DoubleKeyword:
                break;
            case SyntaxKind.FloatKeyword:
                break;
            case SyntaxKind.DecimalKeyword:
                break;
            case SyntaxKind.StringKeyword:
                break;
            case SyntaxKind.CharKeyword:
                break;
            case SyntaxKind.VoidKeyword:
                break;
            case SyntaxKind.ObjectKeyword:
                break;
            case SyntaxKind.TypeOfKeyword:
                break;
            case SyntaxKind.SizeOfKeyword:
                break;
            case SyntaxKind.NullKeyword:
                break;
            case SyntaxKind.TrueKeyword:
                break;
            case SyntaxKind.FalseKeyword:
                break;
            case SyntaxKind.IfKeyword:
                break;
            case SyntaxKind.ElseKeyword:
                break;
            case SyntaxKind.WhileKeyword:
                break;
            case SyntaxKind.ForKeyword:
                break;
            case SyntaxKind.ForEachKeyword:
                break;
            case SyntaxKind.DoKeyword:
                break;
            case SyntaxKind.SwitchKeyword:
                break;
            case SyntaxKind.CaseKeyword:
                break;
            case SyntaxKind.DefaultKeyword:
                break;
            case SyntaxKind.TryKeyword:
                break;
            case SyntaxKind.CatchKeyword:
                break;
            case SyntaxKind.FinallyKeyword:
                break;
            case SyntaxKind.LockKeyword:
                break;
            case SyntaxKind.GotoKeyword:
                break;
            case SyntaxKind.BreakKeyword:
                break;
            case SyntaxKind.ContinueKeyword:
                break;
            case SyntaxKind.ReturnKeyword:
                break;
            case SyntaxKind.ThrowKeyword:
                break;
            case SyntaxKind.PublicKeyword:
                break;
            case SyntaxKind.PrivateKeyword:
                break;
            case SyntaxKind.InternalKeyword:
                break;
            case SyntaxKind.ProtectedKeyword:
                break;
            case SyntaxKind.StaticKeyword:
                break;
            case SyntaxKind.ReadOnlyKeyword:
                break;
            case SyntaxKind.SealedKeyword:
                break;
            case SyntaxKind.ConstKeyword:
                break;
            case SyntaxKind.FixedKeyword:
                break;
            case SyntaxKind.StackAllocKeyword:
                break;
            case SyntaxKind.VolatileKeyword:
                break;
            case SyntaxKind.NewKeyword:
                break;
            case SyntaxKind.OverrideKeyword:
                break;
            case SyntaxKind.AbstractKeyword:
                break;
            case SyntaxKind.VirtualKeyword:
                break;
            case SyntaxKind.EventKeyword:
                break;
            case SyntaxKind.ExternKeyword:
                break;
            case SyntaxKind.RefKeyword:
                break;
            case SyntaxKind.OutKeyword:
                break;
            case SyntaxKind.InKeyword:
                break;
            case SyntaxKind.IsKeyword:
                break;
            case SyntaxKind.AsKeyword:
                break;
            case SyntaxKind.ParamsKeyword:
                break;
            case SyntaxKind.ArgListKeyword:
                break;
            case SyntaxKind.MakeRefKeyword:
                break;
            case SyntaxKind.RefTypeKeyword:
                break;
            case SyntaxKind.RefValueKeyword:
                break;
            case SyntaxKind.ThisKeyword:
                break;
            case SyntaxKind.BaseKeyword:
                break;
            case SyntaxKind.NamespaceKeyword:
                break;
            case SyntaxKind.UsingKeyword:
                break;
            case SyntaxKind.ClassKeyword:
                break;
            case SyntaxKind.StructKeyword:
                break;
            case SyntaxKind.InterfaceKeyword:
                break;
            case SyntaxKind.EnumKeyword:
                break;
            case SyntaxKind.DelegateKeyword:
                break;
            case SyntaxKind.CheckedKeyword:
                break;
            case SyntaxKind.UncheckedKeyword:
                break;
            case SyntaxKind.UnsafeKeyword:
                break;
            case SyntaxKind.OperatorKeyword:
                break;
            case SyntaxKind.ExplicitKeyword:
                break;
            case SyntaxKind.ImplicitKeyword:
                break;
            case SyntaxKind.YieldKeyword:
                break;
            case SyntaxKind.PartialKeyword:
                break;
            case SyntaxKind.AliasKeyword:
                break;
            case SyntaxKind.GlobalKeyword:
                break;
            case SyntaxKind.AssemblyKeyword:
                break;
            case SyntaxKind.ModuleKeyword:
                break;
            case SyntaxKind.TypeKeyword:
                break;
            case SyntaxKind.FieldKeyword:
                break;
            case SyntaxKind.MethodKeyword:
                break;
            case SyntaxKind.ParamKeyword:
                break;
            case SyntaxKind.PropertyKeyword:
                break;
            case SyntaxKind.TypeVarKeyword:
                break;
            case SyntaxKind.GetKeyword:
                break;
            case SyntaxKind.SetKeyword:
                break;
            case SyntaxKind.AddKeyword:
                break;
            case SyntaxKind.RemoveKeyword:
                break;
            case SyntaxKind.WhereKeyword:
                break;
            case SyntaxKind.FromKeyword:
                break;
            case SyntaxKind.GroupKeyword:
                break;
            case SyntaxKind.JoinKeyword:
                break;
            case SyntaxKind.IntoKeyword:
                break;
            case SyntaxKind.LetKeyword:
                break;
            case SyntaxKind.ByKeyword:
                break;
            case SyntaxKind.SelectKeyword:
                break;
            case SyntaxKind.OrderByKeyword:
                break;
            case SyntaxKind.OnKeyword:
                break;
            case SyntaxKind.EqualsKeyword:
                break;
            case SyntaxKind.AscendingKeyword:
                break;
            case SyntaxKind.DescendingKeyword:
                break;
            case SyntaxKind.NameOfKeyword:
                break;
            case SyntaxKind.AsyncKeyword:
                break;
            case SyntaxKind.AwaitKeyword:
                break;
            case SyntaxKind.WhenKeyword:
                break;
            case SyntaxKind.OrKeyword:
                break;
            case SyntaxKind.AndKeyword:
                break;
            case SyntaxKind.NotKeyword:
                break;
            case SyntaxKind.WithKeyword:
                break;
            case SyntaxKind.InitKeyword:
                break;
            case SyntaxKind.RecordKeyword:
                break;
            case SyntaxKind.ManagedKeyword:
                break;
            case SyntaxKind.UnmanagedKeyword:
                break;
            case SyntaxKind.RequiredKeyword:
                break;
            case SyntaxKind.ScopedKeyword:
                break;
            case SyntaxKind.FileKeyword:
                break;
            case SyntaxKind.AllowsKeyword:
                break;
            case SyntaxKind.ExtensionKeyword:
                break;
            case SyntaxKind.UnionKeyword:
                break;
            case SyntaxKind.ClosedKeyword:
                break;
            case SyntaxKind.SafeKeyword:
                break;
            case SyntaxKind.ElifKeyword:
                break;
            case SyntaxKind.EndIfKeyword:
                break;
            case SyntaxKind.RegionKeyword:
                break;
            case SyntaxKind.EndRegionKeyword:
                break;
            case SyntaxKind.DefineKeyword:
                break;
            case SyntaxKind.UndefKeyword:
                break;
            case SyntaxKind.WarningKeyword:
                break;
            case SyntaxKind.ErrorKeyword:
                break;
            case SyntaxKind.LineKeyword:
                break;
            case SyntaxKind.PragmaKeyword:
                break;
            case SyntaxKind.HiddenKeyword:
                break;
            case SyntaxKind.ChecksumKeyword:
                break;
            case SyntaxKind.DisableKeyword:
                break;
            case SyntaxKind.RestoreKeyword:
                break;
            case SyntaxKind.ReferenceKeyword:
                break;
            case SyntaxKind.InterpolatedStringStartToken:
                break;
            case SyntaxKind.InterpolatedStringEndToken:
                break;
            case SyntaxKind.InterpolatedVerbatimStringStartToken:
                break;
            case SyntaxKind.LoadKeyword:
                break;
            case SyntaxKind.NullableKeyword:
                break;
            case SyntaxKind.EnableKeyword:
                break;
            case SyntaxKind.WarningsKeyword:
                break;
            case SyntaxKind.AnnotationsKeyword:
                break;
            case SyntaxKind.VarKeyword:
                break;
            case SyntaxKind.UnderscoreToken:
                break;
            case SyntaxKind.OmittedTypeArgumentToken:
                break;
            case SyntaxKind.OmittedArraySizeExpressionToken:
                break;
            case SyntaxKind.EndOfDirectiveToken:
                break;
            case SyntaxKind.EndOfDocumentationCommentToken:
                break;
            case SyntaxKind.BadToken:
                break;
            case SyntaxKind.IdentifierToken:
                break;
            case SyntaxKind.NumericLiteralToken:
                break;
            case SyntaxKind.CharacterLiteralToken:
                break;
            case SyntaxKind.StringLiteralToken:
                break;
            case SyntaxKind.XmlEntityLiteralToken:
                break;
            case SyntaxKind.XmlTextLiteralToken:
                break;
            case SyntaxKind.XmlTextLiteralNewLineToken:
                break;
            case SyntaxKind.InterpolatedStringToken:
                break;
            case SyntaxKind.InterpolatedStringTextToken:
                break;
            case SyntaxKind.SingleLineRawStringLiteralToken:
                break;
            case SyntaxKind.MultiLineRawStringLiteralToken:
                break;
            case SyntaxKind.Utf8StringLiteralToken:
                break;
            case SyntaxKind.Utf8SingleLineRawStringLiteralToken:
                break;
            case SyntaxKind.Utf8MultiLineRawStringLiteralToken:
                break;
            case SyntaxKind.RazorContentToken:
                break;
            case SyntaxKind.EndOfLineTrivia:
                break;
            case SyntaxKind.WhitespaceTrivia:
                break;
            case SyntaxKind.SingleLineCommentTrivia:
                break;
            case SyntaxKind.MultiLineCommentTrivia:
                break;
            case SyntaxKind.DocumentationCommentExteriorTrivia:
                break;
            case SyntaxKind.SingleLineDocumentationCommentTrivia:
                break;
            case SyntaxKind.MultiLineDocumentationCommentTrivia:
                break;
            case SyntaxKind.DisabledTextTrivia:
                break;
            case SyntaxKind.PreprocessingMessageTrivia:
                break;
            case SyntaxKind.IfDirectiveTrivia:
                break;
            case SyntaxKind.ElifDirectiveTrivia:
                break;
            case SyntaxKind.ElseDirectiveTrivia:
                break;
            case SyntaxKind.EndIfDirectiveTrivia:
                break;
            case SyntaxKind.RegionDirectiveTrivia:
                break;
            case SyntaxKind.EndRegionDirectiveTrivia:
                break;
            case SyntaxKind.DefineDirectiveTrivia:
                break;
            case SyntaxKind.UndefDirectiveTrivia:
                break;
            case SyntaxKind.ErrorDirectiveTrivia:
                break;
            case SyntaxKind.WarningDirectiveTrivia:
                break;
            case SyntaxKind.LineDirectiveTrivia:
                break;
            case SyntaxKind.PragmaWarningDirectiveTrivia:
                break;
            case SyntaxKind.PragmaChecksumDirectiveTrivia:
                break;
            case SyntaxKind.ReferenceDirectiveTrivia:
                break;
            case SyntaxKind.BadDirectiveTrivia:
                break;
            case SyntaxKind.SkippedTokensTrivia:
                break;
            case SyntaxKind.ConflictMarkerTrivia:
                break;
            case SyntaxKind.XmlElement:
                break;
            case SyntaxKind.XmlElementStartTag:
                break;
            case SyntaxKind.XmlElementEndTag:
                break;
            case SyntaxKind.XmlEmptyElement:
                break;
            case SyntaxKind.XmlTextAttribute:
                break;
            case SyntaxKind.XmlCrefAttribute:
                break;
            case SyntaxKind.XmlNameAttribute:
                break;
            case SyntaxKind.XmlName:
                break;
            case SyntaxKind.XmlPrefix:
                break;
            case SyntaxKind.XmlText:
                break;
            case SyntaxKind.XmlCDataSection:
                break;
            case SyntaxKind.XmlComment:
                break;
            case SyntaxKind.XmlProcessingInstruction:
                break;
            case SyntaxKind.TypeCref:
                break;
            case SyntaxKind.QualifiedCref:
                break;
            case SyntaxKind.NameMemberCref:
                break;
            case SyntaxKind.IndexerMemberCref:
                break;
            case SyntaxKind.OperatorMemberCref:
                break;
            case SyntaxKind.ConversionOperatorMemberCref:
                break;
            case SyntaxKind.CrefParameterList:
                break;
            case SyntaxKind.CrefBracketedParameterList:
                break;
            case SyntaxKind.CrefParameter:
                break;
            case SyntaxKind.ExtensionMemberCref:
                break;
            case SyntaxKind.IdentifierName:
                break;
            case SyntaxKind.QualifiedName:
                break;
            case SyntaxKind.GenericName:
                break;
            case SyntaxKind.TypeArgumentList:
                break;
            case SyntaxKind.AliasQualifiedName:
                break;
            case SyntaxKind.PredefinedType:
                break;
            case SyntaxKind.ArrayType:
                break;
            case SyntaxKind.ArrayRankSpecifier:
                break;
            case SyntaxKind.PointerType:
                break;
            case SyntaxKind.NullableType:
                break;
            case SyntaxKind.OmittedTypeArgument:
                break;
            case SyntaxKind.ParenthesizedExpression:
                break;
            case SyntaxKind.ConditionalExpression:
                break;
            case SyntaxKind.InvocationExpression:
                break;
            case SyntaxKind.ElementAccessExpression:
                break;
            case SyntaxKind.ArgumentList:
                break;
            case SyntaxKind.BracketedArgumentList:
                break;
            case SyntaxKind.Argument:
                break;
            case SyntaxKind.NameColon:
                break;
            case SyntaxKind.CastExpression:
                break;
            case SyntaxKind.AnonymousMethodExpression:
                break;
            case SyntaxKind.SimpleLambdaExpression:
                break;
            case SyntaxKind.ParenthesizedLambdaExpression:
                break;
            case SyntaxKind.ObjectInitializerExpression:
                break;
            case SyntaxKind.CollectionInitializerExpression:
                break;
            case SyntaxKind.ArrayInitializerExpression:
                break;
            case SyntaxKind.AnonymousObjectMemberDeclarator:
                break;
            case SyntaxKind.ComplexElementInitializerExpression:
                break;
            case SyntaxKind.ObjectCreationExpression:
                break;
            case SyntaxKind.AnonymousObjectCreationExpression:
                break;
            case SyntaxKind.ArrayCreationExpression:
                break;
            case SyntaxKind.ImplicitArrayCreationExpression:
                break;
            case SyntaxKind.StackAllocArrayCreationExpression:
                break;
            case SyntaxKind.OmittedArraySizeExpression:
                break;
            case SyntaxKind.InterpolatedStringExpression:
                break;
            case SyntaxKind.ImplicitElementAccess:
                break;
            case SyntaxKind.IsPatternExpression:
                break;
            case SyntaxKind.RangeExpression:
                break;
            case SyntaxKind.ImplicitObjectCreationExpression:
                break;
            case SyntaxKind.AddExpression:
                break;
            case SyntaxKind.SubtractExpression:
                break;
            case SyntaxKind.MultiplyExpression:
                break;
            case SyntaxKind.DivideExpression:
                break;
            case SyntaxKind.ModuloExpression:
                break;
            case SyntaxKind.LeftShiftExpression:
                break;
            case SyntaxKind.RightShiftExpression:
                break;
            case SyntaxKind.LogicalOrExpression:
                break;
            case SyntaxKind.LogicalAndExpression:
                break;
            case SyntaxKind.BitwiseOrExpression:
                break;
            case SyntaxKind.BitwiseAndExpression:
                break;
            case SyntaxKind.ExclusiveOrExpression:
                break;
            case SyntaxKind.EqualsExpression:
                break;
            case SyntaxKind.NotEqualsExpression:
                break;
            case SyntaxKind.LessThanExpression:
                break;
            case SyntaxKind.LessThanOrEqualExpression:
                break;
            case SyntaxKind.GreaterThanExpression:
                break;
            case SyntaxKind.GreaterThanOrEqualExpression:
                break;
            case SyntaxKind.IsExpression:
                break;
            case SyntaxKind.AsExpression:
                break;
            case SyntaxKind.CoalesceExpression:
                break;
            case SyntaxKind.SimpleMemberAccessExpression:
                break;
            case SyntaxKind.PointerMemberAccessExpression:
                break;
            case SyntaxKind.ConditionalAccessExpression:
                break;
            case SyntaxKind.UnsignedRightShiftExpression:
                break;
            case SyntaxKind.MemberBindingExpression:
                break;
            case SyntaxKind.ElementBindingExpression:
                break;
            case SyntaxKind.SimpleAssignmentExpression:
                break;
            case SyntaxKind.AddAssignmentExpression:
                break;
            case SyntaxKind.SubtractAssignmentExpression:
                break;
            case SyntaxKind.MultiplyAssignmentExpression:
                break;
            case SyntaxKind.DivideAssignmentExpression:
                break;
            case SyntaxKind.ModuloAssignmentExpression:
                break;
            case SyntaxKind.AndAssignmentExpression:
                break;
            case SyntaxKind.ExclusiveOrAssignmentExpression:
                break;
            case SyntaxKind.OrAssignmentExpression:
                break;
            case SyntaxKind.LeftShiftAssignmentExpression:
                break;
            case SyntaxKind.RightShiftAssignmentExpression:
                break;
            case SyntaxKind.CoalesceAssignmentExpression:
                break;
            case SyntaxKind.UnsignedRightShiftAssignmentExpression:
                break;
            case SyntaxKind.UnaryPlusExpression:
                break;
            case SyntaxKind.UnaryMinusExpression:
                break;
            case SyntaxKind.BitwiseNotExpression:
                break;
            case SyntaxKind.LogicalNotExpression:
                break;
            case SyntaxKind.PreIncrementExpression:
                break;
            case SyntaxKind.PreDecrementExpression:
                break;
            case SyntaxKind.PointerIndirectionExpression:
                break;
            case SyntaxKind.AddressOfExpression:
                break;
            case SyntaxKind.PostIncrementExpression:
                break;
            case SyntaxKind.PostDecrementExpression:
                break;
            case SyntaxKind.AwaitExpression:
                break;
            case SyntaxKind.IndexExpression:
                break;
            case SyntaxKind.ThisExpression:
                break;
            case SyntaxKind.BaseExpression:
                break;
            case SyntaxKind.ArgListExpression:
                break;
            case SyntaxKind.NumericLiteralExpression:
                break;
            case SyntaxKind.StringLiteralExpression:
                break;
            case SyntaxKind.CharacterLiteralExpression:
                break;
            case SyntaxKind.TrueLiteralExpression:
                break;
            case SyntaxKind.FalseLiteralExpression:
                break;
            case SyntaxKind.NullLiteralExpression:
                break;
            case SyntaxKind.DefaultLiteralExpression:
                break;
            case SyntaxKind.Utf8StringLiteralExpression:
                break;
            case SyntaxKind.FieldExpression:
                break;
            case SyntaxKind.TypeOfExpression:
                break;
            case SyntaxKind.SizeOfExpression:
                break;
            case SyntaxKind.CheckedExpression:
                break;
            case SyntaxKind.UncheckedExpression:
                break;
            case SyntaxKind.DefaultExpression:
                break;
            case SyntaxKind.MakeRefExpression:
                break;
            case SyntaxKind.RefValueExpression:
                break;
            case SyntaxKind.RefTypeExpression:
                break;
            case SyntaxKind.UnsafeExpression:
                break;
            case SyntaxKind.QueryExpression:
                break;
            case SyntaxKind.QueryBody:
                break;
            case SyntaxKind.FromClause:
                break;
            case SyntaxKind.LetClause:
                break;
            case SyntaxKind.JoinClause:
                break;
            case SyntaxKind.JoinIntoClause:
                break;
            case SyntaxKind.WhereClause:
                break;
            case SyntaxKind.OrderByClause:
                break;
            case SyntaxKind.AscendingOrdering:
                break;
            case SyntaxKind.DescendingOrdering:
                break;
            case SyntaxKind.SelectClause:
                break;
            case SyntaxKind.GroupClause:
                break;
            case SyntaxKind.QueryContinuation:
                break;
            case SyntaxKind.Block:
                break;
            case SyntaxKind.LocalDeclarationStatement:
                break;
            case SyntaxKind.VariableDeclaration:
                break;
            case SyntaxKind.VariableDeclarator:
                break;
            case SyntaxKind.EqualsValueClause:
                break;
            case SyntaxKind.ExpressionStatement:
                break;
            case SyntaxKind.EmptyStatement:
                break;
            case SyntaxKind.LabeledStatement:
                break;
            case SyntaxKind.GotoStatement:
                break;
            case SyntaxKind.GotoCaseStatement:
                break;
            case SyntaxKind.GotoDefaultStatement:
                break;
            case SyntaxKind.BreakStatement:
                break;
            case SyntaxKind.ContinueStatement:
                break;
            case SyntaxKind.ReturnStatement:
                break;
            case SyntaxKind.YieldReturnStatement:
                break;
            case SyntaxKind.YieldBreakStatement:
                break;
            case SyntaxKind.ThrowStatement:
                break;
            case SyntaxKind.WhileStatement:
                break;
            case SyntaxKind.DoStatement:
                break;
            case SyntaxKind.ForStatement:
                break;
            case SyntaxKind.ForEachStatement:
                break;
            case SyntaxKind.UsingStatement:
                break;
            case SyntaxKind.FixedStatement:
                break;
            case SyntaxKind.CheckedStatement:
                break;
            case SyntaxKind.UncheckedStatement:
                break;
            case SyntaxKind.UnsafeStatement:
                break;
            case SyntaxKind.LockStatement:
                break;
            case SyntaxKind.IfStatement:
                break;
            case SyntaxKind.ElseClause:
                break;
            case SyntaxKind.SwitchStatement:
                break;
            case SyntaxKind.SwitchSection:
                break;
            case SyntaxKind.CaseSwitchLabel:
                break;
            case SyntaxKind.DefaultSwitchLabel:
                break;
            case SyntaxKind.TryStatement:
                break;
            case SyntaxKind.CatchClause:
                break;
            case SyntaxKind.CatchDeclaration:
                break;
            case SyntaxKind.CatchFilterClause:
                break;
            case SyntaxKind.FinallyClause:
                break;
            case SyntaxKind.LocalFunctionStatement:
                break;
            case SyntaxKind.CompilationUnit:
                break;
            case SyntaxKind.GlobalStatement:
                break;
            case SyntaxKind.NamespaceDeclaration:
                break;
            case SyntaxKind.UsingDirective:
                break;
            case SyntaxKind.ExternAliasDirective:
                break;
            case SyntaxKind.FileScopedNamespaceDeclaration:
                break;
            case SyntaxKind.AttributeList:
                break;
            case SyntaxKind.AttributeTargetSpecifier:
                break;
            case SyntaxKind.Attribute:
                break;
            case SyntaxKind.AttributeArgumentList:
                break;
            case SyntaxKind.AttributeArgument:
                break;
            case SyntaxKind.NameEquals:
                break;
            case SyntaxKind.ClassDeclaration:
                break;
            case SyntaxKind.StructDeclaration:
                break;
            case SyntaxKind.InterfaceDeclaration:
                break;
            case SyntaxKind.EnumDeclaration:
                break;
            case SyntaxKind.DelegateDeclaration:
                break;
            case SyntaxKind.BaseList:
                break;
            case SyntaxKind.SimpleBaseType:
                break;
            case SyntaxKind.TypeParameterConstraintClause:
                break;
            case SyntaxKind.ConstructorConstraint:
                break;
            case SyntaxKind.ClassConstraint:
                break;
            case SyntaxKind.StructConstraint:
                break;
            case SyntaxKind.TypeConstraint:
                break;
            case SyntaxKind.ExplicitInterfaceSpecifier:
                break;
            case SyntaxKind.EnumMemberDeclaration:
                break;
            case SyntaxKind.FieldDeclaration:
                break;
            case SyntaxKind.EventFieldDeclaration:
                break;
            case SyntaxKind.MethodDeclaration:
                break;
            case SyntaxKind.OperatorDeclaration:
                break;
            case SyntaxKind.ConversionOperatorDeclaration:
                break;
            case SyntaxKind.ConstructorDeclaration:
                break;
            case SyntaxKind.AllowsConstraintClause:
                break;
            case SyntaxKind.RefStructConstraint:
                break;
            case SyntaxKind.BaseConstructorInitializer:
                break;
            case SyntaxKind.ThisConstructorInitializer:
                break;
            case SyntaxKind.DestructorDeclaration:
                break;
            case SyntaxKind.PropertyDeclaration:
                break;
            case SyntaxKind.EventDeclaration:
                break;
            case SyntaxKind.IndexerDeclaration:
                break;
            case SyntaxKind.AccessorList:
                break;
            case SyntaxKind.GetAccessorDeclaration:
                break;
            case SyntaxKind.SetAccessorDeclaration:
                break;
            case SyntaxKind.AddAccessorDeclaration:
                break;
            case SyntaxKind.RemoveAccessorDeclaration:
                break;
            case SyntaxKind.UnknownAccessorDeclaration:
                break;
            case SyntaxKind.ParameterList:
                break;
            case SyntaxKind.BracketedParameterList:
                break;
            case SyntaxKind.Parameter:
                break;
            case SyntaxKind.TypeParameterList:
                break;
            case SyntaxKind.TypeParameter:
                break;
            case SyntaxKind.IncompleteMember:
                break;
            case SyntaxKind.ArrowExpressionClause:
                break;
            case SyntaxKind.Interpolation:
                break;
            case SyntaxKind.InterpolatedStringText:
                break;
            case SyntaxKind.InterpolationAlignmentClause:
                break;
            case SyntaxKind.InterpolationFormatClause:
                break;
            case SyntaxKind.ShebangDirectiveTrivia:
                break;
            case SyntaxKind.LoadDirectiveTrivia:
                break;
            case SyntaxKind.TupleType:
                break;
            case SyntaxKind.TupleElement:
                break;
            case SyntaxKind.TupleExpression:
                break;
            case SyntaxKind.SingleVariableDesignation:
                break;
            case SyntaxKind.ParenthesizedVariableDesignation:
                break;
            case SyntaxKind.ForEachVariableStatement:
                break;
            case SyntaxKind.DeclarationPattern:
                break;
            case SyntaxKind.ConstantPattern:
                break;
            case SyntaxKind.CasePatternSwitchLabel:
                break;
            case SyntaxKind.WhenClause:
                break;
            case SyntaxKind.DiscardDesignation:
                break;
            case SyntaxKind.RecursivePattern:
                break;
            case SyntaxKind.PropertyPatternClause:
                break;
            case SyntaxKind.Subpattern:
                break;
            case SyntaxKind.PositionalPatternClause:
                break;
            case SyntaxKind.DiscardPattern:
                break;
            case SyntaxKind.SwitchExpression:
                break;
            case SyntaxKind.SwitchExpressionArm:
                break;
            case SyntaxKind.VarPattern:
                break;
            case SyntaxKind.ParenthesizedPattern:
                break;
            case SyntaxKind.RelationalPattern:
                break;
            case SyntaxKind.TypePattern:
                break;
            case SyntaxKind.OrPattern:
                break;
            case SyntaxKind.AndPattern:
                break;
            case SyntaxKind.NotPattern:
                break;
            case SyntaxKind.SlicePattern:
                break;
            case SyntaxKind.ListPattern:
                break;
            case SyntaxKind.DeclarationExpression:
                break;
            case SyntaxKind.RefExpression:
                break;
            case SyntaxKind.RefType:
                break;
            case SyntaxKind.ThrowExpression:
                break;
            case SyntaxKind.ImplicitStackAllocArrayCreationExpression:
                break;
            case SyntaxKind.SuppressNullableWarningExpression:
                break;
            case SyntaxKind.NullableDirectiveTrivia:
                break;
            case SyntaxKind.FunctionPointerType:
                break;
            case SyntaxKind.FunctionPointerParameter:
                break;
            case SyntaxKind.FunctionPointerParameterList:
                break;
            case SyntaxKind.FunctionPointerCallingConvention:
                break;
            case SyntaxKind.InitAccessorDeclaration:
                break;
            case SyntaxKind.WithExpression:
                break;
            case SyntaxKind.WithInitializerExpression:
                break;
            case SyntaxKind.RecordDeclaration:
                break;
            case SyntaxKind.DefaultConstraint:
                break;
            case SyntaxKind.PrimaryConstructorBaseType:
                break;
            case SyntaxKind.FunctionPointerUnmanagedCallingConventionList:
                break;
            case SyntaxKind.FunctionPointerUnmanagedCallingConvention:
                break;
            case SyntaxKind.RecordStructDeclaration:
                break;
            case SyntaxKind.ExpressionColon:
                break;
            case SyntaxKind.LineDirectivePosition:
                break;
            case SyntaxKind.LineSpanDirectiveTrivia:
                break;
            case SyntaxKind.InterpolatedSingleLineRawStringStartToken:
                break;
            case SyntaxKind.InterpolatedMultiLineRawStringStartToken:
                break;
            case SyntaxKind.InterpolatedRawStringEndToken:
                break;
            case SyntaxKind.ScopedType:
                break;
            case SyntaxKind.CollectionExpression:
                break;
            case SyntaxKind.ExpressionElement:
                break;
            case SyntaxKind.SpreadElement:
                break;
            case SyntaxKind.ExtensionBlockDeclaration:
                break;
            case SyntaxKind.IgnoredDirectiveTrivia:
                break;
            case SyntaxKind.WithElement:
                break;
            case SyntaxKind.UnionDeclaration:
                break;
            default:
                break;
        }

        switch (previous.Kind())
        {
            case SyntaxKind.OpenParenToken:
            case SyntaxKind.OpenBracketToken:
            case SyntaxKind.DotToken:
            case SyntaxKind.ColonColonToken:
                return false;
            case SyntaxKind.LessThanToken when previous.Parent is TypeArgumentListSyntax or TypeParameterListSyntax:
            case SyntaxKind.ExclamationToken or SyntaxKind.MinusToken or SyntaxKind.TildeToken when previous.Parent is PrefixUnaryExpressionSyntax:
                return false;
            case SyntaxKind.CommaToken:
            case SyntaxKind.SemicolonToken:
            case SyntaxKind.ColonToken:
            case SyntaxKind.OpenBraceToken:
            case SyntaxKind.CloseBraceToken:
                return true;
            case SyntaxKind.CloseParenToken:
                return previous.Parent is not CastExpressionSyntax && ( IsWordLike(current) || current.IsKind(SyntaxKind.OpenBraceToken) );
            case SyntaxKind.CloseBracketToken:
            case SyntaxKind.GreaterThanToken:
            case SyntaxKind.ExclamationToken:
                return IsWordLike(current) || current.IsKind(SyntaxKind.OpenBraceToken);
            case SyntaxKind.None:
                break;
            case SyntaxKind.List:
                break;
            case SyntaxKind.DollarToken:
                break;
            case SyntaxKind.PercentToken:
                break;
            case SyntaxKind.CaretToken:
                break;
            case SyntaxKind.AmpersandToken:
                break;
            case SyntaxKind.AsteriskToken:
                break;
            case SyntaxKind.PlusToken:
                break;
            case SyntaxKind.EqualsToken:
                break;
            case SyntaxKind.BarToken:
                break;
            case SyntaxKind.BackslashToken:
                break;
            case SyntaxKind.DoubleQuoteToken:
                break;
            case SyntaxKind.SingleQuoteToken:
                break;
            case SyntaxKind.LessThanToken:
                break;
            case SyntaxKind.QuestionToken:
                break;
            case SyntaxKind.HashToken:
                break;
            case SyntaxKind.SlashToken:
                break;
            case SyntaxKind.DotDotToken:
                break;
            case SyntaxKind.SlashGreaterThanToken:
                break;
            case SyntaxKind.LessThanSlashToken:
                break;
            case SyntaxKind.XmlCommentStartToken:
                break;
            case SyntaxKind.XmlCommentEndToken:
                break;
            case SyntaxKind.XmlCDataStartToken:
                break;
            case SyntaxKind.XmlCDataEndToken:
                break;
            case SyntaxKind.XmlProcessingInstructionStartToken:
                break;
            case SyntaxKind.XmlProcessingInstructionEndToken:
                break;
            case SyntaxKind.BarBarToken:
                break;
            case SyntaxKind.AmpersandAmpersandToken:
                break;
            case SyntaxKind.MinusMinusToken:
                break;
            case SyntaxKind.PlusPlusToken:
                break;
            case SyntaxKind.QuestionQuestionToken:
                break;
            case SyntaxKind.MinusGreaterThanToken:
                break;
            case SyntaxKind.ExclamationEqualsToken:
                break;
            case SyntaxKind.EqualsEqualsToken:
                break;
            case SyntaxKind.EqualsGreaterThanToken:
                break;
            case SyntaxKind.LessThanEqualsToken:
                break;
            case SyntaxKind.LessThanLessThanToken:
                break;
            case SyntaxKind.LessThanLessThanEqualsToken:
                break;
            case SyntaxKind.GreaterThanEqualsToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanEqualsToken:
                break;
            case SyntaxKind.SlashEqualsToken:
                break;
            case SyntaxKind.AsteriskEqualsToken:
                break;
            case SyntaxKind.BarEqualsToken:
                break;
            case SyntaxKind.AmpersandEqualsToken:
                break;
            case SyntaxKind.PlusEqualsToken:
                break;
            case SyntaxKind.MinusEqualsToken:
                break;
            case SyntaxKind.CaretEqualsToken:
                break;
            case SyntaxKind.PercentEqualsToken:
                break;
            case SyntaxKind.QuestionQuestionEqualsToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanGreaterThanToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanGreaterThanEqualsToken:
                break;
            case SyntaxKind.BoolKeyword:
                break;
            case SyntaxKind.ByteKeyword:
                break;
            case SyntaxKind.SByteKeyword:
                break;
            case SyntaxKind.ShortKeyword:
                break;
            case SyntaxKind.UShortKeyword:
                break;
            case SyntaxKind.IntKeyword:
                break;
            case SyntaxKind.UIntKeyword:
                break;
            case SyntaxKind.LongKeyword:
                break;
            case SyntaxKind.ULongKeyword:
                break;
            case SyntaxKind.DoubleKeyword:
                break;
            case SyntaxKind.FloatKeyword:
                break;
            case SyntaxKind.DecimalKeyword:
                break;
            case SyntaxKind.StringKeyword:
                break;
            case SyntaxKind.CharKeyword:
                break;
            case SyntaxKind.VoidKeyword:
                break;
            case SyntaxKind.ObjectKeyword:
                break;
            case SyntaxKind.TypeOfKeyword:
                break;
            case SyntaxKind.SizeOfKeyword:
                break;
            case SyntaxKind.NullKeyword:
                break;
            case SyntaxKind.TrueKeyword:
                break;
            case SyntaxKind.FalseKeyword:
                break;
            case SyntaxKind.IfKeyword:
                break;
            case SyntaxKind.ElseKeyword:
                break;
            case SyntaxKind.WhileKeyword:
                break;
            case SyntaxKind.ForKeyword:
                break;
            case SyntaxKind.ForEachKeyword:
                break;
            case SyntaxKind.DoKeyword:
                break;
            case SyntaxKind.SwitchKeyword:
                break;
            case SyntaxKind.CaseKeyword:
                break;
            case SyntaxKind.DefaultKeyword:
                break;
            case SyntaxKind.TryKeyword:
                break;
            case SyntaxKind.CatchKeyword:
                break;
            case SyntaxKind.FinallyKeyword:
                break;
            case SyntaxKind.LockKeyword:
                break;
            case SyntaxKind.GotoKeyword:
                break;
            case SyntaxKind.BreakKeyword:
                break;
            case SyntaxKind.ContinueKeyword:
                break;
            case SyntaxKind.ReturnKeyword:
                break;
            case SyntaxKind.ThrowKeyword:
                break;
            case SyntaxKind.PublicKeyword:
                break;
            case SyntaxKind.PrivateKeyword:
                break;
            case SyntaxKind.InternalKeyword:
                break;
            case SyntaxKind.ProtectedKeyword:
                break;
            case SyntaxKind.StaticKeyword:
                break;
            case SyntaxKind.ReadOnlyKeyword:
                break;
            case SyntaxKind.SealedKeyword:
                break;
            case SyntaxKind.ConstKeyword:
                break;
            case SyntaxKind.FixedKeyword:
                break;
            case SyntaxKind.StackAllocKeyword:
                break;
            case SyntaxKind.VolatileKeyword:
                break;
            case SyntaxKind.NewKeyword:
                break;
            case SyntaxKind.OverrideKeyword:
                break;
            case SyntaxKind.AbstractKeyword:
                break;
            case SyntaxKind.VirtualKeyword:
                break;
            case SyntaxKind.EventKeyword:
                break;
            case SyntaxKind.ExternKeyword:
                break;
            case SyntaxKind.RefKeyword:
                break;
            case SyntaxKind.OutKeyword:
                break;
            case SyntaxKind.InKeyword:
                break;
            case SyntaxKind.IsKeyword:
                break;
            case SyntaxKind.AsKeyword:
                break;
            case SyntaxKind.ParamsKeyword:
                break;
            case SyntaxKind.ArgListKeyword:
                break;
            case SyntaxKind.MakeRefKeyword:
                break;
            case SyntaxKind.RefTypeKeyword:
                break;
            case SyntaxKind.RefValueKeyword:
                break;
            case SyntaxKind.ThisKeyword:
                break;
            case SyntaxKind.BaseKeyword:
                break;
            case SyntaxKind.NamespaceKeyword:
                break;
            case SyntaxKind.UsingKeyword:
                break;
            case SyntaxKind.ClassKeyword:
                break;
            case SyntaxKind.StructKeyword:
                break;
            case SyntaxKind.InterfaceKeyword:
                break;
            case SyntaxKind.EnumKeyword:
                break;
            case SyntaxKind.DelegateKeyword:
                break;
            case SyntaxKind.CheckedKeyword:
                break;
            case SyntaxKind.UncheckedKeyword:
                break;
            case SyntaxKind.UnsafeKeyword:
                break;
            case SyntaxKind.OperatorKeyword:
                break;
            case SyntaxKind.ExplicitKeyword:
                break;
            case SyntaxKind.ImplicitKeyword:
                break;
            case SyntaxKind.YieldKeyword:
                break;
            case SyntaxKind.PartialKeyword:
                break;
            case SyntaxKind.AliasKeyword:
                break;
            case SyntaxKind.GlobalKeyword:
                break;
            case SyntaxKind.AssemblyKeyword:
                break;
            case SyntaxKind.ModuleKeyword:
                break;
            case SyntaxKind.TypeKeyword:
                break;
            case SyntaxKind.FieldKeyword:
                break;
            case SyntaxKind.MethodKeyword:
                break;
            case SyntaxKind.ParamKeyword:
                break;
            case SyntaxKind.PropertyKeyword:
                break;
            case SyntaxKind.TypeVarKeyword:
                break;
            case SyntaxKind.GetKeyword:
                break;
            case SyntaxKind.SetKeyword:
                break;
            case SyntaxKind.AddKeyword:
                break;
            case SyntaxKind.RemoveKeyword:
                break;
            case SyntaxKind.WhereKeyword:
                break;
            case SyntaxKind.FromKeyword:
                break;
            case SyntaxKind.GroupKeyword:
                break;
            case SyntaxKind.JoinKeyword:
                break;
            case SyntaxKind.IntoKeyword:
                break;
            case SyntaxKind.LetKeyword:
                break;
            case SyntaxKind.ByKeyword:
                break;
            case SyntaxKind.SelectKeyword:
                break;
            case SyntaxKind.OrderByKeyword:
                break;
            case SyntaxKind.OnKeyword:
                break;
            case SyntaxKind.EqualsKeyword:
                break;
            case SyntaxKind.AscendingKeyword:
                break;
            case SyntaxKind.DescendingKeyword:
                break;
            case SyntaxKind.NameOfKeyword:
                break;
            case SyntaxKind.AsyncKeyword:
                break;
            case SyntaxKind.AwaitKeyword:
                break;
            case SyntaxKind.WhenKeyword:
                break;
            case SyntaxKind.OrKeyword:
                break;
            case SyntaxKind.AndKeyword:
                break;
            case SyntaxKind.NotKeyword:
                break;
            case SyntaxKind.WithKeyword:
                break;
            case SyntaxKind.InitKeyword:
                break;
            case SyntaxKind.RecordKeyword:
                break;
            case SyntaxKind.ManagedKeyword:
                break;
            case SyntaxKind.UnmanagedKeyword:
                break;
            case SyntaxKind.RequiredKeyword:
                break;
            case SyntaxKind.ScopedKeyword:
                break;
            case SyntaxKind.FileKeyword:
                break;
            case SyntaxKind.AllowsKeyword:
                break;
            case SyntaxKind.ExtensionKeyword:
                break;
            case SyntaxKind.UnionKeyword:
                break;
            case SyntaxKind.ClosedKeyword:
                break;
            case SyntaxKind.SafeKeyword:
                break;
            case SyntaxKind.ElifKeyword:
                break;
            case SyntaxKind.EndIfKeyword:
                break;
            case SyntaxKind.RegionKeyword:
                break;
            case SyntaxKind.EndRegionKeyword:
                break;
            case SyntaxKind.DefineKeyword:
                break;
            case SyntaxKind.UndefKeyword:
                break;
            case SyntaxKind.WarningKeyword:
                break;
            case SyntaxKind.ErrorKeyword:
                break;
            case SyntaxKind.LineKeyword:
                break;
            case SyntaxKind.PragmaKeyword:
                break;
            case SyntaxKind.HiddenKeyword:
                break;
            case SyntaxKind.ChecksumKeyword:
                break;
            case SyntaxKind.DisableKeyword:
                break;
            case SyntaxKind.RestoreKeyword:
                break;
            case SyntaxKind.ReferenceKeyword:
                break;
            case SyntaxKind.InterpolatedStringStartToken:
                break;
            case SyntaxKind.InterpolatedStringEndToken:
                break;
            case SyntaxKind.InterpolatedVerbatimStringStartToken:
                break;
            case SyntaxKind.LoadKeyword:
                break;
            case SyntaxKind.NullableKeyword:
                break;
            case SyntaxKind.EnableKeyword:
                break;
            case SyntaxKind.WarningsKeyword:
                break;
            case SyntaxKind.AnnotationsKeyword:
                break;
            case SyntaxKind.VarKeyword:
                break;
            case SyntaxKind.UnderscoreToken:
                break;
            case SyntaxKind.OmittedTypeArgumentToken:
                break;
            case SyntaxKind.OmittedArraySizeExpressionToken:
                break;
            case SyntaxKind.EndOfDirectiveToken:
                break;
            case SyntaxKind.EndOfDocumentationCommentToken:
                break;
            case SyntaxKind.EndOfFileToken:
                break;
            case SyntaxKind.BadToken:
                break;
            case SyntaxKind.IdentifierToken:
                break;
            case SyntaxKind.NumericLiteralToken:
                break;
            case SyntaxKind.CharacterLiteralToken:
                break;
            case SyntaxKind.StringLiteralToken:
                break;
            case SyntaxKind.XmlEntityLiteralToken:
                break;
            case SyntaxKind.XmlTextLiteralToken:
                break;
            case SyntaxKind.XmlTextLiteralNewLineToken:
                break;
            case SyntaxKind.InterpolatedStringToken:
                break;
            case SyntaxKind.InterpolatedStringTextToken:
                break;
            case SyntaxKind.SingleLineRawStringLiteralToken:
                break;
            case SyntaxKind.MultiLineRawStringLiteralToken:
                break;
            case SyntaxKind.Utf8StringLiteralToken:
                break;
            case SyntaxKind.Utf8SingleLineRawStringLiteralToken:
                break;
            case SyntaxKind.Utf8MultiLineRawStringLiteralToken:
                break;
            case SyntaxKind.RazorContentToken:
                break;
            case SyntaxKind.EndOfLineTrivia:
                break;
            case SyntaxKind.WhitespaceTrivia:
                break;
            case SyntaxKind.SingleLineCommentTrivia:
                break;
            case SyntaxKind.MultiLineCommentTrivia:
                break;
            case SyntaxKind.DocumentationCommentExteriorTrivia:
                break;
            case SyntaxKind.SingleLineDocumentationCommentTrivia:
                break;
            case SyntaxKind.MultiLineDocumentationCommentTrivia:
                break;
            case SyntaxKind.DisabledTextTrivia:
                break;
            case SyntaxKind.PreprocessingMessageTrivia:
                break;
            case SyntaxKind.IfDirectiveTrivia:
                break;
            case SyntaxKind.ElifDirectiveTrivia:
                break;
            case SyntaxKind.ElseDirectiveTrivia:
                break;
            case SyntaxKind.EndIfDirectiveTrivia:
                break;
            case SyntaxKind.RegionDirectiveTrivia:
                break;
            case SyntaxKind.EndRegionDirectiveTrivia:
                break;
            case SyntaxKind.DefineDirectiveTrivia:
                break;
            case SyntaxKind.UndefDirectiveTrivia:
                break;
            case SyntaxKind.ErrorDirectiveTrivia:
                break;
            case SyntaxKind.WarningDirectiveTrivia:
                break;
            case SyntaxKind.LineDirectiveTrivia:
                break;
            case SyntaxKind.PragmaWarningDirectiveTrivia:
                break;
            case SyntaxKind.PragmaChecksumDirectiveTrivia:
                break;
            case SyntaxKind.ReferenceDirectiveTrivia:
                break;
            case SyntaxKind.BadDirectiveTrivia:
                break;
            case SyntaxKind.SkippedTokensTrivia:
                break;
            case SyntaxKind.ConflictMarkerTrivia:
                break;
            case SyntaxKind.XmlElement:
                break;
            case SyntaxKind.XmlElementStartTag:
                break;
            case SyntaxKind.XmlElementEndTag:
                break;
            case SyntaxKind.XmlEmptyElement:
                break;
            case SyntaxKind.XmlTextAttribute:
                break;
            case SyntaxKind.XmlCrefAttribute:
                break;
            case SyntaxKind.XmlNameAttribute:
                break;
            case SyntaxKind.XmlName:
                break;
            case SyntaxKind.XmlPrefix:
                break;
            case SyntaxKind.XmlText:
                break;
            case SyntaxKind.XmlCDataSection:
                break;
            case SyntaxKind.XmlComment:
                break;
            case SyntaxKind.XmlProcessingInstruction:
                break;
            case SyntaxKind.TypeCref:
                break;
            case SyntaxKind.QualifiedCref:
                break;
            case SyntaxKind.NameMemberCref:
                break;
            case SyntaxKind.IndexerMemberCref:
                break;
            case SyntaxKind.OperatorMemberCref:
                break;
            case SyntaxKind.ConversionOperatorMemberCref:
                break;
            case SyntaxKind.CrefParameterList:
                break;
            case SyntaxKind.CrefBracketedParameterList:
                break;
            case SyntaxKind.CrefParameter:
                break;
            case SyntaxKind.ExtensionMemberCref:
                break;
            case SyntaxKind.IdentifierName:
                break;
            case SyntaxKind.QualifiedName:
                break;
            case SyntaxKind.GenericName:
                break;
            case SyntaxKind.TypeArgumentList:
                break;
            case SyntaxKind.AliasQualifiedName:
                break;
            case SyntaxKind.PredefinedType:
                break;
            case SyntaxKind.ArrayType:
                break;
            case SyntaxKind.ArrayRankSpecifier:
                break;
            case SyntaxKind.PointerType:
                break;
            case SyntaxKind.NullableType:
                break;
            case SyntaxKind.OmittedTypeArgument:
                break;
            case SyntaxKind.ParenthesizedExpression:
                break;
            case SyntaxKind.ConditionalExpression:
                break;
            case SyntaxKind.InvocationExpression:
                break;
            case SyntaxKind.ElementAccessExpression:
                break;
            case SyntaxKind.ArgumentList:
                break;
            case SyntaxKind.BracketedArgumentList:
                break;
            case SyntaxKind.Argument:
                break;
            case SyntaxKind.NameColon:
                break;
            case SyntaxKind.CastExpression:
                break;
            case SyntaxKind.AnonymousMethodExpression:
                break;
            case SyntaxKind.SimpleLambdaExpression:
                break;
            case SyntaxKind.ParenthesizedLambdaExpression:
                break;
            case SyntaxKind.ObjectInitializerExpression:
                break;
            case SyntaxKind.CollectionInitializerExpression:
                break;
            case SyntaxKind.ArrayInitializerExpression:
                break;
            case SyntaxKind.AnonymousObjectMemberDeclarator:
                break;
            case SyntaxKind.ComplexElementInitializerExpression:
                break;
            case SyntaxKind.ObjectCreationExpression:
                break;
            case SyntaxKind.AnonymousObjectCreationExpression:
                break;
            case SyntaxKind.ArrayCreationExpression:
                break;
            case SyntaxKind.ImplicitArrayCreationExpression:
                break;
            case SyntaxKind.StackAllocArrayCreationExpression:
                break;
            case SyntaxKind.OmittedArraySizeExpression:
                break;
            case SyntaxKind.InterpolatedStringExpression:
                break;
            case SyntaxKind.ImplicitElementAccess:
                break;
            case SyntaxKind.IsPatternExpression:
                break;
            case SyntaxKind.RangeExpression:
                break;
            case SyntaxKind.ImplicitObjectCreationExpression:
                break;
            case SyntaxKind.AddExpression:
                break;
            case SyntaxKind.SubtractExpression:
                break;
            case SyntaxKind.MultiplyExpression:
                break;
            case SyntaxKind.DivideExpression:
                break;
            case SyntaxKind.ModuloExpression:
                break;
            case SyntaxKind.LeftShiftExpression:
                break;
            case SyntaxKind.RightShiftExpression:
                break;
            case SyntaxKind.LogicalOrExpression:
                break;
            case SyntaxKind.LogicalAndExpression:
                break;
            case SyntaxKind.BitwiseOrExpression:
                break;
            case SyntaxKind.BitwiseAndExpression:
                break;
            case SyntaxKind.ExclusiveOrExpression:
                break;
            case SyntaxKind.EqualsExpression:
                break;
            case SyntaxKind.NotEqualsExpression:
                break;
            case SyntaxKind.LessThanExpression:
                break;
            case SyntaxKind.LessThanOrEqualExpression:
                break;
            case SyntaxKind.GreaterThanExpression:
                break;
            case SyntaxKind.GreaterThanOrEqualExpression:
                break;
            case SyntaxKind.IsExpression:
                break;
            case SyntaxKind.AsExpression:
                break;
            case SyntaxKind.CoalesceExpression:
                break;
            case SyntaxKind.SimpleMemberAccessExpression:
                break;
            case SyntaxKind.PointerMemberAccessExpression:
                break;
            case SyntaxKind.ConditionalAccessExpression:
                break;
            case SyntaxKind.UnsignedRightShiftExpression:
                break;
            case SyntaxKind.MemberBindingExpression:
                break;
            case SyntaxKind.ElementBindingExpression:
                break;
            case SyntaxKind.SimpleAssignmentExpression:
                break;
            case SyntaxKind.AddAssignmentExpression:
                break;
            case SyntaxKind.SubtractAssignmentExpression:
                break;
            case SyntaxKind.MultiplyAssignmentExpression:
                break;
            case SyntaxKind.DivideAssignmentExpression:
                break;
            case SyntaxKind.ModuloAssignmentExpression:
                break;
            case SyntaxKind.AndAssignmentExpression:
                break;
            case SyntaxKind.ExclusiveOrAssignmentExpression:
                break;
            case SyntaxKind.OrAssignmentExpression:
                break;
            case SyntaxKind.LeftShiftAssignmentExpression:
                break;
            case SyntaxKind.RightShiftAssignmentExpression:
                break;
            case SyntaxKind.CoalesceAssignmentExpression:
                break;
            case SyntaxKind.UnsignedRightShiftAssignmentExpression:
                break;
            case SyntaxKind.UnaryPlusExpression:
                break;
            case SyntaxKind.UnaryMinusExpression:
                break;
            case SyntaxKind.BitwiseNotExpression:
                break;
            case SyntaxKind.LogicalNotExpression:
                break;
            case SyntaxKind.PreIncrementExpression:
                break;
            case SyntaxKind.PreDecrementExpression:
                break;
            case SyntaxKind.PointerIndirectionExpression:
                break;
            case SyntaxKind.AddressOfExpression:
                break;
            case SyntaxKind.PostIncrementExpression:
                break;
            case SyntaxKind.PostDecrementExpression:
                break;
            case SyntaxKind.AwaitExpression:
                break;
            case SyntaxKind.IndexExpression:
                break;
            case SyntaxKind.ThisExpression:
                break;
            case SyntaxKind.BaseExpression:
                break;
            case SyntaxKind.ArgListExpression:
                break;
            case SyntaxKind.NumericLiteralExpression:
                break;
            case SyntaxKind.StringLiteralExpression:
                break;
            case SyntaxKind.CharacterLiteralExpression:
                break;
            case SyntaxKind.TrueLiteralExpression:
                break;
            case SyntaxKind.FalseLiteralExpression:
                break;
            case SyntaxKind.NullLiteralExpression:
                break;
            case SyntaxKind.DefaultLiteralExpression:
                break;
            case SyntaxKind.Utf8StringLiteralExpression:
                break;
            case SyntaxKind.FieldExpression:
                break;
            case SyntaxKind.TypeOfExpression:
                break;
            case SyntaxKind.SizeOfExpression:
                break;
            case SyntaxKind.CheckedExpression:
                break;
            case SyntaxKind.UncheckedExpression:
                break;
            case SyntaxKind.DefaultExpression:
                break;
            case SyntaxKind.MakeRefExpression:
                break;
            case SyntaxKind.RefValueExpression:
                break;
            case SyntaxKind.RefTypeExpression:
                break;
            case SyntaxKind.UnsafeExpression:
                break;
            case SyntaxKind.QueryExpression:
                break;
            case SyntaxKind.QueryBody:
                break;
            case SyntaxKind.FromClause:
                break;
            case SyntaxKind.LetClause:
                break;
            case SyntaxKind.JoinClause:
                break;
            case SyntaxKind.JoinIntoClause:
                break;
            case SyntaxKind.WhereClause:
                break;
            case SyntaxKind.OrderByClause:
                break;
            case SyntaxKind.AscendingOrdering:
                break;
            case SyntaxKind.DescendingOrdering:
                break;
            case SyntaxKind.SelectClause:
                break;
            case SyntaxKind.GroupClause:
                break;
            case SyntaxKind.QueryContinuation:
                break;
            case SyntaxKind.Block:
                break;
            case SyntaxKind.LocalDeclarationStatement:
                break;
            case SyntaxKind.VariableDeclaration:
                break;
            case SyntaxKind.VariableDeclarator:
                break;
            case SyntaxKind.EqualsValueClause:
                break;
            case SyntaxKind.ExpressionStatement:
                break;
            case SyntaxKind.EmptyStatement:
                break;
            case SyntaxKind.LabeledStatement:
                break;
            case SyntaxKind.GotoStatement:
                break;
            case SyntaxKind.GotoCaseStatement:
                break;
            case SyntaxKind.GotoDefaultStatement:
                break;
            case SyntaxKind.BreakStatement:
                break;
            case SyntaxKind.ContinueStatement:
                break;
            case SyntaxKind.ReturnStatement:
                break;
            case SyntaxKind.YieldReturnStatement:
                break;
            case SyntaxKind.YieldBreakStatement:
                break;
            case SyntaxKind.ThrowStatement:
                break;
            case SyntaxKind.WhileStatement:
                break;
            case SyntaxKind.DoStatement:
                break;
            case SyntaxKind.ForStatement:
                break;
            case SyntaxKind.ForEachStatement:
                break;
            case SyntaxKind.UsingStatement:
                break;
            case SyntaxKind.FixedStatement:
                break;
            case SyntaxKind.CheckedStatement:
                break;
            case SyntaxKind.UncheckedStatement:
                break;
            case SyntaxKind.UnsafeStatement:
                break;
            case SyntaxKind.LockStatement:
                break;
            case SyntaxKind.IfStatement:
                break;
            case SyntaxKind.ElseClause:
                break;
            case SyntaxKind.SwitchStatement:
                break;
            case SyntaxKind.SwitchSection:
                break;
            case SyntaxKind.CaseSwitchLabel:
                break;
            case SyntaxKind.DefaultSwitchLabel:
                break;
            case SyntaxKind.TryStatement:
                break;
            case SyntaxKind.CatchClause:
                break;
            case SyntaxKind.CatchDeclaration:
                break;
            case SyntaxKind.CatchFilterClause:
                break;
            case SyntaxKind.FinallyClause:
                break;
            case SyntaxKind.LocalFunctionStatement:
                break;
            case SyntaxKind.CompilationUnit:
                break;
            case SyntaxKind.GlobalStatement:
                break;
            case SyntaxKind.NamespaceDeclaration:
                break;
            case SyntaxKind.UsingDirective:
                break;
            case SyntaxKind.ExternAliasDirective:
                break;
            case SyntaxKind.FileScopedNamespaceDeclaration:
                break;
            case SyntaxKind.AttributeList:
                break;
            case SyntaxKind.AttributeTargetSpecifier:
                break;
            case SyntaxKind.Attribute:
                break;
            case SyntaxKind.AttributeArgumentList:
                break;
            case SyntaxKind.AttributeArgument:
                break;
            case SyntaxKind.NameEquals:
                break;
            case SyntaxKind.ClassDeclaration:
                break;
            case SyntaxKind.StructDeclaration:
                break;
            case SyntaxKind.InterfaceDeclaration:
                break;
            case SyntaxKind.EnumDeclaration:
                break;
            case SyntaxKind.DelegateDeclaration:
                break;
            case SyntaxKind.BaseList:
                break;
            case SyntaxKind.SimpleBaseType:
                break;
            case SyntaxKind.TypeParameterConstraintClause:
                break;
            case SyntaxKind.ConstructorConstraint:
                break;
            case SyntaxKind.ClassConstraint:
                break;
            case SyntaxKind.StructConstraint:
                break;
            case SyntaxKind.TypeConstraint:
                break;
            case SyntaxKind.ExplicitInterfaceSpecifier:
                break;
            case SyntaxKind.EnumMemberDeclaration:
                break;
            case SyntaxKind.FieldDeclaration:
                break;
            case SyntaxKind.EventFieldDeclaration:
                break;
            case SyntaxKind.MethodDeclaration:
                break;
            case SyntaxKind.OperatorDeclaration:
                break;
            case SyntaxKind.ConversionOperatorDeclaration:
                break;
            case SyntaxKind.ConstructorDeclaration:
                break;
            case SyntaxKind.AllowsConstraintClause:
                break;
            case SyntaxKind.RefStructConstraint:
                break;
            case SyntaxKind.BaseConstructorInitializer:
                break;
            case SyntaxKind.ThisConstructorInitializer:
                break;
            case SyntaxKind.DestructorDeclaration:
                break;
            case SyntaxKind.PropertyDeclaration:
                break;
            case SyntaxKind.EventDeclaration:
                break;
            case SyntaxKind.IndexerDeclaration:
                break;
            case SyntaxKind.AccessorList:
                break;
            case SyntaxKind.GetAccessorDeclaration:
                break;
            case SyntaxKind.SetAccessorDeclaration:
                break;
            case SyntaxKind.AddAccessorDeclaration:
                break;
            case SyntaxKind.RemoveAccessorDeclaration:
                break;
            case SyntaxKind.UnknownAccessorDeclaration:
                break;
            case SyntaxKind.ParameterList:
                break;
            case SyntaxKind.BracketedParameterList:
                break;
            case SyntaxKind.Parameter:
                break;
            case SyntaxKind.TypeParameterList:
                break;
            case SyntaxKind.TypeParameter:
                break;
            case SyntaxKind.IncompleteMember:
                break;
            case SyntaxKind.ArrowExpressionClause:
                break;
            case SyntaxKind.Interpolation:
                break;
            case SyntaxKind.InterpolatedStringText:
                break;
            case SyntaxKind.InterpolationAlignmentClause:
                break;
            case SyntaxKind.InterpolationFormatClause:
                break;
            case SyntaxKind.ShebangDirectiveTrivia:
                break;
            case SyntaxKind.LoadDirectiveTrivia:
                break;
            case SyntaxKind.TupleType:
                break;
            case SyntaxKind.TupleElement:
                break;
            case SyntaxKind.TupleExpression:
                break;
            case SyntaxKind.SingleVariableDesignation:
                break;
            case SyntaxKind.ParenthesizedVariableDesignation:
                break;
            case SyntaxKind.ForEachVariableStatement:
                break;
            case SyntaxKind.DeclarationPattern:
                break;
            case SyntaxKind.ConstantPattern:
                break;
            case SyntaxKind.CasePatternSwitchLabel:
                break;
            case SyntaxKind.WhenClause:
                break;
            case SyntaxKind.DiscardDesignation:
                break;
            case SyntaxKind.RecursivePattern:
                break;
            case SyntaxKind.PropertyPatternClause:
                break;
            case SyntaxKind.Subpattern:
                break;
            case SyntaxKind.PositionalPatternClause:
                break;
            case SyntaxKind.DiscardPattern:
                break;
            case SyntaxKind.SwitchExpression:
                break;
            case SyntaxKind.SwitchExpressionArm:
                break;
            case SyntaxKind.VarPattern:
                break;
            case SyntaxKind.ParenthesizedPattern:
                break;
            case SyntaxKind.RelationalPattern:
                break;
            case SyntaxKind.TypePattern:
                break;
            case SyntaxKind.OrPattern:
                break;
            case SyntaxKind.AndPattern:
                break;
            case SyntaxKind.NotPattern:
                break;
            case SyntaxKind.SlicePattern:
                break;
            case SyntaxKind.ListPattern:
                break;
            case SyntaxKind.DeclarationExpression:
                break;
            case SyntaxKind.RefExpression:
                break;
            case SyntaxKind.RefType:
                break;
            case SyntaxKind.ThrowExpression:
                break;
            case SyntaxKind.ImplicitStackAllocArrayCreationExpression:
                break;
            case SyntaxKind.SuppressNullableWarningExpression:
                break;
            case SyntaxKind.NullableDirectiveTrivia:
                break;
            case SyntaxKind.FunctionPointerType:
                break;
            case SyntaxKind.FunctionPointerParameter:
                break;
            case SyntaxKind.FunctionPointerParameterList:
                break;
            case SyntaxKind.FunctionPointerCallingConvention:
                break;
            case SyntaxKind.InitAccessorDeclaration:
                break;
            case SyntaxKind.WithExpression:
                break;
            case SyntaxKind.WithInitializerExpression:
                break;
            case SyntaxKind.RecordDeclaration:
                break;
            case SyntaxKind.DefaultConstraint:
                break;
            case SyntaxKind.PrimaryConstructorBaseType:
                break;
            case SyntaxKind.FunctionPointerUnmanagedCallingConventionList:
                break;
            case SyntaxKind.FunctionPointerUnmanagedCallingConvention:
                break;
            case SyntaxKind.RecordStructDeclaration:
                break;
            case SyntaxKind.ExpressionColon:
                break;
            case SyntaxKind.LineDirectivePosition:
                break;
            case SyntaxKind.LineSpanDirectiveTrivia:
                break;
            case SyntaxKind.InterpolatedSingleLineRawStringStartToken:
                break;
            case SyntaxKind.InterpolatedMultiLineRawStringStartToken:
                break;
            case SyntaxKind.InterpolatedRawStringEndToken:
                break;
            case SyntaxKind.ScopedType:
                break;
            case SyntaxKind.CollectionExpression:
                break;
            case SyntaxKind.ExpressionElement:
                break;
            case SyntaxKind.SpreadElement:
                break;
            case SyntaxKind.ExtensionBlockDeclaration:
                break;
            case SyntaxKind.IgnoredDirectiveTrivia:
                break;
            case SyntaxKind.WithElement:
                break;
            case SyntaxKind.UnionDeclaration:
                break;
            default:
                break;
        }

        switch (current.Kind())
        {
            case SyntaxKind.OpenParenToken:
                return IsKeyword(previous) && !IsInvokedLikeAFunction(previous);
            case SyntaxKind.OpenBracketToken:
                return previous.IsKind(SyntaxKind.ReturnKeyword) || previous.IsKind(SyntaxKind.InKeyword);
            case SyntaxKind.None:
                break;
            case SyntaxKind.List:
                break;
            case SyntaxKind.TildeToken:
                break;
            case SyntaxKind.ExclamationToken:
                break;
            case SyntaxKind.DollarToken:
                break;
            case SyntaxKind.PercentToken:
                break;
            case SyntaxKind.CaretToken:
                break;
            case SyntaxKind.AmpersandToken:
                break;
            case SyntaxKind.AsteriskToken:
                break;
            case SyntaxKind.CloseParenToken:
                break;
            case SyntaxKind.MinusToken:
                break;
            case SyntaxKind.PlusToken:
                break;
            case SyntaxKind.EqualsToken:
                break;
            case SyntaxKind.OpenBraceToken:
                break;
            case SyntaxKind.CloseBraceToken:
                break;
            case SyntaxKind.CloseBracketToken:
                break;
            case SyntaxKind.BarToken:
                break;
            case SyntaxKind.BackslashToken:
                break;
            case SyntaxKind.ColonToken:
                break;
            case SyntaxKind.SemicolonToken:
                break;
            case SyntaxKind.DoubleQuoteToken:
                break;
            case SyntaxKind.SingleQuoteToken:
                break;
            case SyntaxKind.LessThanToken:
                break;
            case SyntaxKind.CommaToken:
                break;
            case SyntaxKind.GreaterThanToken:
                break;
            case SyntaxKind.DotToken:
                break;
            case SyntaxKind.QuestionToken:
                break;
            case SyntaxKind.HashToken:
                break;
            case SyntaxKind.SlashToken:
                break;
            case SyntaxKind.DotDotToken:
                break;
            case SyntaxKind.SlashGreaterThanToken:
                break;
            case SyntaxKind.LessThanSlashToken:
                break;
            case SyntaxKind.XmlCommentStartToken:
                break;
            case SyntaxKind.XmlCommentEndToken:
                break;
            case SyntaxKind.XmlCDataStartToken:
                break;
            case SyntaxKind.XmlCDataEndToken:
                break;
            case SyntaxKind.XmlProcessingInstructionStartToken:
                break;
            case SyntaxKind.XmlProcessingInstructionEndToken:
                break;
            case SyntaxKind.BarBarToken:
                break;
            case SyntaxKind.AmpersandAmpersandToken:
                break;
            case SyntaxKind.MinusMinusToken:
                break;
            case SyntaxKind.PlusPlusToken:
                break;
            case SyntaxKind.ColonColonToken:
                break;
            case SyntaxKind.QuestionQuestionToken:
                break;
            case SyntaxKind.MinusGreaterThanToken:
                break;
            case SyntaxKind.ExclamationEqualsToken:
                break;
            case SyntaxKind.EqualsEqualsToken:
                break;
            case SyntaxKind.EqualsGreaterThanToken:
                break;
            case SyntaxKind.LessThanEqualsToken:
                break;
            case SyntaxKind.LessThanLessThanToken:
                break;
            case SyntaxKind.LessThanLessThanEqualsToken:
                break;
            case SyntaxKind.GreaterThanEqualsToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanEqualsToken:
                break;
            case SyntaxKind.SlashEqualsToken:
                break;
            case SyntaxKind.AsteriskEqualsToken:
                break;
            case SyntaxKind.BarEqualsToken:
                break;
            case SyntaxKind.AmpersandEqualsToken:
                break;
            case SyntaxKind.PlusEqualsToken:
                break;
            case SyntaxKind.MinusEqualsToken:
                break;
            case SyntaxKind.CaretEqualsToken:
                break;
            case SyntaxKind.PercentEqualsToken:
                break;
            case SyntaxKind.QuestionQuestionEqualsToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanGreaterThanToken:
                break;
            case SyntaxKind.GreaterThanGreaterThanGreaterThanEqualsToken:
                break;
            case SyntaxKind.BoolKeyword:
                break;
            case SyntaxKind.ByteKeyword:
                break;
            case SyntaxKind.SByteKeyword:
                break;
            case SyntaxKind.ShortKeyword:
                break;
            case SyntaxKind.UShortKeyword:
                break;
            case SyntaxKind.IntKeyword:
                break;
            case SyntaxKind.UIntKeyword:
                break;
            case SyntaxKind.LongKeyword:
                break;
            case SyntaxKind.ULongKeyword:
                break;
            case SyntaxKind.DoubleKeyword:
                break;
            case SyntaxKind.FloatKeyword:
                break;
            case SyntaxKind.DecimalKeyword:
                break;
            case SyntaxKind.StringKeyword:
                break;
            case SyntaxKind.CharKeyword:
                break;
            case SyntaxKind.VoidKeyword:
                break;
            case SyntaxKind.ObjectKeyword:
                break;
            case SyntaxKind.TypeOfKeyword:
                break;
            case SyntaxKind.SizeOfKeyword:
                break;
            case SyntaxKind.NullKeyword:
                break;
            case SyntaxKind.TrueKeyword:
                break;
            case SyntaxKind.FalseKeyword:
                break;
            case SyntaxKind.IfKeyword:
                break;
            case SyntaxKind.ElseKeyword:
                break;
            case SyntaxKind.WhileKeyword:
                break;
            case SyntaxKind.ForKeyword:
                break;
            case SyntaxKind.ForEachKeyword:
                break;
            case SyntaxKind.DoKeyword:
                break;
            case SyntaxKind.SwitchKeyword:
                break;
            case SyntaxKind.CaseKeyword:
                break;
            case SyntaxKind.DefaultKeyword:
                break;
            case SyntaxKind.TryKeyword:
                break;
            case SyntaxKind.CatchKeyword:
                break;
            case SyntaxKind.FinallyKeyword:
                break;
            case SyntaxKind.LockKeyword:
                break;
            case SyntaxKind.GotoKeyword:
                break;
            case SyntaxKind.BreakKeyword:
                break;
            case SyntaxKind.ContinueKeyword:
                break;
            case SyntaxKind.ReturnKeyword:
                break;
            case SyntaxKind.ThrowKeyword:
                break;
            case SyntaxKind.PublicKeyword:
                break;
            case SyntaxKind.PrivateKeyword:
                break;
            case SyntaxKind.InternalKeyword:
                break;
            case SyntaxKind.ProtectedKeyword:
                break;
            case SyntaxKind.StaticKeyword:
                break;
            case SyntaxKind.ReadOnlyKeyword:
                break;
            case SyntaxKind.SealedKeyword:
                break;
            case SyntaxKind.ConstKeyword:
                break;
            case SyntaxKind.FixedKeyword:
                break;
            case SyntaxKind.StackAllocKeyword:
                break;
            case SyntaxKind.VolatileKeyword:
                break;
            case SyntaxKind.NewKeyword:
                break;
            case SyntaxKind.OverrideKeyword:
                break;
            case SyntaxKind.AbstractKeyword:
                break;
            case SyntaxKind.VirtualKeyword:
                break;
            case SyntaxKind.EventKeyword:
                break;
            case SyntaxKind.ExternKeyword:
                break;
            case SyntaxKind.RefKeyword:
                break;
            case SyntaxKind.OutKeyword:
                break;
            case SyntaxKind.InKeyword:
                break;
            case SyntaxKind.IsKeyword:
                break;
            case SyntaxKind.AsKeyword:
                break;
            case SyntaxKind.ParamsKeyword:
                break;
            case SyntaxKind.ArgListKeyword:
                break;
            case SyntaxKind.MakeRefKeyword:
                break;
            case SyntaxKind.RefTypeKeyword:
                break;
            case SyntaxKind.RefValueKeyword:
                break;
            case SyntaxKind.ThisKeyword:
                break;
            case SyntaxKind.BaseKeyword:
                break;
            case SyntaxKind.NamespaceKeyword:
                break;
            case SyntaxKind.UsingKeyword:
                break;
            case SyntaxKind.ClassKeyword:
                break;
            case SyntaxKind.StructKeyword:
                break;
            case SyntaxKind.InterfaceKeyword:
                break;
            case SyntaxKind.EnumKeyword:
                break;
            case SyntaxKind.DelegateKeyword:
                break;
            case SyntaxKind.CheckedKeyword:
                break;
            case SyntaxKind.UncheckedKeyword:
                break;
            case SyntaxKind.UnsafeKeyword:
                break;
            case SyntaxKind.OperatorKeyword:
                break;
            case SyntaxKind.ExplicitKeyword:
                break;
            case SyntaxKind.ImplicitKeyword:
                break;
            case SyntaxKind.YieldKeyword:
                break;
            case SyntaxKind.PartialKeyword:
                break;
            case SyntaxKind.AliasKeyword:
                break;
            case SyntaxKind.GlobalKeyword:
                break;
            case SyntaxKind.AssemblyKeyword:
                break;
            case SyntaxKind.ModuleKeyword:
                break;
            case SyntaxKind.TypeKeyword:
                break;
            case SyntaxKind.FieldKeyword:
                break;
            case SyntaxKind.MethodKeyword:
                break;
            case SyntaxKind.ParamKeyword:
                break;
            case SyntaxKind.PropertyKeyword:
                break;
            case SyntaxKind.TypeVarKeyword:
                break;
            case SyntaxKind.GetKeyword:
                break;
            case SyntaxKind.SetKeyword:
                break;
            case SyntaxKind.AddKeyword:
                break;
            case SyntaxKind.RemoveKeyword:
                break;
            case SyntaxKind.WhereKeyword:
                break;
            case SyntaxKind.FromKeyword:
                break;
            case SyntaxKind.GroupKeyword:
                break;
            case SyntaxKind.JoinKeyword:
                break;
            case SyntaxKind.IntoKeyword:
                break;
            case SyntaxKind.LetKeyword:
                break;
            case SyntaxKind.ByKeyword:
                break;
            case SyntaxKind.SelectKeyword:
                break;
            case SyntaxKind.OrderByKeyword:
                break;
            case SyntaxKind.OnKeyword:
                break;
            case SyntaxKind.EqualsKeyword:
                break;
            case SyntaxKind.AscendingKeyword:
                break;
            case SyntaxKind.DescendingKeyword:
                break;
            case SyntaxKind.NameOfKeyword:
                break;
            case SyntaxKind.AsyncKeyword:
                break;
            case SyntaxKind.AwaitKeyword:
                break;
            case SyntaxKind.WhenKeyword:
                break;
            case SyntaxKind.OrKeyword:
                break;
            case SyntaxKind.AndKeyword:
                break;
            case SyntaxKind.NotKeyword:
                break;
            case SyntaxKind.WithKeyword:
                break;
            case SyntaxKind.InitKeyword:
                break;
            case SyntaxKind.RecordKeyword:
                break;
            case SyntaxKind.ManagedKeyword:
                break;
            case SyntaxKind.UnmanagedKeyword:
                break;
            case SyntaxKind.RequiredKeyword:
                break;
            case SyntaxKind.ScopedKeyword:
                break;
            case SyntaxKind.FileKeyword:
                break;
            case SyntaxKind.AllowsKeyword:
                break;
            case SyntaxKind.ExtensionKeyword:
                break;
            case SyntaxKind.UnionKeyword:
                break;
            case SyntaxKind.ClosedKeyword:
                break;
            case SyntaxKind.SafeKeyword:
                break;
            case SyntaxKind.ElifKeyword:
                break;
            case SyntaxKind.EndIfKeyword:
                break;
            case SyntaxKind.RegionKeyword:
                break;
            case SyntaxKind.EndRegionKeyword:
                break;
            case SyntaxKind.DefineKeyword:
                break;
            case SyntaxKind.UndefKeyword:
                break;
            case SyntaxKind.WarningKeyword:
                break;
            case SyntaxKind.ErrorKeyword:
                break;
            case SyntaxKind.LineKeyword:
                break;
            case SyntaxKind.PragmaKeyword:
                break;
            case SyntaxKind.HiddenKeyword:
                break;
            case SyntaxKind.ChecksumKeyword:
                break;
            case SyntaxKind.DisableKeyword:
                break;
            case SyntaxKind.RestoreKeyword:
                break;
            case SyntaxKind.ReferenceKeyword:
                break;
            case SyntaxKind.InterpolatedStringStartToken:
                break;
            case SyntaxKind.InterpolatedStringEndToken:
                break;
            case SyntaxKind.InterpolatedVerbatimStringStartToken:
                break;
            case SyntaxKind.LoadKeyword:
                break;
            case SyntaxKind.NullableKeyword:
                break;
            case SyntaxKind.EnableKeyword:
                break;
            case SyntaxKind.WarningsKeyword:
                break;
            case SyntaxKind.AnnotationsKeyword:
                break;
            case SyntaxKind.VarKeyword:
                break;
            case SyntaxKind.UnderscoreToken:
                break;
            case SyntaxKind.OmittedTypeArgumentToken:
                break;
            case SyntaxKind.OmittedArraySizeExpressionToken:
                break;
            case SyntaxKind.EndOfDirectiveToken:
                break;
            case SyntaxKind.EndOfDocumentationCommentToken:
                break;
            case SyntaxKind.EndOfFileToken:
                break;
            case SyntaxKind.BadToken:
                break;
            case SyntaxKind.IdentifierToken:
                break;
            case SyntaxKind.NumericLiteralToken:
                break;
            case SyntaxKind.CharacterLiteralToken:
                break;
            case SyntaxKind.StringLiteralToken:
                break;
            case SyntaxKind.XmlEntityLiteralToken:
                break;
            case SyntaxKind.XmlTextLiteralToken:
                break;
            case SyntaxKind.XmlTextLiteralNewLineToken:
                break;
            case SyntaxKind.InterpolatedStringToken:
                break;
            case SyntaxKind.InterpolatedStringTextToken:
                break;
            case SyntaxKind.SingleLineRawStringLiteralToken:
                break;
            case SyntaxKind.MultiLineRawStringLiteralToken:
                break;
            case SyntaxKind.Utf8StringLiteralToken:
                break;
            case SyntaxKind.Utf8SingleLineRawStringLiteralToken:
                break;
            case SyntaxKind.Utf8MultiLineRawStringLiteralToken:
                break;
            case SyntaxKind.RazorContentToken:
                break;
            case SyntaxKind.EndOfLineTrivia:
                break;
            case SyntaxKind.WhitespaceTrivia:
                break;
            case SyntaxKind.SingleLineCommentTrivia:
                break;
            case SyntaxKind.MultiLineCommentTrivia:
                break;
            case SyntaxKind.DocumentationCommentExteriorTrivia:
                break;
            case SyntaxKind.SingleLineDocumentationCommentTrivia:
                break;
            case SyntaxKind.MultiLineDocumentationCommentTrivia:
                break;
            case SyntaxKind.DisabledTextTrivia:
                break;
            case SyntaxKind.PreprocessingMessageTrivia:
                break;
            case SyntaxKind.IfDirectiveTrivia:
                break;
            case SyntaxKind.ElifDirectiveTrivia:
                break;
            case SyntaxKind.ElseDirectiveTrivia:
                break;
            case SyntaxKind.EndIfDirectiveTrivia:
                break;
            case SyntaxKind.RegionDirectiveTrivia:
                break;
            case SyntaxKind.EndRegionDirectiveTrivia:
                break;
            case SyntaxKind.DefineDirectiveTrivia:
                break;
            case SyntaxKind.UndefDirectiveTrivia:
                break;
            case SyntaxKind.ErrorDirectiveTrivia:
                break;
            case SyntaxKind.WarningDirectiveTrivia:
                break;
            case SyntaxKind.LineDirectiveTrivia:
                break;
            case SyntaxKind.PragmaWarningDirectiveTrivia:
                break;
            case SyntaxKind.PragmaChecksumDirectiveTrivia:
                break;
            case SyntaxKind.ReferenceDirectiveTrivia:
                break;
            case SyntaxKind.BadDirectiveTrivia:
                break;
            case SyntaxKind.SkippedTokensTrivia:
                break;
            case SyntaxKind.ConflictMarkerTrivia:
                break;
            case SyntaxKind.XmlElement:
                break;
            case SyntaxKind.XmlElementStartTag:
                break;
            case SyntaxKind.XmlElementEndTag:
                break;
            case SyntaxKind.XmlEmptyElement:
                break;
            case SyntaxKind.XmlTextAttribute:
                break;
            case SyntaxKind.XmlCrefAttribute:
                break;
            case SyntaxKind.XmlNameAttribute:
                break;
            case SyntaxKind.XmlName:
                break;
            case SyntaxKind.XmlPrefix:
                break;
            case SyntaxKind.XmlText:
                break;
            case SyntaxKind.XmlCDataSection:
                break;
            case SyntaxKind.XmlComment:
                break;
            case SyntaxKind.XmlProcessingInstruction:
                break;
            case SyntaxKind.TypeCref:
                break;
            case SyntaxKind.QualifiedCref:
                break;
            case SyntaxKind.NameMemberCref:
                break;
            case SyntaxKind.IndexerMemberCref:
                break;
            case SyntaxKind.OperatorMemberCref:
                break;
            case SyntaxKind.ConversionOperatorMemberCref:
                break;
            case SyntaxKind.CrefParameterList:
                break;
            case SyntaxKind.CrefBracketedParameterList:
                break;
            case SyntaxKind.CrefParameter:
                break;
            case SyntaxKind.ExtensionMemberCref:
                break;
            case SyntaxKind.IdentifierName:
                break;
            case SyntaxKind.QualifiedName:
                break;
            case SyntaxKind.GenericName:
                break;
            case SyntaxKind.TypeArgumentList:
                break;
            case SyntaxKind.AliasQualifiedName:
                break;
            case SyntaxKind.PredefinedType:
                break;
            case SyntaxKind.ArrayType:
                break;
            case SyntaxKind.ArrayRankSpecifier:
                break;
            case SyntaxKind.PointerType:
                break;
            case SyntaxKind.NullableType:
                break;
            case SyntaxKind.OmittedTypeArgument:
                break;
            case SyntaxKind.ParenthesizedExpression:
                break;
            case SyntaxKind.ConditionalExpression:
                break;
            case SyntaxKind.InvocationExpression:
                break;
            case SyntaxKind.ElementAccessExpression:
                break;
            case SyntaxKind.ArgumentList:
                break;
            case SyntaxKind.BracketedArgumentList:
                break;
            case SyntaxKind.Argument:
                break;
            case SyntaxKind.NameColon:
                break;
            case SyntaxKind.CastExpression:
                break;
            case SyntaxKind.AnonymousMethodExpression:
                break;
            case SyntaxKind.SimpleLambdaExpression:
                break;
            case SyntaxKind.ParenthesizedLambdaExpression:
                break;
            case SyntaxKind.ObjectInitializerExpression:
                break;
            case SyntaxKind.CollectionInitializerExpression:
                break;
            case SyntaxKind.ArrayInitializerExpression:
                break;
            case SyntaxKind.AnonymousObjectMemberDeclarator:
                break;
            case SyntaxKind.ComplexElementInitializerExpression:
                break;
            case SyntaxKind.ObjectCreationExpression:
                break;
            case SyntaxKind.AnonymousObjectCreationExpression:
                break;
            case SyntaxKind.ArrayCreationExpression:
                break;
            case SyntaxKind.ImplicitArrayCreationExpression:
                break;
            case SyntaxKind.StackAllocArrayCreationExpression:
                break;
            case SyntaxKind.OmittedArraySizeExpression:
                break;
            case SyntaxKind.InterpolatedStringExpression:
                break;
            case SyntaxKind.ImplicitElementAccess:
                break;
            case SyntaxKind.IsPatternExpression:
                break;
            case SyntaxKind.RangeExpression:
                break;
            case SyntaxKind.ImplicitObjectCreationExpression:
                break;
            case SyntaxKind.AddExpression:
                break;
            case SyntaxKind.SubtractExpression:
                break;
            case SyntaxKind.MultiplyExpression:
                break;
            case SyntaxKind.DivideExpression:
                break;
            case SyntaxKind.ModuloExpression:
                break;
            case SyntaxKind.LeftShiftExpression:
                break;
            case SyntaxKind.RightShiftExpression:
                break;
            case SyntaxKind.LogicalOrExpression:
                break;
            case SyntaxKind.LogicalAndExpression:
                break;
            case SyntaxKind.BitwiseOrExpression:
                break;
            case SyntaxKind.BitwiseAndExpression:
                break;
            case SyntaxKind.ExclusiveOrExpression:
                break;
            case SyntaxKind.EqualsExpression:
                break;
            case SyntaxKind.NotEqualsExpression:
                break;
            case SyntaxKind.LessThanExpression:
                break;
            case SyntaxKind.LessThanOrEqualExpression:
                break;
            case SyntaxKind.GreaterThanExpression:
                break;
            case SyntaxKind.GreaterThanOrEqualExpression:
                break;
            case SyntaxKind.IsExpression:
                break;
            case SyntaxKind.AsExpression:
                break;
            case SyntaxKind.CoalesceExpression:
                break;
            case SyntaxKind.SimpleMemberAccessExpression:
                break;
            case SyntaxKind.PointerMemberAccessExpression:
                break;
            case SyntaxKind.ConditionalAccessExpression:
                break;
            case SyntaxKind.UnsignedRightShiftExpression:
                break;
            case SyntaxKind.MemberBindingExpression:
                break;
            case SyntaxKind.ElementBindingExpression:
                break;
            case SyntaxKind.SimpleAssignmentExpression:
                break;
            case SyntaxKind.AddAssignmentExpression:
                break;
            case SyntaxKind.SubtractAssignmentExpression:
                break;
            case SyntaxKind.MultiplyAssignmentExpression:
                break;
            case SyntaxKind.DivideAssignmentExpression:
                break;
            case SyntaxKind.ModuloAssignmentExpression:
                break;
            case SyntaxKind.AndAssignmentExpression:
                break;
            case SyntaxKind.ExclusiveOrAssignmentExpression:
                break;
            case SyntaxKind.OrAssignmentExpression:
                break;
            case SyntaxKind.LeftShiftAssignmentExpression:
                break;
            case SyntaxKind.RightShiftAssignmentExpression:
                break;
            case SyntaxKind.CoalesceAssignmentExpression:
                break;
            case SyntaxKind.UnsignedRightShiftAssignmentExpression:
                break;
            case SyntaxKind.UnaryPlusExpression:
                break;
            case SyntaxKind.UnaryMinusExpression:
                break;
            case SyntaxKind.BitwiseNotExpression:
                break;
            case SyntaxKind.LogicalNotExpression:
                break;
            case SyntaxKind.PreIncrementExpression:
                break;
            case SyntaxKind.PreDecrementExpression:
                break;
            case SyntaxKind.PointerIndirectionExpression:
                break;
            case SyntaxKind.AddressOfExpression:
                break;
            case SyntaxKind.PostIncrementExpression:
                break;
            case SyntaxKind.PostDecrementExpression:
                break;
            case SyntaxKind.AwaitExpression:
                break;
            case SyntaxKind.IndexExpression:
                break;
            case SyntaxKind.ThisExpression:
                break;
            case SyntaxKind.BaseExpression:
                break;
            case SyntaxKind.ArgListExpression:
                break;
            case SyntaxKind.NumericLiteralExpression:
                break;
            case SyntaxKind.StringLiteralExpression:
                break;
            case SyntaxKind.CharacterLiteralExpression:
                break;
            case SyntaxKind.TrueLiteralExpression:
                break;
            case SyntaxKind.FalseLiteralExpression:
                break;
            case SyntaxKind.NullLiteralExpression:
                break;
            case SyntaxKind.DefaultLiteralExpression:
                break;
            case SyntaxKind.Utf8StringLiteralExpression:
                break;
            case SyntaxKind.FieldExpression:
                break;
            case SyntaxKind.TypeOfExpression:
                break;
            case SyntaxKind.SizeOfExpression:
                break;
            case SyntaxKind.CheckedExpression:
                break;
            case SyntaxKind.UncheckedExpression:
                break;
            case SyntaxKind.DefaultExpression:
                break;
            case SyntaxKind.MakeRefExpression:
                break;
            case SyntaxKind.RefValueExpression:
                break;
            case SyntaxKind.RefTypeExpression:
                break;
            case SyntaxKind.UnsafeExpression:
                break;
            case SyntaxKind.QueryExpression:
                break;
            case SyntaxKind.QueryBody:
                break;
            case SyntaxKind.FromClause:
                break;
            case SyntaxKind.LetClause:
                break;
            case SyntaxKind.JoinClause:
                break;
            case SyntaxKind.JoinIntoClause:
                break;
            case SyntaxKind.WhereClause:
                break;
            case SyntaxKind.OrderByClause:
                break;
            case SyntaxKind.AscendingOrdering:
                break;
            case SyntaxKind.DescendingOrdering:
                break;
            case SyntaxKind.SelectClause:
                break;
            case SyntaxKind.GroupClause:
                break;
            case SyntaxKind.QueryContinuation:
                break;
            case SyntaxKind.Block:
                break;
            case SyntaxKind.LocalDeclarationStatement:
                break;
            case SyntaxKind.VariableDeclaration:
                break;
            case SyntaxKind.VariableDeclarator:
                break;
            case SyntaxKind.EqualsValueClause:
                break;
            case SyntaxKind.ExpressionStatement:
                break;
            case SyntaxKind.EmptyStatement:
                break;
            case SyntaxKind.LabeledStatement:
                break;
            case SyntaxKind.GotoStatement:
                break;
            case SyntaxKind.GotoCaseStatement:
                break;
            case SyntaxKind.GotoDefaultStatement:
                break;
            case SyntaxKind.BreakStatement:
                break;
            case SyntaxKind.ContinueStatement:
                break;
            case SyntaxKind.ReturnStatement:
                break;
            case SyntaxKind.YieldReturnStatement:
                break;
            case SyntaxKind.YieldBreakStatement:
                break;
            case SyntaxKind.ThrowStatement:
                break;
            case SyntaxKind.WhileStatement:
                break;
            case SyntaxKind.DoStatement:
                break;
            case SyntaxKind.ForStatement:
                break;
            case SyntaxKind.ForEachStatement:
                break;
            case SyntaxKind.UsingStatement:
                break;
            case SyntaxKind.FixedStatement:
                break;
            case SyntaxKind.CheckedStatement:
                break;
            case SyntaxKind.UncheckedStatement:
                break;
            case SyntaxKind.UnsafeStatement:
                break;
            case SyntaxKind.LockStatement:
                break;
            case SyntaxKind.IfStatement:
                break;
            case SyntaxKind.ElseClause:
                break;
            case SyntaxKind.SwitchStatement:
                break;
            case SyntaxKind.SwitchSection:
                break;
            case SyntaxKind.CaseSwitchLabel:
                break;
            case SyntaxKind.DefaultSwitchLabel:
                break;
            case SyntaxKind.TryStatement:
                break;
            case SyntaxKind.CatchClause:
                break;
            case SyntaxKind.CatchDeclaration:
                break;
            case SyntaxKind.CatchFilterClause:
                break;
            case SyntaxKind.FinallyClause:
                break;
            case SyntaxKind.LocalFunctionStatement:
                break;
            case SyntaxKind.CompilationUnit:
                break;
            case SyntaxKind.GlobalStatement:
                break;
            case SyntaxKind.NamespaceDeclaration:
                break;
            case SyntaxKind.UsingDirective:
                break;
            case SyntaxKind.ExternAliasDirective:
                break;
            case SyntaxKind.FileScopedNamespaceDeclaration:
                break;
            case SyntaxKind.AttributeList:
                break;
            case SyntaxKind.AttributeTargetSpecifier:
                break;
            case SyntaxKind.Attribute:
                break;
            case SyntaxKind.AttributeArgumentList:
                break;
            case SyntaxKind.AttributeArgument:
                break;
            case SyntaxKind.NameEquals:
                break;
            case SyntaxKind.ClassDeclaration:
                break;
            case SyntaxKind.StructDeclaration:
                break;
            case SyntaxKind.InterfaceDeclaration:
                break;
            case SyntaxKind.EnumDeclaration:
                break;
            case SyntaxKind.DelegateDeclaration:
                break;
            case SyntaxKind.BaseList:
                break;
            case SyntaxKind.SimpleBaseType:
                break;
            case SyntaxKind.TypeParameterConstraintClause:
                break;
            case SyntaxKind.ConstructorConstraint:
                break;
            case SyntaxKind.ClassConstraint:
                break;
            case SyntaxKind.StructConstraint:
                break;
            case SyntaxKind.TypeConstraint:
                break;
            case SyntaxKind.ExplicitInterfaceSpecifier:
                break;
            case SyntaxKind.EnumMemberDeclaration:
                break;
            case SyntaxKind.FieldDeclaration:
                break;
            case SyntaxKind.EventFieldDeclaration:
                break;
            case SyntaxKind.MethodDeclaration:
                break;
            case SyntaxKind.OperatorDeclaration:
                break;
            case SyntaxKind.ConversionOperatorDeclaration:
                break;
            case SyntaxKind.ConstructorDeclaration:
                break;
            case SyntaxKind.AllowsConstraintClause:
                break;
            case SyntaxKind.RefStructConstraint:
                break;
            case SyntaxKind.BaseConstructorInitializer:
                break;
            case SyntaxKind.ThisConstructorInitializer:
                break;
            case SyntaxKind.DestructorDeclaration:
                break;
            case SyntaxKind.PropertyDeclaration:
                break;
            case SyntaxKind.EventDeclaration:
                break;
            case SyntaxKind.IndexerDeclaration:
                break;
            case SyntaxKind.AccessorList:
                break;
            case SyntaxKind.GetAccessorDeclaration:
                break;
            case SyntaxKind.SetAccessorDeclaration:
                break;
            case SyntaxKind.AddAccessorDeclaration:
                break;
            case SyntaxKind.RemoveAccessorDeclaration:
                break;
            case SyntaxKind.UnknownAccessorDeclaration:
                break;
            case SyntaxKind.ParameterList:
                break;
            case SyntaxKind.BracketedParameterList:
                break;
            case SyntaxKind.Parameter:
                break;
            case SyntaxKind.TypeParameterList:
                break;
            case SyntaxKind.TypeParameter:
                break;
            case SyntaxKind.IncompleteMember:
                break;
            case SyntaxKind.ArrowExpressionClause:
                break;
            case SyntaxKind.Interpolation:
                break;
            case SyntaxKind.InterpolatedStringText:
                break;
            case SyntaxKind.InterpolationAlignmentClause:
                break;
            case SyntaxKind.InterpolationFormatClause:
                break;
            case SyntaxKind.ShebangDirectiveTrivia:
                break;
            case SyntaxKind.LoadDirectiveTrivia:
                break;
            case SyntaxKind.TupleType:
                break;
            case SyntaxKind.TupleElement:
                break;
            case SyntaxKind.TupleExpression:
                break;
            case SyntaxKind.SingleVariableDesignation:
                break;
            case SyntaxKind.ParenthesizedVariableDesignation:
                break;
            case SyntaxKind.ForEachVariableStatement:
                break;
            case SyntaxKind.DeclarationPattern:
                break;
            case SyntaxKind.ConstantPattern:
                break;
            case SyntaxKind.CasePatternSwitchLabel:
                break;
            case SyntaxKind.WhenClause:
                break;
            case SyntaxKind.DiscardDesignation:
                break;
            case SyntaxKind.RecursivePattern:
                break;
            case SyntaxKind.PropertyPatternClause:
                break;
            case SyntaxKind.Subpattern:
                break;
            case SyntaxKind.PositionalPatternClause:
                break;
            case SyntaxKind.DiscardPattern:
                break;
            case SyntaxKind.SwitchExpression:
                break;
            case SyntaxKind.SwitchExpressionArm:
                break;
            case SyntaxKind.VarPattern:
                break;
            case SyntaxKind.ParenthesizedPattern:
                break;
            case SyntaxKind.RelationalPattern:
                break;
            case SyntaxKind.TypePattern:
                break;
            case SyntaxKind.OrPattern:
                break;
            case SyntaxKind.AndPattern:
                break;
            case SyntaxKind.NotPattern:
                break;
            case SyntaxKind.SlicePattern:
                break;
            case SyntaxKind.ListPattern:
                break;
            case SyntaxKind.DeclarationExpression:
                break;
            case SyntaxKind.RefExpression:
                break;
            case SyntaxKind.RefType:
                break;
            case SyntaxKind.ThrowExpression:
                break;
            case SyntaxKind.ImplicitStackAllocArrayCreationExpression:
                break;
            case SyntaxKind.SuppressNullableWarningExpression:
                break;
            case SyntaxKind.NullableDirectiveTrivia:
                break;
            case SyntaxKind.FunctionPointerType:
                break;
            case SyntaxKind.FunctionPointerParameter:
                break;
            case SyntaxKind.FunctionPointerParameterList:
                break;
            case SyntaxKind.FunctionPointerCallingConvention:
                break;
            case SyntaxKind.InitAccessorDeclaration:
                break;
            case SyntaxKind.WithExpression:
                break;
            case SyntaxKind.WithInitializerExpression:
                break;
            case SyntaxKind.RecordDeclaration:
                break;
            case SyntaxKind.DefaultConstraint:
                break;
            case SyntaxKind.PrimaryConstructorBaseType:
                break;
            case SyntaxKind.FunctionPointerUnmanagedCallingConventionList:
                break;
            case SyntaxKind.FunctionPointerUnmanagedCallingConvention:
                break;
            case SyntaxKind.RecordStructDeclaration:
                break;
            case SyntaxKind.ExpressionColon:
                break;
            case SyntaxKind.LineDirectivePosition:
                break;
            case SyntaxKind.LineSpanDirectiveTrivia:
                break;
            case SyntaxKind.InterpolatedSingleLineRawStringStartToken:
                break;
            case SyntaxKind.InterpolatedMultiLineRawStringStartToken:
                break;
            case SyntaxKind.InterpolatedRawStringEndToken:
                break;
            case SyntaxKind.ScopedType:
                break;
            case SyntaxKind.CollectionExpression:
                break;
            case SyntaxKind.ExpressionElement:
                break;
            case SyntaxKind.SpreadElement:
                break;
            case SyntaxKind.ExtensionBlockDeclaration:
                break;
            case SyntaxKind.IgnoredDirectiveTrivia:
                break;
            case SyntaxKind.WithElement:
                break;
            case SyntaxKind.UnionDeclaration:
                break;
            default:
                break;
        }

        return  !IsWordLike(previous) 
            ?  false 
            :  IsWordLike(current) || current.IsKind(SyntaxKind.OpenBraceToken) || current.Parent is PrefixUnaryExpressionSyntax;
    }

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
            switch (node)
            {
                case BlockSyntax block when IsInside(position, block.OpenBraceToken, block.CloseBraceToken):
                case BaseTypeDeclarationSyntax type when IsInside(position, type.OpenBraceToken, type.CloseBraceToken):
                case NamespaceDeclarationSyntax ns when IsInside(position, ns.OpenBraceToken, ns.CloseBraceToken):
                case SwitchStatementSyntax switchStatement when IsInside(position, switchStatement.OpenBraceToken, switchStatement.CloseBraceToken):
                case AccessorListSyntax accessors when !IsInline(accessors) && IsInside(position, accessors.OpenBraceToken, accessors.CloseBraceToken):
                case SwitchSectionSyntax { Statements: [var firstStatement, ..] } when position >= firstStatement.SpanStart:
                    depth++;
                    break;
                case CollectionExpressionSyntax collection when IsMultiline(collection):
                    var isBracket = token == collection.OpenBracketToken || token == collection.CloseBracketToken;
                    var isReturned = collection.Parent is ReturnStatementSyntax;
                    depth += ( isBracket ? 0 : 1 ) + ( isReturned ? 0 : 1 );
                    break;
                default:
                    break;
            }
        }

        return depth;
    }

    private static bool IsInside(int position, SyntaxToken open, SyntaxToken close) => position > open.SpanStart && position < close.SpanStart;
}
