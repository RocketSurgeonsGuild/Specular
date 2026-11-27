using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Specular.Analyzers;

internal static class SwitchGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items">The call sites, each with its cached block text.</param>
    /// <param name="statementsOf">Turns a call site's cached text into the statements of its section, parsing it once.</param>
    public static SwitchStatementSyntax GenerateSwitchStatement(IReadOnlyList<ResolvedSourceLocation> items, Func<string, IReadOnlyList<StatementSyntax>> statementsOf)
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
                    inner => generateFilePathSwitchStatement(inner, statementsOf),
                    value => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value)),
                    statementsOf
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
        Func<T, LiteralExpressionSyntax> literalFactory,
        Func<string, IReadOnlyList<StatementSyntax>> statementsOf
    ) where T : notnull
    {
        if (blocks is [var localBlock])
        {
            return SectionOf(statementsOf(localBlock.Expression));
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

    private static SwitchSectionSyntax generateFilePathSwitchStatement(
        IReadOnlyList<ResolvedSourceLocation> innerGroup,
        Func<string, IReadOnlyList<StatementSyntax>> statementsOf
    ) => createNestedSwitchSections(
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
        group => SectionOf(statementsOf(group is [var first, ..] ? first.Expression : "")),
        value =>
            LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                Literal(value)
            ),
        statementsOf
    );

    // A section that already returns (GetTypes and GetAssemblies) would make a trailing break unreachable code (CS0162) in the
    // consumer's build.
    private static SwitchSectionSyntax SectionOf(IReadOnlyList<StatementSyntax> statements)
    {
        var section = SwitchSection().AddStatements([.. statements]);
        return section.Statements.LastOrDefault() is ReturnStatementSyntax or ThrowStatementSyntax ? section : section.AddStatements(BreakStatement());
    }

    private static readonly CSharpParseOptions ScriptParseOptions = new(kind: SourceCodeKind.Script);

    internal static List<StatementSyntax> ParseStatements(string expression)
    {
        var tree = ParseSyntaxTree(expression, ScriptParseOptions);
        return tree
              .GetRoot()
              .DescendantNodes()
              .OfType<BlockSyntax>()
              .SelectMany(z => z.Statements)
              .ToList();
    }
}
