#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
#endif
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

// One overload per tuple size, 2 to 8: the most a tuple type declares directly (the eighth element goes through ValueTuple's TRest).
// Each flattens the tuple into the request list and rebuilds the results in the same positions; keep them identical but for size.
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
static partial class ScanIncrementalExtensions
{

    /// <summary>
    ///     Scans 2 requests together and returns their results in the same positions, so each can be named where it's read:
    ///     <c>var (first, second) = results;</c>. The requests share one run and one cache file, as with the overload
    ///     taking a list of requests; a request that appears twice is scanned once.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, position for position; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<(ScanRequest, ScanRequest)> requests
    ) =>
        context.Scan(
            consumerId,
            requests.SelectMany(static (scanRequests, _) => ImmutableArray.Create(scanRequests.Item1, scanRequests.Item2)),
            static scan => (scan.Results[0], scan.Results[1])
        );

    /// <summary>
    ///     Scans 3 requests together and returns their results in the same positions, so each can be named where it's read:
    ///     <c>var (first, second, third) = results;</c>. The requests share one run and one cache file, as with the overload
    ///     taking a list of requests; a request that appears twice is scanned once.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, position for position; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<(ScanRequest, ScanRequest, ScanRequest)> requests
    ) =>
        context.Scan(
            consumerId,
            requests.SelectMany(static (scanRequests, _) => ImmutableArray.Create(scanRequests.Item1, scanRequests.Item2, scanRequests.Item3)),
            static scan => (scan.Results[0], scan.Results[1], scan.Results[2])
        );

    /// <summary>
    ///     Scans 4 requests together and returns their results in the same positions, so each can be named where it's read:
    ///     <c>var (first, second, third, fourth) = results;</c>. The requests share one run and one cache file, as with the overload
    ///     taking a list of requests; a request that appears twice is scanned once.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, position for position; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<(ScanRequest, ScanRequest, ScanRequest, ScanRequest)> requests
    ) =>
        context.Scan(
            consumerId,
            requests.SelectMany(static (scanRequests, _) => ImmutableArray.Create(scanRequests.Item1, scanRequests.Item2, scanRequests.Item3, scanRequests.Item4)),
            static scan => (scan.Results[0], scan.Results[1], scan.Results[2], scan.Results[3])
        );

    /// <summary>
    ///     Scans 5 requests together and returns their results in the same positions, so each can be named where it's read:
    ///     <c>var (first, second, third, fourth, fifth) = results;</c>. The requests share one run and one cache file, as with the overload
    ///     taking a list of requests; a request that appears twice is scanned once.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, position for position; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<(ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest)> requests
    ) =>
        context.Scan(
            consumerId,
            requests.SelectMany(static (scanRequests, _) => ImmutableArray.Create(scanRequests.Item1, scanRequests.Item2, scanRequests.Item3, scanRequests.Item4, scanRequests.Item5)),
            static scan => (scan.Results[0], scan.Results[1], scan.Results[2], scan.Results[3], scan.Results[4])
        );

    /// <summary>
    ///     Scans 6 requests together and returns their results in the same positions, so each can be named where it's read:
    ///     <c>var (first, second, third, fourth, fifth, sixth) = results;</c>. The requests share one run and one cache file, as with the overload
    ///     taking a list of requests; a request that appears twice is scanned once.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, position for position; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<(ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest)> requests
    ) =>
        context.Scan(
            consumerId,
            requests.SelectMany(static (scanRequests, _) => ImmutableArray.Create(scanRequests.Item1, scanRequests.Item2, scanRequests.Item3, scanRequests.Item4, scanRequests.Item5, scanRequests.Item6)),
            static scan => (scan.Results[0], scan.Results[1], scan.Results[2], scan.Results[3], scan.Results[4], scan.Results[5])
        );

    /// <summary>
    ///     Scans 7 requests together and returns their results in the same positions, so each can be named where it's read:
    ///     <c>var (first, second, third, fourth, fifth, sixth, seventh) = results;</c>. The requests share one run and one cache file, as with the overload
    ///     taking a list of requests; a request that appears twice is scanned once.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, position for position; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<(ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest)> requests
    ) =>
        context.Scan(
            consumerId,
            requests.SelectMany(static (scanRequests, _) => ImmutableArray.Create(scanRequests.Item1, scanRequests.Item2, scanRequests.Item3, scanRequests.Item4, scanRequests.Item5, scanRequests.Item6, scanRequests.Item7)),
            static scan => (scan.Results[0], scan.Results[1], scan.Results[2], scan.Results[3], scan.Results[4], scan.Results[5], scan.Results[6])
        );

    /// <summary>
    ///     Scans 8 requests together and returns their results in the same positions, so each can be named where it's read:
    ///     <c>var (first, second, third, fourth, fifth, sixth, seventh, eighth) = results;</c>. The requests share one run and one cache file, as with the overload
    ///     taking a list of requests; a request that appears twice is scanned once.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="requests">The requests, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, position for position; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<(ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest, ScanRequest)> requests
    ) =>
        context.Scan(
            consumerId,
            requests.SelectMany(static (scanRequests, _) => ImmutableArray.Create(scanRequests.Item1, scanRequests.Item2, scanRequests.Item3, scanRequests.Item4, scanRequests.Item5, scanRequests.Item6, scanRequests.Item7, scanRequests.Item8)),
            static scan => (scan.Results[0], scan.Results[1], scan.Results[2], scan.Results[3], scan.Results[4], scan.Results[5], scan.Results[6], scan.Results[7])
        );
}
