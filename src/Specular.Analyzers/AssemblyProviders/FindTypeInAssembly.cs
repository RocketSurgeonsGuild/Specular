using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Specular.Analyzers.Descriptors;

namespace Specular.Analyzers.AssemblyProviders;

internal sealed class FindTypeInAssembly(Compilation compilation, ICompiledTypeFilter<IAssemblySymbol> assemblyFilter) : TypeSymbolVisitorBase(
    compilation,
    assemblyFilter,
    new CompiledTypeFilter(ClassFilter.PublicOnly, ImmutableList<ITypeFilterDescriptor>.Empty)
)
{
    public static INamedTypeSymbol? FindType(Compilation compilation, IAssemblySymbol assemblySymbol)
    {
        var visitor = new FindTypeInAssembly(
            compilation,
            new CompiledAssemblyFilter(ImmutableList.Create<IAssemblyDescriptor>(new AssemblyDescriptor(assemblySymbol)))
        );
        visitor.Visit(assemblySymbol);
        return visitor._type;
    }

    private INamedTypeSymbol? _type;

    protected override bool FoundNamedType(INamedTypeSymbol symbol)
    {
        // avoid attributes that can kill us with compiler errors.
        if (symbol.GetAttributes().Any(z => z is { AttributeClass.MetadataName: "ObsoleteAttribute" or "ExperimentalAttribute" })) return false;
        _type = symbol;
        return true;
    }
}
