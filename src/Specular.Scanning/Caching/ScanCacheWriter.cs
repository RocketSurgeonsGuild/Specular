using System;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Specular.Scanning;

// RS1035 bans file IO in analyzers. Writing the cache back to obj/ is the one deliberate exception; keep all IO in this file.
#pragma warning disable RS1035

/// <summary>Writes consumer cache files to <c>$(IntermediateOutputPath)ctp/</c>, where the build feeds them back as additional files.</summary>
internal static class ScanCacheWriter
{
    private static readonly UTF8Encoding Utf8NoBom = new(false);

    /// <summary>
    ///     The cache directory, from the <c>build_property.IntermediateOutputPath</c> (made absolute with <c>build_property.ProjectDir</c>)
    ///     global options; null when they do not give an absolute path, e.g. outside MSBuild.
    /// </summary>
    public static string? GetCacheDirectory(AnalyzerConfigOptions globalOptions)
    {
        if (!globalOptions.TryGetValue("build_property.IntermediateOutputPath", out var directory) || string.IsNullOrWhiteSpace(directory))
        {
            return null;
        }

        if (!Path.IsPathRooted(directory))
        {
            // A relative path would resolve against the compiler server's working directory, not the project.
            if (!globalOptions.TryGetValue("build_property.ProjectDir", out var projectDirectory)
             || string.IsNullOrWhiteSpace(projectDirectory)
             || !Path.IsPathRooted(projectDirectory))
            {
                return null;
            }

            directory = Path.Combine(projectDirectory, directory);
        }

        return Path.Combine(directory, "ctp");
    }

    /// <summary>
    ///     Writes <paramref name="content" /> as <paramref name="consumerId" />'s cache file, unless the file already has exactly that
    ///     content: rewriting it would change its timestamp and make the build treat the project as out of date.
    /// </summary>
    /// <returns>Whether the file was written. IO failures are swallowed: a cache must never fail a build.</returns>
    /// <exception cref="ArgumentException">The consumer id is invalid.</exception>
    public static bool Write(AnalyzerConfigOptions globalOptions, string consumerId, string content)
    {
        var fileName = ScanCacheFile.GetFileName(consumerId);
        if (GetCacheDirectory(globalOptions) is not { } directory)
        {
            return false;
        }

        try
        {
            var path = Path.Combine(directory, fileName);
            if (File.Exists(path) && string.Equals(File.ReadAllText(path, Utf8NoBom), content, StringComparison.Ordinal))
            {
                return false;
            }

            _ = Directory.CreateDirectory(directory);
            File.WriteAllText(path, content, Utf8NoBom);
            return true;
        }
#pragma warning disable CA1031 // Any IO failure (locked file, permissions, path under a file) only costs a cache miss next build.
        catch (Exception)
#pragma warning restore CA1031
        {
            return false;
        }
    }
}

#pragma warning restore RS1035
