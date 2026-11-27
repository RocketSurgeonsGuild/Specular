using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Specular.Analyzers;

/// <summary>
///     A piece of generated C#: its text, and for <c>typeof(T)</c> the <c>T</c> inside it, which
///     <c>ServiceDescriptor.Lifetime&lt;TService, TImplementation&gt;()</c> reuses as type arguments.
/// </summary>
internal readonly record struct CodeExpression(string Text, string? TypeOfType = null, bool IsLambda = false)
{
    public override string ToString() => Text;
}

/// <summary>
///     The text forms the generator writes, spelled exactly as <c>NormalizeWhitespace</c> laid out the trees it used to build, so the
///     generated sources and the build cache's text (and its hash) stay the same.
/// </summary>
internal static class CodeText
{
    /// <summary>The line ending the generator has always written into its sources.</summary>
    public const string SourceEndOfLine = "\r\n";

    /// <summary>A C# string literal, escaped as <see cref="SyntaxFactory.Literal(string)" /> would.</summary>
    public static string Literal(string value) => SymbolDisplay.FormatLiteral(value, true);

    /// <summary>
    ///     A type name from <see cref="Helpers.GetTypeOfName" /> as it reads once parsed and normalized: a space after each comma
    ///     between type arguments, and <c>T&lt;,, &gt;</c> for an unbound generic.
    /// </summary>
    public static string TypeName(string typeOfName)
    {
        if (typeOfName.IndexOf(',') < 0) return typeOfName;

        var builder = new StringBuilder(typeOfName.Length + 8);
        for (var i = 0; i < typeOfName.Length; i++)
        {
            var character = typeOfName[i];
            _ = builder.Append(character);
            if (character != ',' || i + 1 >= typeOfName.Length) continue;

            var next = typeOfName[i + 1];
            if (next is not ',' and not ' ') _ = builder.Append(' ');
        }

        return builder.ToString();
    }

    /// <summary>
    ///     A call site's statements as the build cache stores them: a block with <c>\n</c> line endings and four-space indents.
    /// </summary>
    public static string Block(IReadOnlyCollection<string> statements)
    {
        if (statements.Count == 0) return "{\n}";

        var builder = new StringBuilder("{\n");
        foreach (var statement in statements)
        {
            _ = builder.Append("    ").Append(statement).Append('\n');
        }

        return builder.Append('}').ToString();
    }

    public static CodeExpression TypeOf(string typeOfName)
    {
        var type = TypeName(typeOfName);
        return new($"typeof({type})", type);
    }
}

/// <summary>Writes indented lines of generated code.</summary>
internal sealed class SourceWriter(string endOfLine, int capacity = 1024)
{
    private const int IndentSize = 4;
    private static readonly string[] Indents = Enumerable.Range(0, 16).Select(depth => new string(' ', depth * IndentSize)).ToArray();

    private readonly StringBuilder _text = new(capacity);

    public int Depth { get; set; }

    public SourceWriter Line(string text)
    {
        _ = _text.Append(Indent(Depth)).Append(text).Append(endOfLine);
        return this;
    }

    public SourceWriter BlankLine()
    {
        _ = _text.Append(endOfLine);
        return this;
    }

    /// <summary>Writes text that already carries its own line endings and indentation.</summary>
    public SourceWriter Raw(string text)
    {
        _ = _text.Append(text);
        return this;
    }

    public SourceWriter Open()
    {
        _ = Line("{");
        Depth++;
        return this;
    }

    public SourceWriter Close(string closing = "}")
    {
        Depth--;
        return Line(closing);
    }

    public override string ToString() => _text.ToString();

    public static string Indent(int depth) => depth < Indents.Length ? Indents[depth] : new(' ', depth * IndentSize);
}
