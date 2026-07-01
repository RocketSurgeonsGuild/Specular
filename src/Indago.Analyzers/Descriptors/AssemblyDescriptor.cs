using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record AssemblyDescriptor(IAssemblySymbol Assembly) : IAssemblyDescriptor
{
    public override string ToString() => "Assembly: " + Assembly.Name;
}
