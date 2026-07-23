using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Specular.Analyzers.Descriptors;

namespace Specular.Analyzers.AssemblyProviders;

internal sealed class CompiledAssemblyFilter(ImmutableList<IAssemblyDescriptor> assemblyDescriptors, SourceLocation? sourceLocation = null) : ICompiledTypeFilter<IAssemblySymbol>
{
    public ImmutableList<IAssemblyDescriptor> AssemblyDescriptors { get; } = assemblyDescriptors;

    internal static readonly HashSet<string> coreAssemblies =
    [
        "mscorlib",
        "netstandard",
        "System",
        "System.Core",
        "System.Runtime",
        "System.Private.CoreLib",
    ];

    private readonly bool _includeSystemAssemblies = assemblyDescriptors.OfType<IncludeSystemAssembliesDescriptor>().Any();

    public string Hash => sourceLocation?.ExpressionHash ?? Guid.NewGuid().ToString("N");

    public bool IsMatch(Compilation compilation, IAssemblySymbol targetSymbol)
    {
        if (!_includeSystemAssemblies && coreAssemblies.Contains(targetSymbol.Name)) return false;

        var referencedAssemblySymbols = targetSymbol
                                       .Modules
                                       .SelectMany(z => z.ReferencedAssemblySymbols)
                                       .ToImmutableHashSet(SymbolEqualityComparer.Default);

        return AssemblyDescriptors
           .All(filter => filter switch
                          {
                              AssemblyDescriptor { Assembly: var assembly, } => SymbolEqualityComparer.Default.Equals(assembly, targetSymbol),
                              NotAssemblyDescriptor { Assembly: var assembly, } => !SymbolEqualityComparer.Default.Equals(assembly, targetSymbol),
                              AssemblyDependenciesDescriptor { Assembly: var assembly, } => referencedAssemblySymbols.Contains(assembly),
                              AllAssemblyDescriptor => true,
                              IncludeSystemAssembliesDescriptor => true,
                              _ => false,
                          }
            );
    }
}
