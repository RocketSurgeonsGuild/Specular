namespace Specular.Scanning;

/// <summary>One pipeline run: every request's result, and the cache file content those scans leave behind.</summary>
internal sealed record ScanRun(EquatableArray<ScanResult> Results, string CacheContent);
