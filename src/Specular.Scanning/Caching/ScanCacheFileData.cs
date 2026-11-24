using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

// The on-disk shape is decoupled from the public result records so either can evolve; bump ScanCacheFile.SchemaVersion on any change here.

/// <summary>A consumer's cache file.</summary>
/// <param name="Schema">The <see cref="ScanCacheFile.SchemaVersion" /> the file was written with.</param>
/// <param name="Scans">Request key → assembly metadata name → matched types.</param>
/// <param name="Store">Typed-store key → assembly metadata name → payload.</param>
internal sealed record ScanCacheFileData(
    int Schema,
    Dictionary<string, Dictionary<string, CachedScanEntry?>?>? Scans,
    Dictionary<string, Dictionary<string, CachedStoreEntry?>?>? Store
);

/// <summary>The types one request matched in one assembly.</summary>
/// <param name="Mvid">The assembly's MVID when the entry was written.</param>
/// <param name="Types">The matched types.</param>
internal sealed record CachedScanEntry(Guid Mvid, List<CachedType?>? Types);

/// <summary>A matched type.</summary>
/// <param name="Name">The full metadata name.</param>
/// <param name="Kind">The type kind.</param>
/// <param name="Access">The declared accessibility.</param>
/// <param name="Accessible">Whether the compilation can reference the type.</param>
/// <param name="Arity">The number of generic type parameters.</param>
/// <param name="Assembly">The containing assembly, written only when it differs from the entry's assembly.</param>
internal sealed record CachedType(string? Name, TypeKind Kind, Accessibility Access, bool Accessible, int Arity, string? Assembly);

/// <summary>One typed-store value for one assembly.</summary>
/// <param name="Mvid">The assembly's MVID when the entry was written.</param>
/// <param name="Version">The consumer's data version when the entry was written.</param>
/// <param name="Data">The payload, serialized with the consumer's <see cref="System.Text.Json.Serialization.Metadata.JsonTypeInfo{T}" />.</param>
internal sealed record CachedStoreEntry(Guid Mvid, int Version, JsonElement Data);

/// <summary>Source-generated serialization for the cache file, so no runtime reflection is involved.</summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
)]
[JsonSerializable(typeof(ScanCacheFileData))]
internal sealed partial class ScanCacheJsonContext : JsonSerializerContext;
