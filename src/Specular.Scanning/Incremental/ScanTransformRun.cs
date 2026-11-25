namespace Specular.Scanning;

/// <summary>One pipeline run: what the transform made of the scans, and the cache file content the run leaves behind.</summary>
internal sealed record ScanTransformRun<TOutput>(TOutput Output, string CacheContent);
