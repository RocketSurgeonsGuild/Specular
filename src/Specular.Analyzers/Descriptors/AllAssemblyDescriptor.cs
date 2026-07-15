using System.Diagnostics;

namespace Specular.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal sealed record AllAssemblyDescriptor : IAssemblyDescriptor
{
    public override string ToString() => "All";
}
