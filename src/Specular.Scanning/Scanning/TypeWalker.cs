#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
using System.Threading;
#endif
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Walks the types of an assembly the way Specular's <c>TypeSymbolVisitorBase</c> does: every namespace, then every type and its
///     nested types, offering only types that can be referenced by name (compiler-generated types are skipped, their nested types
///     are still walked).
/// </summary>
internal static class TypeWalker
{
    /// <summary>Offers each type to <paramref name="visit" />, in declaration order, until it returns false.</summary>
    /// <returns>False when <paramref name="visit" /> stopped the walk.</returns>
    public static bool Walk(INamespaceSymbol @namespace, Func<INamedTypeSymbol, bool> visit, CancellationToken cancellationToken)
    {
        foreach (var member in @namespace.GetMembers())
        {
            var keepGoing = member switch
            {
                INamespaceSymbol childNamespace => Walk(childNamespace, visit, cancellationToken),
                INamedTypeSymbol type => WalkType(type, visit, cancellationToken),
                _ => true,
            };
            if (!keepGoing) return false;
        }

        return true;
    }

    private static bool WalkType(INamedTypeSymbol type, Func<INamedTypeSymbol, bool> visit, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (type.CanBeReferencedByName && !visit(type)) return false;

        foreach (var nested in type.GetTypeMembers())
        {
            if (!WalkType(nested, visit, cancellationToken)) return false;
        }

        return true;
    }
}
