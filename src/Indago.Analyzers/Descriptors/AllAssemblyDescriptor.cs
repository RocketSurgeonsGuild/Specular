using System.Diagnostics;

namespace Indago.Analyzers.Descriptors;

[DebuggerDisplay("{ToString()}")]
internal record AllAssemblyDescriptor : IAssemblyDescriptor
{
    public override string ToString() => "All";
}
