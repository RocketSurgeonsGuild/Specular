using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Specular.Analyzers;

internal static class Helpers
{
    // Validates that an invocation resolves to a method declared on ISpecularProvider. Detection in the
    // syntax providers is structural only (the receiver may reference the not-yet-generated SpecularProvider),
    // so this runs against a compilation augmented with an SpecularProvider shell to confirm genuine scan calls.
    public static bool IsSpecularProviderCall(SemanticModel semanticModel, InvocationExpressionSyntax invocation)
    {
        var info = semanticModel.GetSymbolInfo(invocation);
        return ( info.Symbol ?? info.CandidateSymbols.FirstOrDefault() ) is IMethodSymbol { ContainingType.MetadataName: "ISpecularProvider" };
    }

    /// <summary>The directives that open every generated provider file, each line ending in <see cref="CodeText.SourceEndOfLine" />.</summary>
    public static string FileHeader =>
        $"#nullable enable{CodeText.SourceEndOfLine}#pragma warning disable {string.Join(", ", _disabledWarnings)}{CodeText.SourceEndOfLine}";

    /// <summary>The directives that close every generated provider file.</summary>
    public static string FileFooter =>
        $"#pragma warning restore {string.Join(", ", _disabledWarnings)}{CodeText.SourceEndOfLine}#nullable restore{CodeText.SourceEndOfLine}";

    public static string AssemblyVariableName(IAssemblySymbol symbol) => SpecialCharacterRemover.Replace(symbol.Identity.GetDisplayName(true), "");

    public static TypeSyntax? ExtractSyntaxFromMethod(
        InvocationExpressionSyntax expression,
        NameSyntax name
    ) => name is GenericNameSyntax { TypeArgumentList.Arguments.Count: 1 } genericNameSyntax
        ? genericNameSyntax.TypeArgumentList.Arguments[0]
        : name is not SimpleNameSyntax
            ? null
            : expression.ArgumentList.Arguments is [{ Expression: TypeOfExpressionSyntax typeOfExpression }]
                ? typeOfExpression.Type
                : null;

    public static IEnumerable<INamedTypeSymbol> GetBaseTypes(Compilation compilation, INamedTypeSymbol namedTypeSymbol)
    {
        while (namedTypeSymbol.BaseType is { })
        {
            if (SymbolEqualityComparer.Default.Equals(namedTypeSymbol.BaseType, compilation.ObjectType)) yield break;

            yield return namedTypeSymbol.BaseType;
            namedTypeSymbol = namedTypeSymbol.BaseType;
        }
    }

    public static INamedTypeSymbol GetClosedGenericConversion(
        Compilation compilation,
        INamedTypeSymbol assignableToType,
        INamedTypeSymbol assignableFromType
    )
    {
        if (assignableToType is not { IsUnboundGenericType: true, Arity: > 0 }) return assignableToType;

        if (GetUnboundGenericType(assignableFromType) is { } unboundFromType && compilation.HasImplicitConversion(assignableToType, unboundFromType))
            // TODO:
            return assignableToType;
        //            return assignableToType.Construct(assignableFromType.TypeArguments.ToArray());
        var matchingInterfaces = assignableFromType
                                .AllInterfaces
                                .Where(symbol => symbol is { } && compilation.HasImplicitConversion(GetUnboundGenericType(symbol), assignableToType));
        return matchingInterfaces.FirstOrDefault() ?? assignableToType;
    }

