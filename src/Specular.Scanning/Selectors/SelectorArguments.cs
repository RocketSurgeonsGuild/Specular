using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>Argument validation and symbol-to-metadata conversions shared by the selector implementations.</summary>
internal static class SelectorArguments
{
    // Polyfill's ArgumentNullException.ThrowIfNull is an opt-in feature group this project does not enable.
    public static void ThrowIfNull([NotNull] object? argument, string paramName)
    {
        if (argument is null) throw new ArgumentNullException(paramName);
    }

    public static string NotNullOrEmpty(string? value, string paramName)
    {
        ThrowIfNull(value, paramName);
        return value.Length == 0 ? throw new ArgumentException("The value cannot be empty.", paramName) : value;
    }

    public static INamedTypeSymbol NamedType(ITypeSymbol? type, string paramName)
    {
        ThrowIfNull(type, paramName);
        // Only named types can be found again by metadata name when the request is executed or loaded from the cache.
        return type is INamedTypeSymbol { TypeKind: not TypeKind.Error } named
            ? named
            : throw new ArgumentException($"'{type.ToDisplayString()}' is not a named type; only named types can be selected.", paramName);
    }

    public static IAssemblySymbol ContainingAssembly(ITypeSymbol? type, string paramName) =>
        NamedType(type, paramName).ContainingAssembly
     ?? throw new ArgumentException($"'{type!.ToDisplayString()}' has no containing assembly.", paramName);

    // Matches the namespace text the type filter compares against (ToDisplayString), including "<global namespace>".
    public static string Namespace(ITypeSymbol? type, string paramName) => NamedType(type, paramName).ContainingNamespace.ToDisplayString();

    public static TypeReference ToTypeReference(ITypeSymbol? type, string paramName)
    {
        var named = NamedType(type, paramName);
        return new(named.ContainingAssembly?.MetadataName ?? "", ScanSymbolNames.GetFullMetadataName(named), IsUnboundGeneric(named));
    }

    public static TypeReference ToTypeReference(string? metadataName, string paramName)
    {
        var name = NotNullOrEmpty(metadataName, paramName);
        return new("", name, HasGenericArity(name));
    }

    public static ImmutableArray<T> Values<T>(T first, T[]? rest, string firstName, string restName)
    {
        if (first is null) throw new ArgumentNullException(firstName);
        ThrowIfNull(rest, restName);

        var values = ImmutableArray.CreateBuilder<T>(rest.Length + 1);
        values.Add(first);
        foreach (var value in rest)
        {
            if (value is null) throw new ArgumentException("The values cannot contain null.", restName);
            if (!values.Contains(value)) values.Add(value);
        }

        return values.ToImmutable();
    }

    public static ImmutableArray<string> Strings(string first, string[]? rest, string firstName, string restName)
    {
        _ = NotNullOrEmpty(first, firstName);
        var values = Values(first, rest, firstName, restName);
        return values.Any(static value => value.Length == 0)
            ? throw new ArgumentException("The values cannot contain an empty string.", restName)
            : values;
    }

    public static ImmutableArray<TResult> Convert<TSource, TResult>(
        TSource first,
        TSource[]? rest,
        string firstName,
        string restName,
        Func<TSource, string, TResult> convert
    )
    {
        ThrowIfNull(rest, restName);
        var converted = ImmutableArray.CreateBuilder<TResult>(rest.Length + 1);
        converted.Add(convert(first, firstName));
        foreach (var value in rest)
        {
            if (value is null) throw new ArgumentException("The values cannot contain null.", restName);
            var item = convert(value, restName);
            if (!converted.Contains(item)) converted.Add(item);
        }

        return converted.ToImmutable();
    }

    // A generic definition stands for every closed form, exactly like typeof(IHandler<>) does in Specular's runtime selectors.
    private static bool IsUnboundGeneric(INamedTypeSymbol type) => type.IsUnboundGenericType || ( type is { Arity: > 0, IsDefinition: true } );

    private static bool HasGenericArity(string metadataName)
    {
        var start = metadataName.LastIndexOfAny(['.', '+']) + 1;
        var tick = metadataName.IndexOf('`', start);
        if (tick < 0 || tick == metadataName.Length - 1) return false;

        for (var i = tick + 1; i < metadataName.Length; i++)
        {
            if (metadataName[i] is < '0' or > '9') return false;
        }

        return true;
    }
}
