using System;
using System.IO;
using System.Text.Json;

namespace Specular.Scanning;

/// <summary>Names, parses and serializes consumer cache files (<c>&lt;ConsumerId&gt;.ctpjson</c>).</summary>
internal static class ScanCacheFile
{
    /// <summary>The cache file format version; a file written with any other version is ignored and rebuilt.</summary>
    public const int SchemaVersion = 1;

    /// <summary>The cache file extension.</summary>
    public const string Extension = ".ctpjson";

    /// <summary>A file with no entries, shared because readers never mutate a parsed file.</summary>
    public static ScanCacheFileData Empty { get; } = new(SchemaVersion, [], []);

    /// <summary>The cache file name for <paramref name="consumerId" />.</summary>
    /// <exception cref="ArgumentException">The consumer id is empty or has characters other than letters, digits, '.', '_' and '-'.</exception>
    public static string GetFileName(string consumerId) => ValidateConsumerId(consumerId) + Extension;

    /// <summary>Whether <paramref name="path" /> names <paramref name="consumerId" />'s cache file.</summary>
    public static bool IsCacheFile(string? path, string consumerId) =>
        string.Equals(Path.GetFileName(path), GetFileName(consumerId), StringComparison.OrdinalIgnoreCase);

    /// <summary>
    ///     Throws unless <paramref name="consumerId" /> is a non-empty run of ASCII letters, digits, '.', '_' and '-': it becomes a
    ///     file name, so anything that could escape the cache directory or differ across file systems is refused.
    /// </summary>
    public static string ValidateConsumerId(string consumerId)
    {
        if (string.IsNullOrEmpty(consumerId))
        {
            throw new ArgumentException("A consumer id must not be empty.", nameof(consumerId));
        }

        foreach (var c in consumerId)
        {
            if (c is not ( >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9' or '.' or '_' or '-' ))
            {
                throw new ArgumentException(
                    $"The consumer id '{consumerId}' may only contain ASCII letters, digits, '.', '_' and '-'.",
                    nameof(consumerId)
                );
            }
        }

        return consumerId;
    }

    /// <summary>Parses a cache file; anything unreadable, including another schema version, is an empty file.</summary>
    public static ScanCacheFileData Parse(string? text)
    {
        if (text is null || string.IsNullOrWhiteSpace(text))
        {
            return Empty;
        }

        try
        {
            return JsonSerializer.Deserialize(text, ScanCacheJsonContext.Default.ScanCacheFileData) is { Schema: SchemaVersion } data
                ? data with { Scans = data.Scans ?? [], Store = data.Store ?? [] }
                : Empty;
        }
#pragma warning disable CA1031 // A corrupt cache file is ignored and rebuilt; it must never fail a build.
        catch (Exception)
#pragma warning restore CA1031
        {
            return Empty;
        }
    }

    /// <summary>Serializes a cache file compactly.</summary>
    public static string Serialize(ScanCacheFileData data) => JsonSerializer.Serialize(data, ScanCacheJsonContext.Default.ScanCacheFileData);
}
