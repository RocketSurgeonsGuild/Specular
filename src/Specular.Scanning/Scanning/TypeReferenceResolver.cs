using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>
///     Resolves <see cref="TypeReference" />s back to symbols the way Specular's <c>AssemblyProviderConfiguration.findType</c> does:
///     a public type found in the named assembly, otherwise <c>Compilation.GetTypeByMetadataName</c>.
/// </summary>
internal sealed class TypeReferenceResolver(Compilation compilation, IReadOnlyDictionary<string, IAssemblySymbol> assemblies)
{
    /// <summary>The symbol, in its unbound form when the reference asks for one; null when it does not resolve.</summary>
    public INamedTypeSymbol? Resolve(TypeReference reference)
    {
        if (Find(reference) is not { } type) return null;

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
}
