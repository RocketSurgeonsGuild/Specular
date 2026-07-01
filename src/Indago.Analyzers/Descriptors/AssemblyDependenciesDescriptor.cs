using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record AssemblyDependenciesDescriptor(IAssemblySymbol Assembly) : IAssemblyDescriptor
{
    public override string ToString() => "CompiledAssemblyDependencies of " + Assembly.Name;
}
