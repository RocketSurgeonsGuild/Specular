using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>Resolves the module version id (MVID) that keys the build cache for a referenced assembly.</summary>
internal static class ModuleVersionIds
{
    /// <summary>
    ///     The MVID of <paramref name="assembly" />'s manifest module, or null when it is not cacheable: anything but a
    ///     <see cref="PortableExecutableReference" /> with readable metadata, including the compilation's own assembly and IDE
    ///     <see cref="CompilationReference" />s, whose identity rarely changes between edits (risk R1).
    /// </summary>
    public static Guid? Get(Compilation compilation, IAssemblySymbol assembly)
    {
        if (SymbolEqualityComparer.Default.Equals(assembly, compilation.Assembly))
        {
            return null;
        }

        try
        {
            if (compilation.GetMetadataReference(assembly) is not PortableExecutableReference reference)
            {
                return null;
            }

            var mvid = reference.GetMetadata() switch
            {
                AssemblyMetadata { } metadata when metadata.GetModules() is { Length: > 0 } modules => modules[0].GetModuleVersionId(),
                ModuleMetadata module => module.GetModuleVersionId(),
                _ => Guid.Empty,
            };
            return mvid == Guid.Empty ? null : mvid;
        }
#pragma warning disable CA1031 // A reference whose metadata cannot be read is simply not cacheable; the cache must never fail a build.
        catch (Exception)
#pragma warning restore CA1031
        {
            return null;
        }
    }
}
