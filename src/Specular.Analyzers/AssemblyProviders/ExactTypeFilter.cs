using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Specular.Scanning;

namespace Specular.Analyzers.AssemblyProviders;

internal enum ExactTypeFilterKind
{
    AssignableTo = 1,
    WithAttribute = 2,
    WithAnyAttribute = 3,
}

/// <summary>
///     A type or attribute clause matched against the exact symbols a selector in source named. Specular.Scanning names types by
///     reference, which cannot express everything a symbol can (a type argument that is an array or a type parameter, a generic
///     definition); Specular uses this filter for any clause whose types would not resolve back to the same symbols, so selectors keep
///     matching what they always have.
/// </summary>
internal sealed class ExactTypeFilter(ExactTypeFilterKind kind, bool include, ImmutableArray<INamedTypeSymbol> types) : CustomTypeFilter
{
    private readonly ExactTypeFilterKind _kind = kind;
    private readonly bool _include = include;
    private readonly ImmutableArray<INamedTypeSymbol> _types = types;

    public override string CacheKey { get; } = $"specular-exact/1|{(int)kind}|{( include ? 1 : 0 )}|{string.Join("|", types.Select(GetKey).OrderBy(z => z, StringComparer.Ordinal))}";

    // The same per-clause rules as Specular.Scanning's TypeMatcher, on symbols instead of resolved references.
    public override bool IsMatch(Compilation compilation, INamedTypeSymbol type) =>
        _include
     == _kind switch
     {
         ExactTypeFilterKind.AssignableTo => _types.Any(target => ScanAssignability.HasImplicitGenericConversion(compilation, target, type)),
         ExactTypeFilterKind.WithAttribute => type
                                             .GetAttributes()
                                             .Any(applied => _types.Any(attribute => SymbolEqualityComparer.Default.Equals(applied.AttributeClass, attribute))),
         ExactTypeFilterKind.WithAnyAttribute => type.GetAttributes().Any(applied => _types.Any(attribute => SameNameAndNamespace(attribute, applied.AttributeClass))),
         _ => false,
     };

    private static string GetKey(INamedTypeSymbol type) =>
        ( type.ContainingAssembly?.Identity.GetDisplayName() ?? "" ) + "!" + type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

    private static bool SameNameAndNamespace(INamedTypeSymbol attribute, INamedTypeSymbol? applied) =>
        applied is { }
     && string.Equals(attribute.Name, applied.Name, StringComparison.Ordinal)
     && SymbolEqualityComparer.Default.Equals(attribute.ContainingNamespace, applied.ContainingNamespace);
}
