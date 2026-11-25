using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>The equatable result of a scan.</summary>
/// <param name="Request">The request that produced it.</param>
/// <param name="Assemblies">The assemblies that passed the assembly filter, in a deterministic order.</param>
/// <param name="Types">For a types scan, the matched types in a deterministic order; empty for an assemblies scan.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed partial record ScanResult(ScanRequest Request, EquatableArray<ScannedAssembly> Assemblies, EquatableArray<ScannedType> Types)
{
    /// <summary>
    ///     For a types scan, each distinct <see cref="TypeReference" /> in the request's assignability and attribute clauses that did not
    ///     resolve in the scanned compilation, in the order they first appear (assignability clauses, then attribute clauses); empty
    ///     when everything resolved and for an assemblies scan. Report these as diagnostics: the scan treated each one as a type nothing
    ///     is assignable to and no type carries, so an include clause whose types all failed matched nothing, and an exclude clause
    ///     excluded nothing for it.
    /// </summary>
    /// <remarks>
    ///     A reference is unresolved when no such type exists (a misspelled name), its assembly is not referenced, one of its
    ///     <see cref="TypeReference.TypeArguments" /> does not resolve, or its name is defined in more than one referenced assembly and
    ///     <see cref="TypeReference.AssemblyName" /> is empty (<c>Compilation.GetTypeByMetadataName</c> returns null for ambiguous
    ///     names; select the symbol, or a reference naming its assembly, to disambiguate). It depends on the request and the
    ///     compilation, never on the build cache: a request with an unresolved reference is always scanned, never served from or
    ///     written to the cache.
    /// </remarks>
    public EquatableArray<TypeReference> UnresolvedTypes { get; init; } = EquatableArray<TypeReference>.Empty;

    /// <summary>Resolves the result back to symbols in <paramref name="compilation" />.</summary>
    /// <param name="compilation">The compilation to resolve in.</param>
    /// <returns>The symbols; entries that no longer resolve are skipped.</returns>
    public ScanSymbols Resolve(Compilation compilation) => ResolveCore(compilation);

    private partial ScanSymbols ResolveCore(Compilation compilation);
}
