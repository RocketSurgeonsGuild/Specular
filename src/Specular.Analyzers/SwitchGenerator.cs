using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers;

internal static class SwitchGenerator
{
    public static SwitchStatementSyntax GenerateSwitchStatement(IReadOnlyList<ResolvedSourceLocation> items)
    {
        var lineNumberIdentifier = IdentifierName("lineNumber");
        var switchStatement = SwitchStatement(lineNumberIdentifier);
        foreach (var (lineNumber, lineItems) in Helpers.GroupInOrder(items, x => x.Location.LineNumber))
        {
            // disallow list?
            var location = lineItems[0].Location;
            var lineSwitchSection = createNestedSwitchSections(
                    lineItems,
                    InvocationExpression(
                            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, ParseName("global::System.IO.Path"), IdentifierName("GetFileName"))
                        )
                       .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("filePath"))))),
                    x => x.Location.FileName,
                    generateFilePathSwitchStatement,
                    value => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value))
                )
               .AddLabels(
                    CaseSwitchLabel(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(lineNumber)))
                       .WithKeyword(
                            Token(
                                TriviaList(Comment($"// FilePath: {location.FilePath.Replace("\\", "/")} Expression: {location.ExpressionHash}")),
                                SyntaxKind.CaseKeyword,
                                TriviaList()
                            )
                        )
                );

            switchStatement = switchStatement.AddSections(lineSwitchSection);
        }

        return switchStatement;
    }

    private static SwitchSectionSyntax createNestedSwitchSections<T>(
        IReadOnlyList<ResolvedSourceLocation> blocks,
        ExpressionSyntax identifier,
        Func<ResolvedSourceLocation, T> regroup,
        Func<IReadOnlyList<ResolvedSourceLocation>, SwitchSectionSyntax> next,
        Func<T, LiteralExpressionSyntax> literalFactory
    ) where T : notnull
    {
        if (blocks is [var localBlock])
        {
            return SectionOf(ParseStatements(localBlock.Expression));
        }

        // Several call sites that all share this key (the same file name on a line, as a project's call site and the identical one a
        // referenced project carries) leave nothing for this switch to tell apart, so the next level is emitted directly.
        var groups = Helpers.GroupInOrder(blocks, regroup);
        if (groups is [var (_, onlyGroup)])
        {
            return next(onlyGroup);
        }

        var section = SwitchStatement(identifier);
        foreach (var (key, items) in groups)
        {
            var location = items[0].Location;
            var newSection = next(items)
               .AddLabels(
                    CaseSwitchLabel(literalFactory(key))
                       .WithKeyword(
                            Token(
                                TriviaList(Comment($"// FilePath: {location.FilePath.Replace("\\", "/")} Expression: {location.ExpressionHash}")),
                                SyntaxKind.CaseKeyword,
                                TriviaList()
                            )
                        )
                );
            section = section.AddSections(
                newSection
            );
        }

        return SwitchSection().AddStatements(section, BreakStatement());
    }

    private static SwitchSectionSyntax generateFilePathSwitchStatement(IReadOnlyList<ResolvedSourceLocation> innerGroup) => createNestedSwitchSections(
        innerGroup.GroupBy(z => z.Location.ExpressionHash).Select(z => z.First()).ToArray(),
        InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("SpecularSupport"),
                    IdentifierName("GetArgumentExpressionHash")
                )
            )
           .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("argumentExpression"))))),
        x => x.Location.ExpressionHash,
        generateExpressionHashSwitchStatement,
        value =>
            LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                Literal(value)
            )
    );

    private static SwitchSectionSyntax generateExpressionHashSwitchStatement(IReadOnlyList<ResolvedSourceLocation> innerGroup) =>
        SectionOf(ParseStatements(innerGroup is [var first, ..] ? first.Expression : ""));

    // A section that already returns (GetTypes and GetAssemblies) would make a trailing break unreachable code (CS0162) in the
    // consumer's build.
    private static SwitchSectionSyntax SectionOf(IEnumerable<StatementSyntax> statements)
    {
        var section = SwitchSection().AddStatements(statements.ToArray());
        return section.Statements.LastOrDefault() is ReturnStatementSyntax or ThrowStatementSyntax ? section : section.AddStatements(BreakStatement());
    }

    internal static IEnumerable<StatementSyntax> ParseStatements(string expression)
    {
        var tree = ParseSyntaxTree(expression, new CSharpParseOptions(kind: SourceCodeKind.Script));
        return tree
              .GetRoot()
              .DescendantNodes()
              .OfType<BlockSyntax>()
              .SelectMany(z => z.Statements);
    }
}
