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
