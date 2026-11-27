using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>The full metadata names scans record types by, and the lookup back from them.</summary>
internal static class ScanSymbolNames
{
    /// <summary>
    ///     <paramref name="namespace" />'s display name (<c>ToDisplayString()</c>), formatted once per namespace symbol. Namespace filters
    ///     compare it for every type every request scans, and formatting it allocates each time.
    /// </summary>
    public static string GetNamespaceName(INamespaceSymbol @namespace) => NamespaceNames.GetValue(@namespace, static z => z.ToDisplayString());

    // Keyed by the symbol, so a name lives as long as its namespace symbol: a compilation's source namespaces go with it.
    private static readonly ConditionalWeakTable<INamespaceSymbol, string> NamespaceNames = new();

    /// <summary>
    ///     The full metadata name: namespaces joined with <c>.</c>, nested types with <c>+</c>, generic types with their arity
    ///     (<c>Ns.Outer`1+Inner</c>), so <c>GetTypeByMetadataName</c> resolves it. Specular.Analyzers' <c>Helpers.GetFullMetadataName</c>
    ///     differs only for types nested in generics, which it writes as <c>Ns.Outer&lt;T&gt;+Inner</c>.
    /// </summary>
    public static string GetFullMetadataName(ISymbol? symbol)
    {
        if (symbol is null || IsRootNamespace(symbol)) return "";

        var sb = new StringBuilder(symbol.MetadataName);
        var workingSymbol = symbol.ContainingSymbol;

        while (workingSymbol is { } && !IsRootNamespace(workingSymbol))
        {
            _ = sb.Insert(0, workingSymbol is ITypeSymbol && symbol is ITypeSymbol ? '+' : '.').Insert(0, workingSymbol.MetadataName);
            workingSymbol = workingSymbol.ContainingSymbol;
        }

        return sb.ToString();
    }

    /// <summary>The type <paramref name="fullMetadataName" /> names in <paramref name="assembly" />, if a scan could have found it.</summary>
    public static INamedTypeSymbol? FindType(IAssemblySymbol assembly, string fullMetadataName) =>
        assembly.GetTypeByMetadataName(fullMetadataName) is { CanBeReferencedByName: true } type ? type : null;

    private static bool IsRootNamespace(ISymbol symbol) => symbol is INamespaceSymbol { IsGlobalNamespace: true };
}
