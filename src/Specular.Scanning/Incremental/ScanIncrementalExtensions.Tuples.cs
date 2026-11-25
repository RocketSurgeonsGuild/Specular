#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
#endif
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

// Two sets of overloads, each for 2 to 8 requests (the most a tuple type declares directly; the eighth element goes through
// ValueTuple's TRest). The first takes a tuple of requests, flattens it into the request list and rebuilds the results in the same
// positions; the second takes separate providers and combines them into that tuple. Keep each set identical but for size.
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

    /// <summary>
    ///     Scans 2 requests that come from separate providers, so each can be built in its own part of the pipeline, and returns
    ///     their results in parameter order. The same as the overload taking a tuple of requests, which this combines them into.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request1">The first request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request2">The second request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in parameter order; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request1,
        IncrementalValueProvider<ScanRequest> request2
    ) =>
        context.Scan(
            consumerId,
            request1
               .Combine(request2)
               .Select(static (pair, _) => (pair.Left, pair.Right))
        );

    /// <summary>
    ///     Scans 3 requests that come from separate providers, so each can be built in its own part of the pipeline, and returns
    ///     their results in parameter order. The same as the overload taking a tuple of requests, which this combines them into.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request1">The first request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request2">The second request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request3">The third request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in parameter order; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request1,
        IncrementalValueProvider<ScanRequest> request2,
        IncrementalValueProvider<ScanRequest> request3
    ) =>
        context.Scan(
            consumerId,
            request1
               .Combine(request2)
               .Combine(request3)
               .Select(static (pair, _) => (pair.Left.Left, pair.Left.Right, pair.Right))
        );

    /// <summary>
    ///     Scans 4 requests that come from separate providers, so each can be built in its own part of the pipeline, and returns
    ///     their results in parameter order. The same as the overload taking a tuple of requests, which this combines them into.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request1">The first request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request2">The second request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request3">The third request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request4">The fourth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in parameter order; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request1,
        IncrementalValueProvider<ScanRequest> request2,
        IncrementalValueProvider<ScanRequest> request3,
        IncrementalValueProvider<ScanRequest> request4
    ) =>
        context.Scan(
            consumerId,
            request1
               .Combine(request2)
               .Combine(request3)
               .Combine(request4)
               .Select(static (pair, _) => (pair.Left.Left.Left, pair.Left.Left.Right, pair.Left.Right, pair.Right))
        );

    /// <summary>
    ///     Scans 5 requests that come from separate providers, so each can be built in its own part of the pipeline, and returns
    ///     their results in parameter order. The same as the overload taking a tuple of requests, which this combines them into.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request1">The first request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request2">The second request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request3">The third request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request4">The fourth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request5">The fifth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in parameter order; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request1,
        IncrementalValueProvider<ScanRequest> request2,
        IncrementalValueProvider<ScanRequest> request3,
        IncrementalValueProvider<ScanRequest> request4,
        IncrementalValueProvider<ScanRequest> request5
    ) =>
        context.Scan(
            consumerId,
            request1
               .Combine(request2)
               .Combine(request3)
               .Combine(request4)
               .Combine(request5)
               .Select(static (pair, _) => (pair.Left.Left.Left.Left, pair.Left.Left.Left.Right, pair.Left.Left.Right, pair.Left.Right, pair.Right))
        );

    /// <summary>
    ///     Scans 6 requests that come from separate providers, so each can be built in its own part of the pipeline, and returns
    ///     their results in parameter order. The same as the overload taking a tuple of requests, which this combines them into.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request1">The first request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request2">The second request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request3">The third request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request4">The fourth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request5">The fifth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request6">The sixth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in parameter order; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request1,
        IncrementalValueProvider<ScanRequest> request2,
        IncrementalValueProvider<ScanRequest> request3,
        IncrementalValueProvider<ScanRequest> request4,
        IncrementalValueProvider<ScanRequest> request5,
        IncrementalValueProvider<ScanRequest> request6
    ) =>
        context.Scan(
            consumerId,
            request1
               .Combine(request2)
               .Combine(request3)
               .Combine(request4)
               .Combine(request5)
               .Combine(request6)
               .Select(static (pair, _) => (pair.Left.Left.Left.Left.Left, pair.Left.Left.Left.Left.Right, pair.Left.Left.Left.Right, pair.Left.Left.Right, pair.Left.Right, pair.Right))
        );

    /// <summary>
    ///     Scans 7 requests that come from separate providers, so each can be built in its own part of the pipeline, and returns
    ///     their results in parameter order. The same as the overload taking a tuple of requests, which this combines them into.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request1">The first request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request2">The second request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request3">The third request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request4">The fourth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request5">The fifth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request6">The sixth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request7">The seventh request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in parameter order; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request1,
        IncrementalValueProvider<ScanRequest> request2,
        IncrementalValueProvider<ScanRequest> request3,
        IncrementalValueProvider<ScanRequest> request4,
        IncrementalValueProvider<ScanRequest> request5,
        IncrementalValueProvider<ScanRequest> request6,
        IncrementalValueProvider<ScanRequest> request7
    ) =>
        context.Scan(
            consumerId,
            request1
               .Combine(request2)
               .Combine(request3)
               .Combine(request4)
               .Combine(request5)
               .Combine(request6)
               .Combine(request7)
               .Select(static (pair, _) => (pair.Left.Left.Left.Left.Left.Left, pair.Left.Left.Left.Left.Left.Right, pair.Left.Left.Left.Left.Right, pair.Left.Left.Left.Right, pair.Left.Left.Right, pair.Left.Right, pair.Right))
        );

    /// <summary>
    ///     Scans 8 requests that come from separate providers, so each can be built in its own part of the pipeline, and returns
    ///     their results in parameter order. The same as the overload taking a tuple of requests, which this combines them into.
    /// </summary>
    /// <param name="context">The generator's initialization context.</param>
    /// <param name="consumerId">The generator's consumer id, unique to it: ASCII letters, digits, '.', '_' and '-'.</param>
    /// <param name="request1">The first request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request2">The second request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request3">The third request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request4">The fourth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request5">The fifth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request6">The sixth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request7">The seventh request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <param name="request8">The eighth request, typically built with <see cref="ScanRequest.Build" /> from the compilation.</param>
    /// <returns>The results, in parameter order; the tuple compares by value, so the pipeline caches it.</returns>
    /// <exception cref="ArgumentException"><paramref name="consumerId" /> is empty or has other characters.</exception>
    public static IncrementalValueProvider<(ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult, ScanResult)> Scan(
        this IncrementalGeneratorInitializationContext context,
        string consumerId,
        IncrementalValueProvider<ScanRequest> request1,
        IncrementalValueProvider<ScanRequest> request2,
        IncrementalValueProvider<ScanRequest> request3,
        IncrementalValueProvider<ScanRequest> request4,
        IncrementalValueProvider<ScanRequest> request5,
        IncrementalValueProvider<ScanRequest> request6,
        IncrementalValueProvider<ScanRequest> request7,
        IncrementalValueProvider<ScanRequest> request8
    ) =>
        context.Scan(
            consumerId,
            request1
               .Combine(request2)
               .Combine(request3)
               .Combine(request4)
               .Combine(request5)
               .Combine(request6)
               .Combine(request7)
               .Combine(request8)
               .Select(static (pair, _) => (pair.Left.Left.Left.Left.Left.Left.Left, pair.Left.Left.Left.Left.Left.Left.Right, pair.Left.Left.Left.Left.Left.Right, pair.Left.Left.Left.Left.Right, pair.Left.Left.Left.Right, pair.Left.Left.Right, pair.Left.Right, pair.Right))
        );
}
