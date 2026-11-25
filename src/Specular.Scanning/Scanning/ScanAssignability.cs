#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System.Collections.Generic;
#endif
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>Assignability as Specular decides it, ported unchanged from <c>Specular.Analyzers</c>' <c>Helpers.HasImplicitGenericConversion</c>.</summary>
internal static class ScanAssignability
{
    /// <summary>
    ///     Whether <paramref name="assignableFromType" /> is assignable to <paramref name="assignableToType" />; an unbound generic
    ///     target matches any closed form through the type itself, its base types or its interfaces. <c>object</c> never matches.
    /// </summary>
    public static bool HasImplicitGenericConversion(Compilation compilation, INamedTypeSymbol assignableToType, INamedTypeSymbol assignableFromType)
    {
        if (SymbolEqualityComparer.Default.Equals(compilation.ObjectType, assignableToType)) return false;

        if (SymbolEqualityComparer.Default.Equals(compilation.ObjectType, assignableFromType)) return false;

        if (SymbolEqualityComparer.Default.Equals(assignableToType, assignableFromType)) return true;

        if (compilation.HasImplicitConversion(assignableFromType, assignableToType)) return true;

        if (assignableToType is not { Arity: > 0, IsUnboundGenericType: true }) return false;

        if (GetUnboundGenericType(assignableFromType) is { } unboundAssignableFromType
         && compilation.HasImplicitConversion(assignableToType, unboundAssignableFromType))
        {
            return true;
        }

        foreach (var baseType in GetBaseTypes(compilation, assignableFromType))
        {
            if (GetUnboundGenericType(baseType) is { } unbound && compilation.HasImplicitConversion(unbound, assignableToType)) return true;
        }

        foreach (var @interface in assignableFromType.AllInterfaces)
        {
            if (GetUnboundGenericType(@interface) is { } unbound && compilation.HasImplicitConversion(unbound, assignableToType)) return true;
        }

        return false;
    }

    private static IEnumerable<INamedTypeSymbol> GetBaseTypes(Compilation compilation, INamedTypeSymbol namedTypeSymbol)
    {
        while (namedTypeSymbol.BaseType is { })
        {
            if (SymbolEqualityComparer.Default.Equals(namedTypeSymbol.BaseType, compilation.ObjectType)) yield break;

            yield return namedTypeSymbol.BaseType;
            namedTypeSymbol = namedTypeSymbol.BaseType;
        }
    }

    private static INamedTypeSymbol? GetUnboundGenericType(INamedTypeSymbol symbol) => symbol switch
    {
        { IsGenericType: true, IsUnboundGenericType: true } => symbol,
        { IsGenericType: true } => symbol.ConstructUnboundGenericType(),
        _ => null,
    };
}
