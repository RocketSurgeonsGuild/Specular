using System.Diagnostics;

namespace Specular.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record IncludeSystemAssembliesDescriptor : IAssemblyDescriptor
{
    public override string ToString() => "IncludeSystemAssemblies";
}
