using System.Text.Json.Serialization;

namespace Specular.Analyzers.Configuration;

// Serializes the [AssemblyMetadata] scan requests Specular writes into each assembly, the content hash, and Specular's entries in the
// build cache (the typed store of Specular.Scanning's ScanCache). Source-generated, so no runtime reflection is involved.
[JsonSourceGenerationOptions]
[JsonSerializable(typeof(AssemblyCollectionData))]
[JsonSerializable(typeof(ReflectionCollectionData))]
[JsonSerializable(typeof(ServiceDescriptorCollectionData))]
[JsonSerializable(typeof(AssemblyFilterData))]
[JsonSerializable(typeof(SourceLocation))]
[JsonSerializable(typeof(TypeFilterData))]
[JsonSerializable(typeof(NamespaceFilterData))]
[JsonSerializable(typeof(NameFilterData))]
[JsonSerializable(typeof(TypeKindFilterData))]
[JsonSerializable(typeof(TypeInfoFilterData))]
[JsonSerializable(typeof(WithAttributeData))]
[JsonSerializable(typeof(WithAttributeStringData))]
[JsonSerializable(typeof(AssignableToTypeData))]
[JsonSerializable(typeof(AssignableToAnyTypeData))]
[JsonSerializable(typeof(CompiledAssemblyProviderData))]
[JsonSerializable(typeof(GetAssemblyConfiguration))]
[JsonSerializable(typeof(GetReflectionCollectionData))]
[JsonSerializable(typeof(GetServiceDescriptorCollectionData))]
[JsonSerializable(typeof(ResolvedSourceLocation))]
[JsonSerializable(typeof(ScanReportTypeData))]
[JsonSerializable(typeof(ServiceDescriptorScanReportEntryData))]
internal sealed partial class JsonSourceGenerationContext : JsonSerializerContext;
