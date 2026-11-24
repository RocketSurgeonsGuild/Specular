namespace Specular.Scanning;

/// <summary>A type named by metadata, so requests stay equatable and serializable without holding symbols.</summary>
/// <param name="AssemblyName">The containing assembly's metadata name, or empty to resolve with <c>Compilation.GetTypeByMetadataName</c>.</param>
/// <param name="MetadataName">The full metadata name, e.g. <c>MyLib.IHandler`1</c>.</param>
/// <param name="UnboundGeneric">Whether the type is an unbound (open) generic such as <c>IHandler&lt;&gt;</c>.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record TypeReference(string AssemblyName, string MetadataName, bool UnboundGeneric);
