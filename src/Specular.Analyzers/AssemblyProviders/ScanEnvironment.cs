using Microsoft.CodeAnalysis;
using Specular.Scanning;

namespace Specular.Analyzers.AssemblyProviders;

/// <summary>What one generator run scans against: the compilation and every assembly it can see, found the way Specular.Scanning finds them.</summary>
internal sealed class ScanEnvironment
{
    public ScanEnvironment(Compilation compilation)
    {
        Compilation = compilation;
        AllAssemblies = ScanAssemblies.ByName(ScanAssemblies.Get(compilation));
        Resolver = new(compilation, AllAssemblies);
    }

    public Compilation Compilation { get; }

    /// <summary>
    ///     Every assembly by metadata name, including those <c>ExcludeAssemblyFromCTP</c> leaves out of the scan: a selector written in
    ///     source names its assemblies and types by symbol, so it can still refer to them.
    /// </summary>
    public IReadOnlyDictionary<string, IAssemblySymbol> AllAssemblies { get; }

    /// <summary>Resolves type references taken from symbols in this compilation's source.</summary>
    public TypeReferenceResolver Resolver { get; }
}
