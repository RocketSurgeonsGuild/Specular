#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
using System.Threading;
#endif
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Walks the types of an assembly: every namespace, then every type and its nested types, offering only types that can be
///     referenced by name. A type that can't be (compiler-generated, such as the <c>&lt;&gt;z__ReadOnlyArray&lt;T&gt;</c> collection
///     expressions emit) is skipped with everything nested in it: C# can't declare a type inside one, so whatever is nested there is
///     compiler-generated too, even when its own name is speakable (<c>&lt;&gt;z__ReadOnlySingleElementList&lt;T&gt;.Enumerator</c>).
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
        if (!type.CanBeReferencedByName) return true;
        if (!visit(type)) return false;

        foreach (var nested in type.GetTypeMembers())
        {
            if (!WalkType(nested, visit, cancellationToken)) return false;
        }

        return true;
    }
}
