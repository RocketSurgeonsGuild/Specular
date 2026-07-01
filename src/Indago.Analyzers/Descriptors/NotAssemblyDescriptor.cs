using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record NotAssemblyDescriptor(IAssemblySymbol Assembly) : IAssemblyDescriptor
{
    public override string ToString() => "CompiledAssembly of " + Assembly.Name;
}
