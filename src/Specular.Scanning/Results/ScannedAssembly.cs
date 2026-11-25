namespace Specular.Scanning;

/// <summary>An assembly a scan looked in.</summary>
/// <param name="Name">The assembly's metadata name.</param>
/// <param name="HasInaccessibleMatches">Whether any matched type in it is not accessible from the compilation.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record ScannedAssembly(string Name, bool HasInaccessibleMatches);
