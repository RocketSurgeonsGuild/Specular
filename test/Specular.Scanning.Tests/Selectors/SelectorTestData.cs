using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning.Tests.Selectors;

internal static class SelectorTestData
{
    // Lambdas have no natural type to hang .Should() on.
    public static Action Act(Action action) => action;

    public static EquatableArray<T> Array<T>(params T[] items) => new(ImmutableArray.Create(items));

    public static TypeReference Library(string metadataName, bool unboundGeneric = false) =>
        new(SelectorCompilation.LibraryAssemblyName, metadataName, unboundGeneric);

    public static TypeReference App(string metadataName, bool unboundGeneric = false) =>
        new(SelectorCompilation.AppAssemblyName, metadataName, unboundGeneric);

    /// <summary>The reference the selector records for a special type such as <c>int</c>, wherever the test's core library puts it.</summary>
    public static TypeReference Special(SpecialType specialType)
    {
        var type = SelectorCompilation.Compilation.GetSpecialType(specialType);
        return new(type.ContainingAssembly.MetadataName, $"{type.ContainingNamespace.ToDisplayString()}.{type.MetadataName}", false);
    }

    public static TypeReference Closed(TypeReference definition, params TypeReference[] typeArguments) =>
        definition with { TypeArguments = Array(typeArguments) };
}

internal sealed class NameLengthFilter(int length) : CustomTypeFilter
{
    public override string CacheKey => $"length={length}";

    public override bool IsMatch(Compilation compilation, INamedTypeSymbol type) => type.Name.Length == length;
}

internal sealed class OtherNameLengthFilter(int length) : CustomTypeFilter
{
    public override string CacheKey => $"length={length}";

    public override bool IsMatch(Compilation compilation, INamedTypeSymbol type) => type.Name.Length != length;
}