    // Same groups, group order and element order as Enumerable.GroupBy, with each group materialized once so it can be indexed.
    public static List<(TKey Key, List<TElement> Items)> GroupInOrder<TElement, TKey>(IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        where TKey : notnull
    {
        var groups = new List<(TKey Key, List<TElement> Items)>();
        var indexByKey = new Dictionary<TKey, int>();
        foreach (var element in source)
        {
            var key = keySelector(element);
            if (!indexByKey.TryGetValue(key, out var index))
            {
                index = groups.Count;
                indexByKey.Add(key, index);
                groups.Add((key, []));
            }

            groups[index].Items.Add(element);
        }

        return groups;
    }

    // Same order as OrderBy(GetFullMetadataName): constructed generics share a metadata name, so ties keep their source order.
    public static List<T> OrderByFullMetadataName<T>(List<T> symbols)
        where T : ISymbol
    {
        var keyed = new List<(string Key, int Index, T Symbol)>(symbols.Count);
        foreach (var symbol in symbols)
        {
            keyed.Add((GetFullMetadataName(symbol), keyed.Count, symbol));
        }

        return sortByKey(keyed);
    }

    /// <inheritdoc cref="OrderByFullMetadataName{T}(List{T})" />
    public static List<T> OrderByFullMetadataName<T>(ImmutableArray<T> symbols)
        where T : ISymbol
    {
        var keyed = new List<(string Key, int Index, T Symbol)>(symbols.Length);
        foreach (var symbol in symbols)
        {
            keyed.Add((GetFullMetadataName(symbol), keyed.Count, symbol));
        }

        return sortByKey(keyed);
    }

    private static List<T> sortByKey<T>(List<(string Key, int Index, T Symbol)> keyed)
    {
        keyed.Sort(
            static (left, right) => Comparer<string>.Default.Compare(left.Key, right.Key) is var order and not 0 ? order : left.Index.CompareTo(right.Index)
        );
        var ordered = new List<T>(keyed.Count);
        foreach (var item in keyed)
        {
            ordered.Add(item.Symbol);
        }

        return ordered;
    }

    // The three names below depend only on the symbol, and generating code asks for them again for every request a type matches and
    // every registration it gets. Inside CacheNames() each is built once per symbol for the step that opened it; outside one they are
    // built on every call. A dictionary per step rather than a ConditionalWeakTable: a cold run meets thousands of new symbols, and
    // every table entry is a dependent handle the GC has to track.
    public static NameCacheScope CacheNames()
    {
        var previous = nameCache;
        nameCache = new();
        return new(previous);
    }

    [ThreadStatic]
    private static NameCache? nameCache;

    internal sealed class NameCache
    {
        public readonly Dictionary<ISymbol, string> FullMetadataNames = [with(SymbolEqualityComparer.Default)];
        public readonly Dictionary<ISymbol, string> GenericDisplayNames = [with(SymbolEqualityComparer.Default)];
        public readonly Dictionary<ISymbol, string> TypeOfNames = [with(SymbolEqualityComparer.Default)];
    }

    internal readonly struct NameCacheScope(NameCache? previous) : IDisposable
    {
        public void Dispose() => nameCache = previous;
    }

    private static string Cached(Dictionary<ISymbol, string>? cache, ISymbol symbol, Func<ISymbol, string> compute)
    {
        if (cache is null) return compute(symbol);
        if (cache.TryGetValue(symbol, out var name)) return name;

        name = compute(symbol);
        cache[symbol] = name;
        return name;
    }

    public static string GetFullMetadataName(ISymbol? symbol) =>
        symbol is null || IsRootNamespace(symbol) ? "" : Cached(nameCache?.FullMetadataNames, symbol, computeFullMetadataName);

    private static readonly Func<ISymbol, string> computeFullMetadataName = ComputeFullMetadataName;

    private static string ComputeFullMetadataName(ISymbol symbol)
    {

        var sb = new StringBuilder(symbol.MetadataName);

        var last = symbol;

        var workingSymbol = symbol.ContainingSymbol;

        while (!IsRootNamespace(workingSymbol))
        {
            sb = ( workingSymbol is ITypeSymbol && last is ITypeSymbol ? sb.Insert(0, '+') : sb.Insert(0, '.') )
               .Insert(0, workingSymbol.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat).Trim());
            //sb.Insert(0, symbol.MetadataName);
            workingSymbol = workingSymbol.ContainingSymbol;
        }

        return sb.ToString();
    }

    public static string GetGenericDisplayName(ISymbol? symbol) =>
        symbol is null || IsRootNamespace(symbol) ? "" : Cached(nameCache?.GenericDisplayNames, symbol, computeGenericDisplayName);

    private static readonly Func<ISymbol, string> computeGenericDisplayName = ComputeGenericDisplayName;

    private static string ComputeGenericDisplayName(ISymbol symbol)
    {

        var sb = new StringBuilder(symbol.MetadataName);
        if (symbol is INamedTypeSymbol namedTypeSymbol && ( namedTypeSymbol.IsOpenGenericType() || namedTypeSymbol.IsGenericType ))
        {
            sb = new(symbol.Name);
            if (namedTypeSymbol.IsOpenGenericType())
            {
                sb.Append('<');
                for (var i = 1; i < namedTypeSymbol.Arity; i++)
                {
                    sb.Append(',');
                }

                sb.Append('>');
            }
            else
            {
                sb.Append('<');
                for (var index = 0; index < namedTypeSymbol.TypeArguments.Length; index++)
                {
                    var argument = namedTypeSymbol.TypeArguments[index];
                    sb.Append(GetGenericDisplayName(argument));
                    if (index < namedTypeSymbol.TypeArguments.Length - 1) sb.Append(',');
                }

                sb.Append('>');
            }
        }

        var last = symbol;

        var workingSymbol = symbol.ContainingSymbol;

        while (!IsRootNamespace(workingSymbol))
        {
            sb = ( workingSymbol is ITypeSymbol && last is ITypeSymbol ? sb.Insert(0, '+') : sb.Insert(0, '.') )
               .Insert(0, workingSymbol.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat).Trim());
            //sb.Insert(0, symbol.MetadataName);
            workingSymbol = workingSymbol.ContainingSymbol;
        }

