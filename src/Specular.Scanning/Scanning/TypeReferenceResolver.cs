#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
using System.Collections.Generic;
using System.Linq;
#endif
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Resolves <see cref="TypeReference" />s back to symbols the way Specular's <c>AssemblyProviderConfiguration.findType</c> does:
///     a public type found in the named assembly, otherwise <c>Compilation.GetTypeByMetadataName</c> (which returns null for a name
///     defined in more than one referenced assembly). A reference with type arguments resolves to that closed type.
/// </summary>
internal sealed class TypeReferenceResolver(Compilation compilation, IReadOnlyDictionary<string, IAssemblySymbol> assemblies)
{
    /// <summary>
    ///     The symbol: in its unbound form when the reference asks for one, constructed when it has type arguments; null when it, or
    ///     any of its type arguments, does not resolve.
    /// </summary>
    public INamedTypeSymbol? Resolve(TypeReference reference)
    {
        if (Find(reference) is not { } type) return null;

        if (reference.TypeArguments.Count > 0)
        {
            // Unbound and closed at once has no meaning; reporting it beats guessing which one was intended.
            return reference.UnboundGeneric ? null : Construct(type, reference.TypeArguments);
        }

        // Specular would throw constructing an unbound form of a non-generic type; the plain type is the closest meaning.
        return reference.UnboundGeneric && type is { IsGenericType: true, IsUnboundGenericType: false } ? type.ConstructUnboundGenericType() : type;
    }

    private INamedTypeSymbol? Find(TypeReference reference)
    {
        // Specular resolves core-assembly types through the compilation, since its assembly visitors skip core assemblies.
        if (reference.AssemblyName.Length == 0
         || AssemblyMatcher.IsCoreAssembly(reference.AssemblyName)
         || !assemblies.TryGetValue(reference.AssemblyName, out var assembly))
        {
            return compilation.GetTypeByMetadataName(reference.MetadataName);
        }

        // Specular's FindTypeVisitor only finds public types; anything else falls through to the compilation lookup.
        return ScanSymbolNames.FindType(assembly, reference.MetadataName) is { DeclaredAccessibility: Accessibility.Public } type
            ? type
            : compilation.GetTypeByMetadataName(reference.MetadataName);
    }

    // Arguments are listed outermost containing type first, so Outer`1+Inner`1 with [int, string] is Outer<int>.Inner<string>: each
    // level is looked up in the already-constructed level above it and constructed with its own share of the arguments.
    private INamedTypeSymbol? Construct(INamedTypeSymbol definition, EquatableArray<TypeReference> typeArguments)
    {
        var levels = ContainingTypesAndSelf(definition);
        if (levels.Sum(static level => level.Arity) != typeArguments.Count) return null;

        var arguments = new ITypeSymbol[typeArguments.Count];
        for (var i = 0; i < arguments.Length; i++)
        {
            if (ResolveArgument(typeArguments[i]) is not { } argument) return null;
            arguments[i] = argument;
        }

        INamedTypeSymbol? constructed = null;
        var next = 0;
        foreach (var level in levels)
        {
            var member = constructed is null ? level : SingleOrNull(constructed.GetTypeMembers(level.Name, level.Arity));
            if (member is null) return null;

            if (level.Arity > 0)
            {
                var levelArguments = new ITypeSymbol[level.Arity];
                Array.Copy(arguments, next, levelArguments, 0, level.Arity);
                member = member.Construct(levelArguments);
                next += level.Arity;
            }

            constructed = member;
        }

        return constructed;
    }

    // An argument must be closed all the way down: a definition still has its own type parameters and an unbound form has none at
    // all, so constructing with either would not name a type any scanned type could be assignable to.
    private INamedTypeSymbol? ResolveArgument(TypeReference reference)
    {
        return reference.UnboundGeneric || Resolve(reference) is not { } argument
            ? null
            : reference.TypeArguments.Count == 0 && ContainingTypesAndSelf(argument).Exists(static level => level.Arity > 0) ? null : argument;
    }

    private static List<INamedTypeSymbol> ContainingTypesAndSelf(INamedTypeSymbol type)
    {
        var levels = new List<INamedTypeSymbol>();
        for (var level = type; level is { }; level = level.ContainingType)
        {
            levels.Insert(0, level);
        }

        return levels;
    }

    private static INamedTypeSymbol? SingleOrNull(ImmutableArray<INamedTypeSymbol> types) => types.Length == 1 ? types[0] : null;
}
