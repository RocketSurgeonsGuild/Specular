using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

/// <summary>
///     The scan requests a referenced assembly carries in its <c>[AssemblyMetadata]</c> attributes, kept in the build cache so the next
///     build does not decode them again. The cache ties it to the assembly's MVID.
/// </summary>
internal sealed record CompiledAssemblyProviderData
(
    ImmutableList<GetAssemblyConfiguration> InternalAssemblyRequests,
    ImmutableList<GetReflectionCollectionData> InternalReflectionRequests,
    ImmutableList<GetServiceDescriptorCollectionData> InternalServiceDescriptorRequests,
    bool ExcludeFromResolution
)
{
    [JsonIgnore]
    public bool IsEmpty =>
        InternalAssemblyRequests.IsEmpty
     && InternalReflectionRequests.IsEmpty
     && InternalServiceDescriptorRequests.IsEmpty;
}