        sb.Insert(0, "global::");
        return sb.ToString();
    }

    public static string GetTypeOfName(ISymbol? symbol) =>
        symbol is null || IsRootNamespace(symbol) ? "" : Cached(nameCache?.TypeOfNames, symbol, computeTypeOfName);

    private static readonly Func<ISymbol, string> computeTypeOfName = ComputeTypeOfName;

    private static string ComputeTypeOfName(ISymbol symbol)
    {

        var sb = new StringBuilder(symbol.MetadataName);
        if (symbol is INamedTypeSymbol namedTypeSymbol && ( namedTypeSymbol.IsOpenGenericType() || namedTypeSymbol.IsGenericType ))
        {
            sb = new(symbol.Name);
            if (namedTypeSymbol.IsOpenGenericType())
            {
                sb.Append('<');
                for (var i = 1; i < namedTypeSymbol.Arity; i++)
                {
                    sb.Append(',');
                }

                sb.Append('>');
            }
            else
            {
                sb.Append('<');
                for (var index = 0; index < namedTypeSymbol.TypeArguments.Length; index++)
                {
                    var argument = namedTypeSymbol.TypeArguments[index];
                    sb.Append(GetTypeOfName(argument));
                    if (index < namedTypeSymbol.TypeArguments.Length - 1) sb.Append(',');
                }

                sb.Append('>');
            }
        }

        var workingSymbol = symbol.ContainingSymbol;

        while (!IsRootNamespace(workingSymbol))
        {
            sb.Insert(0, '.');
            sb.Insert(0, workingSymbol.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat).Trim());
            //sb.Insert(0, symbol.MetadataName);
            workingSymbol = workingSymbol.ContainingSymbol;
        }

        sb.Insert(0, "global::");
        return sb.ToString();
    }

    public static INamedTypeSymbol? GetUnboundGenericType(INamedTypeSymbol symbol) => symbol switch
    {
        { IsGenericType: true, IsUnboundGenericType: true } => symbol,
        { IsGenericType: true } => symbol.ConstructUnboundGenericType(),
        _ => default,
    };

    internal static string AddAssemblyAttribute(string key, string? value) =>
        $"[assembly: System.Reflection.AssemblyMetadata({CodeText.Literal(key)}, {( value is null ? "null" : CodeText.Literal(value) )})]";

    [SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "MD5 is used only as a stable, non-cryptographic cache key for selector text; it is never used for security.")]
    internal static SourceLocation CreateSourceLocation(
        SourceLocationKind kind,
        InvocationExpressionSyntax methodCallSyntax,
        string sourceAssemblyName,
        CancellationToken cancellationToken
    )
    {
        if (methodCallSyntax is { Expression: MemberAccessExpressionSyntax memberAccess, ArgumentList.Arguments: [{ Expression: { } argumentExpression }] }) { }
        else if (methodCallSyntax is
        { Expression: MemberAccessExpressionSyntax memberAccess2, ArgumentList.Arguments: [_, { Expression: { } argumentExpression2 }] })
        {
            memberAccess = memberAccess2;
            argumentExpression = argumentExpression2;
        }
        else
        {
            throw new InvalidOperationException("Invalid method call syntax");
        }

        // The selector as the scan report and the cache show it; a multi-line lambda keeps the CRLF NormalizeWhitespace gave it.
        var normalizedExpression = GeneratedCodeLayout.ToText(argumentExpression, GeneratedCodeLayout.SourceEndOfLine);
        var expression = string.Concat(
            argumentExpression
               .ToFullString()
               .Split(['\n', '\r', ' ', '\t'], StringSplitOptions.RemoveEmptyEntries)
               .Select(z => z.Trim())
        );
        using var hasher = MD5.Create();
        var hash = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(expression)));

        var source = new SourceLocation(
            kind,
            memberAccess.Name.SyntaxTree.GetText(cancellationToken).Lines.GetLineFromPosition(memberAccess.Name.SpanStart).LineNumber + 1,
            memberAccess.SyntaxTree.FilePath,
            hash,
            sourceAssemblyName,
            normalizedExpression
        );
        return source;
    }

    internal static string CompilerGeneratedAttributes =>
        $"[System.CodeDom.Compiler.GeneratedCode({CodeText.Literal(AssemblyInfo.Project.AssemblyName)}, {CodeText.Literal(AssemblyInfo.Attributes.FileVersion)}), "
      + "System.Runtime.CompilerServices.CompilerGenerated, Microsoft.CodeAnalysis.EmbeddedAttribute, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]";

    private static bool IsRootNamespace(ISymbol symbol)
    {
        INamespaceSymbol? s;
        return ( s = symbol as INamespaceSymbol ) is { } && s.IsGlobalNamespace;
    }

    private static readonly string[] _disabledWarnings =
    [
        "CA1002",
        "CA1034",
        "CA1822",
        "CS0105",
        "CS1573",
        "CA5351",
        "CS8618",
        "CS8669",
        "IL2026",
        "IL2072",
    ];

    private static readonly Regex SpecialCharacterRemover = new("[^\\w\\d]", RegexOptions.Compiled);
}
