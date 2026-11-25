using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>Runs <see cref="ScanRequest" />s inside an incremental generator, with results cached across builds.</summary>
/// <remarks>
///     Call <c>Scan</c> once per consumer id in a generator, with all of its requests: the consumer id names the cache file
///     (<c>obj/ctp/&lt;consumerId&gt;.ctpjson</c>), and each call saves only the entries its own requests used, so two calls sharing an id
///     would keep overwriting each other's.
/// </remarks>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
static class ScanIncrementalExtensions
{
    /// <summary>
    ///     Scans every request against the compilation and returns one result per request. Referenced assemblies' results are reused
    ///     while the assembly is unchanged: in memory between runs, and across builds through the cache file, which builds save (the IDE
    ///     does not).
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in request order.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValuesProvider<ScanResult> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValuesProvider<ScanRequest> requests
    )
    {
        _ = ScanCacheFile.ValidateConsumerId(consumerId);

        var previousCacheFile = context
                               .AdditionalTextsProvider
                               .Where(text => ScanCacheFile.IsCacheFile(text.Path, consumerId))
                               .Select(static (text, cancellationToken) => text.GetText(cancellationToken)?.ToString())
                               .Collect()
                               .Select(static (texts, _) => ScanCacheFile.Parse(texts.FirstOrDefault()))
                               .WithTrackingName(ScanTrackingNames.CacheFile);

        var run = requests
                 .Collect()
                 .Combine(context.CompilationProvider)
                 .Combine(previousCacheFile)
                 .Select((input, cancellationToken) => RunScans(consumerId, input.Left.Left, input.Left.Right, input.Right, cancellationToken))
                 .WithTrackingName(ScanTrackingNames.Run);

        // Implementation-only, as Specular's own cache: the IDE never writes into obj/, and only builds read the file back anyway.
        context.RegisterImplementationSourceOutput(
            run.Combine(context.AnalyzerConfigOptionsProvider),
            (_, input) => ScanCacheWriter.Write(input.Right.GlobalOptions, consumerId, input.Left.CacheContent)
        );

        return run.SelectMany(static (scanRun, _) => scanRun.Results.AsImmutableArray()).WithTrackingName(ScanTrackingNames.Results);
    }

    /// <summary>Scans a single request; see the other overload, which a generator with several requests should use instead.</summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request">The request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The result.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<ScanResult> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request
    ) =>
        context
           .Scan(consumerId, request.SelectMany(static (scanRequest, _) => ImmutableArray.Create(scanRequest)))
           .Collect()
           .Select(static (results, _) => results[0]);

    private static ScanRun RunScans(
        string consumerId,
        ImmutableArray<ScanRequest> requests,
        Compilation compilation,
        ScanCacheFileData previousCacheFile,
        CancellationToken cancellationToken
    )
    {
        var diskCache = new ScanCache(consumerId, compilation, previousCacheFile);
        var cache = new MemoizingScanCache(compilation, diskCache);

        // A request listed twice is scanned once; every listing still gets its result, so results line up with requests.
        var scanned = new Dictionary<ScanRequest, ScanResult>();
        var results = ImmutableArray.CreateBuilder<ScanResult>(requests.Length);
        foreach (var request in requests)
        {
            if (!scanned.TryGetValue(request, out var result))
            {
                result = Scanner.Scan(compilation, request, cache, cancellationToken);
                scanned[request] = result;
            }

            results.Add(result);
        }

        return new(results.MoveToImmutable(), diskCache.Serialize());
    }
}
