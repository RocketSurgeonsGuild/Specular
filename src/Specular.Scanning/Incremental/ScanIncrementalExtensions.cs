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
///     (<c>obj/ctp/&lt;consumerId&gt;.ctpjson</c>), and each call saves only the entries its own requests (and its transform) used, so
///     two calls sharing an id would keep overwriting each other's.
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
    ) =>
        context
           .Scan(consumerId, requests, static scan => scan.Results)
           .SelectMany(static (results, _) => results.AsImmutableArray())
           .WithTrackingName(ScanTrackingNames.Results);

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
        context.Scan(consumerId, request.SelectMany(static (scanRequest, _) => ImmutableArray.Create(scanRequest)), static scan => scan.Results[0]);

    /// <summary>
    ///     Scans every request, then hands the results to <paramref name="transform" /> in the same step, with the run's
    ///     <see cref="ScanCache" /> for values of your own derived per referenced assembly. Scans and those values share one consumer id
    ///     and one cache file, and both are reused while the assembly is unchanged: in memory between runs, and across builds through the
    ///     file.
    /// </summary>
    /// <typeparam name="TOutput">
    ///     What the generator needs from the run. It must be equatable by value (a record of strings, numbers and
    ///     <see cref="EquatableArray{T}" />s, for example), so the steps after it rerun only when it changes; it must never hold symbols, the
    ///     compilation or the <see cref="ScanContext" />.
    /// </typeparam>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="transform">
    ///     Reduces the run to <typeparamref name="TOutput" />. It runs after every change to the compilation or the requests, so keep its
    ///     own work per referenced assembly behind the cache. The <see cref="ScanContext" /> is valid only while it runs.
    /// </param>
    /// <returns>The transform's output.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="transform" /> is null.</exception>
    public static IncrementalValueProvider<TOutput> Scan<TOutput>(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValuesProvider<ScanRequest> requests,
        Func<ScanContext, TOutput> transform
    )
    {
        _ = ScanCacheFile.ValidateConsumerId(consumerId);
        if (transform is null) throw new ArgumentNullException(nameof(transform));

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
                 .Select((input, cancellationToken) => Run(consumerId, input.Left.Left, input.Left.Right, input.Right, transform, cancellationToken))
                 .WithTrackingName(ScanTrackingNames.Run);

        // Implementation-only, as Specular's own cache: the IDE never writes into obj/, and only builds read the file back anyway.
        context.RegisterImplementationSourceOutput(
            run.Combine(context.AnalyzerConfigOptionsProvider),
            (_, input) => ScanCacheWriter.Write(input.Right.GlobalOptions, consumerId, input.Left.CacheContent)
        );

        return run.Select(static (scanRun, _) => scanRun.Output).WithTrackingName(ScanTrackingNames.Output);
    }

    private static ScanTransformRun<TOutput> Run<TOutput>(
        string consumerId,
        ImmutableArray<ScanRequest> requests,
        Compilation compilation,
        ScanCacheFileData previousCacheFile,
        Func<ScanContext, TOutput> transform,
        CancellationToken cancellationToken
    )
    {
        var diskCache = new ScanCache(consumerId, compilation, previousCacheFile, new MemoizingStore(compilation, consumerId));
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

        var output = transform(new(compilation, results.MoveToImmutable(), diskCache, cancellationToken));
        return new(output, diskCache.Serialize());
    }
}
