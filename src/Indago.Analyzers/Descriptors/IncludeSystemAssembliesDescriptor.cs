using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record IncludeSystemAssembliesDescriptor : IAssemblyDescriptor
{
    public override string ToString() => "IncludeSystemAssemblies";
}
