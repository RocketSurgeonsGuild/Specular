using System.Collections;
using System.Runtime.CompilerServices;
using DiffEngine;

namespace Specular.Scanning.Tests;

internal static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        // Snapshots show what changed in a review; the request key has its own pinned test, and would otherwise change every
        // snapshot whenever its format moves on.
        VerifierSettings.IgnoreMember<ScanRequest>(static z => z.CacheKey);
        // A result's request is the scenario's own input; its snapshot is about what the scan found.
        VerifierSettings.IgnoreMember<ScanResult>(static z => z.Request);
        // Module version ids in cache files change on every emit.
        VerifierSettings.ScrubInlineGuids();

        // EquatableArray is a struct, which Verify's empty-collection handling printed as null. With that handling off, the converter
        // writes each one as the array it wraps, and empty ones are left out of the snapshot like any empty collection.
        VerifierSettings.DontIgnoreEmptyCollections();
        VerifierSettings.AddExtraSettings(static settings => settings.Converters.Add(new EquatableArrayConverter()));

        DiffRunner.Disabled = true;
        DerivePathInfo(static (_, projectDirectory, type, method) => new(Path.Combine(projectDirectory, "snapshots"), type.Name, method.Name));
    }

    private sealed class EquatableArrayConverter : WriteOnlyJsonConverter
    {
        public override bool CanConvert(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(EquatableArray<>);

        public override void Write(VerifyJsonWriter writer, object value) => writer.Serialize(( (IEnumerable)value ).Cast<object>().ToArray());
    }
}
