using System.Globalization;

namespace Specular.Analyzers;

/// <summary>A call site's statements, one string per line, indented relative to the statements around them.</summary>
internal sealed record StatementLines(IReadOnlyList<string> Lines, bool EndsWithReturn);

internal static class SwitchGenerator
{
    public static void WriteSwitchStatement(SourceWriter writer, IReadOnlyList<ResolvedSourceLocation> items, Func<string, StatementLines> render)
    {
        _ = writer.Line("switch (lineNumber)").Open();
        foreach (var (lineNumber, lineItems) in Helpers.GroupInOrder(items, x => x.Location.LineNumber))
        {
            // disallow list?
            var location = lineItems[0].Location;
            var body = createNestedSwitchSections(
                lineItems,
                "global::System.IO.Path.GetFileName(filePath)",
                x => x.Location.FileName,
                inner => generateFilePathSwitchStatement(inner, render),
                CodeText.Literal,
                render
            );
            WriteSection(writer, location, lineNumber.ToString(CultureInfo.InvariantCulture), body);
        }

        _ = writer.Close();
    }

    private static void WriteSection(SourceWriter writer, SourceLocation location, string label, Action<SourceWriter> body)
    {
        _ = writer
           .Line($"// FilePath: {location.FilePath.Replace("\\", "/")} Expression: {location.ExpressionHash}")
           .Line($"case {label}:");
        writer.Depth++;
        body(writer);
        writer.Depth--;
    }

    private static Action<SourceWriter> createNestedSwitchSections<T>(
        IReadOnlyList<ResolvedSourceLocation> blocks,
        string switchOn,
        Func<ResolvedSourceLocation, T> regroup,
        Func<IReadOnlyList<ResolvedSourceLocation>, Action<SourceWriter>> next,
        Func<T, string> literalFactory,
        Func<string, StatementLines> render
    ) where T : notnull
    {
        if (blocks is [var localBlock])
        {
            return SectionOf(render(localBlock.Expression));
        }

        // Several call sites that all share this key (the same file name on a line, as a project's call site and the identical one a
        // referenced project carries) leave nothing for this switch to tell apart, so the next level is emitted directly.
        var groups = Helpers.GroupInOrder(blocks, regroup);
        return groups is [var (_, onlyGroup)]
            ? next(onlyGroup)
            : ( writer =>
        {
            _ = writer.Line($"switch ({switchOn})").Open();
            foreach (var (key, items) in groups)
            {
                WriteSection(writer, items[0].Location, literalFactory(key), next(items));
            }

            // A blank line follows a closing brace, as it does throughout the generated code.
            _ = writer.Close().BlankLine().Line("break;");
        } );
    }

    private static Action<SourceWriter> generateFilePathSwitchStatement(IReadOnlyList<ResolvedSourceLocation> innerGroup, Func<string, StatementLines> render) =>
        createNestedSwitchSections(
            innerGroup.GroupBy(z => z.Location.ExpressionHash).Select(z => z.First()).ToArray(),
            "SpecularSupport.GetArgumentExpressionHash(argumentExpression)",
            x => x.Location.ExpressionHash,
            group => SectionOf(render(group is [var first, ..] ? first.Expression : "")),
            CodeText.Literal,
            render
        );

    // A section that already returns (GetTypes and GetAssemblies) would make a trailing break unreachable code (CS0162) in the
    // consumer's build.
    private static Action<SourceWriter> SectionOf(StatementLines statements) =>
        writer =>
        {
            foreach (var line in statements.Lines)
            {
                _ = writer.Line(line);
            }

            if (!statements.EndsWithReturn) _ = writer.Line("break;");
        };

    /// <summary>
    ///     The statements of one or more cached blocks (<see cref="CodeText.Block" />, joined by line breaks). Each generated statement
    ///     fits on one line, so the lines inside the braces are the statements.
    /// </summary>
    internal static List<string> BlockStatements(string blocks)
    {
        var statements = new List<string>();
        foreach (var line in blocks.Split('\n'))
        {
            var statement = line.Trim();
            if (statement.Length == 0 || statement is "{" or "}") continue;

            statements.Add(statement);
        }

        return statements;
    }
}
