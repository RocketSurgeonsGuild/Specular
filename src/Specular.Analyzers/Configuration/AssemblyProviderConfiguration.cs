using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Specular.Analyzers.AssemblyProviders;
using Specular.Analyzers.Descriptors;
using Specular.Scanning;

namespace Specular.Analyzers.Configuration;

internal sealed partial class AssemblyProviderConfiguration
(
    HashSet<Diagnostic> reportedDiagnostics,
    ScanEnvironment environment,
    bool isAot,
    SpecularCache cache
)
{
    public Compilation Compilation { get; } = environment.Compilation;
    public bool IsAot { get; } = isAot;

    /// <summary>The build cache: scan results, the requests referenced assemblies carry, and what each call site generated.</summary>
    public SpecularCache Cache => cache;
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "A source generator must never crash the build; the unexpected exception is surfaced as a diagnostic.")]
    public (
        ImmutableList<AssemblyCollection.Item> InternalAssemblyRequests,
        ImmutableList<ReflectionCollection.Item> InternalReflectionRequests,
        ImmutableList<ResolvedSourceLocation> ReflectionSources,
        ImmutableList<ServiceDescriptorCollection.Item> InternalServiceDescriptorRequests,
        ImmutableList<ResolvedSourceLocation> ServiceDescriptorSources
        ) FromAssemblyAttributes(
            ref ImmutableDictionary<string, IAssemblySymbol> assemblySymbols,
            ImmutableList<ReflectionCollection.Item> reflectionRequests,
            ImmutableList<ServiceDescriptorCollection.Item> serviceDescriptorRequests,
            HashSet<Diagnostic> globalDiagnostics,
            CancellationToken cancellationToken
        )
    {
        var reflectionSources = ImmutableList.CreateBuilder<ResolvedSourceLocation>();
        var serviceDescriptorSources = ImmutableList.CreateBuilder<ResolvedSourceLocation>();

        var internalAssemblyRequestsBuilder = ImmutableList.CreateBuilder<AssemblyCollection.Item>();
        var internalReflectionRequestsBuilder = ImmutableList.CreateBuilder<ReflectionCollection.Item>();
        var internalServiceDescriptorRequestsBuilder = ImmutableList.CreateBuilder<ServiceDescriptorCollection.Item>();
        foreach (var assembly in assemblySymbols.Values.Except([Compilation.Assembly]).OrderBy(z => z.MetadataName))
        {
            try
            {
                GetAssemblyExpressionData(
                    assembly,
                    out var assemblyAssemblySources,
                    out var assemblyReflectionBuilder,
                    out var assemblyServiceDescriptorBuilder,
                    out var excludeFromResolution,
                    assemblySymbols
                );

                internalAssemblyRequestsBuilder.AddRange(assemblyAssemblySources);
                internalReflectionRequestsBuilder.AddRange(assemblyReflectionBuilder);
                internalServiceDescriptorRequestsBuilder.AddRange(assemblyServiceDescriptorBuilder);
                if (excludeFromResolution) assemblySymbols = assemblySymbols.Remove(assembly.MetadataName);
            }
            catch (Exception e)
            {
                _ = globalDiagnostics.Add(
                    Diagnostic.Create(
                        Diagnostics.UnhandledException,
                        assembly.Locations.FirstOrDefault(),
                        e.Message.Replace("\r", "").Replace("\n", ""),
                        e.StackTrace.Replace("\r", "").Replace("\n", ""),
                        e.GetType().Name,
                        e.ToString()
                    )
                );
            }
        }

        var internalReflectionRequests = internalReflectionRequestsBuilder.ToImmutable();
        var internalServiceDescriptorRequests = internalServiceDescriptorRequestsBuilder.ToImmutable();
        var allReflectionRequests = reflectionRequests.AddRange(internalReflectionRequests);
        var allServiceDescriptorRequests = serviceDescriptorRequests.AddRange(internalServiceDescriptorRequests);
        var diagnostics = new HashSet<Diagnostic>();
        foreach (var assembly in assemblySymbols.Values.Except([Compilation.Assembly]).OrderBy(z => z.MetadataName))
        {
            foreach (var request in allReflectionRequests)
            {
                var source = ReflectionCollection.ResolveSource(
                    this,
                    Compilation,
                    diagnostics,
                    request,
                    assembly,
                    cancellationToken
                );
                if (source is { }) reflectionSources.Add(source);
            }

            foreach (var request in allServiceDescriptorRequests)
            {
                var source = ServiceDescriptorCollection.ResolveSource(
                    this,
                    Compilation,
                    diagnostics,
                    request,
                    assembly,
                    cancellationToken
                );
                if (source is { }) serviceDescriptorSources.Add(source);
            }
        }

        globalDiagnostics.UnionWith(diagnostics);

        var result = (
            internalAssemblyRequestsBuilder.ToImmutable(),
            internalReflectionRequests,
            reflectionSources.ToImmutable(),
            internalServiceDescriptorRequests,
            serviceDescriptorSources.ToImmutable()
        );

        return result;
    }

    /// <summary>The [AssemblyMetadata] key and value each request is carried in, for assemblies that reference this one.</summary>
    public static IEnumerable<(string Key, string Value)> ToAssemblyAttributes(
        HashSet<Diagnostic> diagnostics,
        ImmutableList<AssemblyCollection.Item> assemblyRequests,
        ImmutableList<ReflectionCollection.Item> reflectionTypes,
        ImmutableList<ServiceDescriptorCollection.Item> serviceDescriptorTypes
    )
    {
        foreach (var request in assemblyRequests
                               .OrderBy(z => z.Location.FileName)
                               .ThenBy(z => z.Location.LineNumber)
                               .ThenBy(z => z.Location.ExpressionHash))
        {
            yield return (AssembliesKey, GetAssembliesToString(request));
        }

        foreach (var request in reflectionTypes
                               .OrderBy(z => z.Location.FileName)
                               .ThenBy(z => z.Location.LineNumber)
                               .ThenBy(z => z.Location.ExpressionHash))
        {
            yield return (ReflectionTypesKey, GetReflectionToString(request));
        }

        foreach (var request in serviceDescriptorTypes
                               .OrderBy(z => z.Location.FileName)
                               .ThenBy(z => z.Location.LineNumber)
                               .ThenBy(z => z.Location.ExpressionHash))
        {
            yield return (ServiceDescriptorTypesKey, GetServiceDescriptorToString(diagnostics, request));
        }
    }
    private static string CompressString(byte[] bytes) => Convert.ToBase64String(bytes);

    private static byte[] DecompressString(string base64String) => Convert.FromBase64String(base64String);

    private AssemblyCollection.Item GetAssembliesFromData(ImmutableDictionary<string, IAssemblySymbol> assemblySymbols, GetAssemblyConfiguration config) =>
        new(config.Assembly.Location, new(environment, AssemblySelection.Load(config.Assembly.Assembly, assemblySymbols), null));

    private AssemblyCollection.Item GetAssembliesFromString(ImmutableDictionary<string, IAssemblySymbol> assemblySymbols, string value)
    {
        var result = DecompressString(value);
        // ReSharper disable once NullableWarningSuppressionIsUsed
        var config = JsonSerializer.Deserialize(result, JsonSourceGenerationContext.Default.GetAssemblyConfiguration)!;
        Debug.Assert(config.Type == nameof(JsonSourceGenerationContext.Default.GetAssemblyConfiguration));
        return GetAssembliesFromData(assemblySymbols, config);
    }

    private static string GetAssembliesToString(AssemblyCollection.Item item)
    {
        var data = GetAssemblyCollectionData(item);
        var result = JsonSerializer.SerializeToUtf8Bytes(data, JsonSourceGenerationContext.Default.GetAssemblyConfiguration);
        return CompressString(result);
    }

    private static GetAssemblyConfiguration GetAssemblyCollectionData(AssemblyCollection.Item item) =>
        new(new(item.Location, item.Selection.Assemblies.Data));

    private void GetAssemblyExpressionData(
        IAssemblySymbol assembly,
        out ImmutableList<AssemblyCollection.Item> assemblyItems,
        out ImmutableList<ReflectionCollection.Item> reflection,
        out ImmutableList<ServiceDescriptorCollection.Item> serviceDescriptor,
        out bool excludeFromResolution,
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols
    )
    {
        if (cache.TryGetRequests(assembly, out var cachedData))
        {
            assemblyItems = cachedData.InternalAssemblyRequests.Select(z => GetAssembliesFromData(assemblySymbols, z)).ToImmutableList();
            reflection = cachedData.InternalReflectionRequests.Select(z => GetReflectionFromData(assemblySymbols, z)).ToImmutableList();
            serviceDescriptor = cachedData.InternalServiceDescriptorRequests.Select(z => GetServiceDescriptorFromData(assemblySymbols, z)).ToImmutableList();
            excludeFromResolution = cachedData.ExcludeFromResolution;
            return;
        }

        var assemblyBuilder = ImmutableList.CreateBuilder<AssemblyCollection.Item>();
        var reflectionBuilder = ImmutableList.CreateBuilder<ReflectionCollection.Item>();
        var serviceDescriptorBuilder = ImmutableList.CreateBuilder<ServiceDescriptorCollection.Item>();
        excludeFromResolution = false;
        var attributes = assembly.GetAttributes();
        foreach (var attribute in attributes)
        {
            if (attribute is { AttributeClass.MetadataName: "ExcludeFromSpecularAttribute" }) excludeFromResolution = true;

            if (attribute is not { AttributeClass.MetadataName: "AssemblyMetadataAttribute" }) continue;

            try
            {
                switch (attribute)
                {
                    case { ConstructorArguments: [{ Value: AssembliesKey }, { Value: string getAssembliesData }] }:
                        {
                            {
                                var data = GetAssembliesFromString(assemblySymbols, getAssembliesData);
                                assemblyBuilder.Add(data);
                            }
                            break;
                        }

                    case { ConstructorArguments: [{ Value: ReflectionTypesKey }, { Value: string reflectionData }] }:
                        {
                            {
                                var data = GetReflectionFromString(assemblySymbols, reflectionData);
                                reflectionBuilder.Add(data);
                            }
                            break;
                        }

                    case { ConstructorArguments: [{ Value: ServiceDescriptorTypesKey }, { Value: string serviceDescriptorData }] }:
                        {
                            {
                                var data = GetServiceDescriptorFromString(assemblySymbols, serviceDescriptorData);
                                serviceDescriptorBuilder.Add(data);
                            }
                            break;
                        }

                    default:
                        break;
                }
            }
            catch (InvalidOperationException)
            {
                //?
            }
            catch (JsonException)
            {
                //?
            }
        }

        assemblyItems = assemblyBuilder.ToImmutable();
        reflection = reflectionBuilder.ToImmutable();
        serviceDescriptor = serviceDescriptorBuilder.ToImmutable();

        // Assemblies without requests are kept too, so the next build skips reading their attributes as well.
        cache.SetRequests(
            assembly,
            new(
                assemblyBuilder.Select(GetAssemblyCollectionData).ToImmutableList(),
                reflectionBuilder.Select(GetReflectionCollectionData).ToImmutableList(),
                serviceDescriptorBuilder.Select(z => GetServiceDescriptorCollectionData(reportedDiagnostics, z)).ToImmutableList(),
                excludeFromResolution
            )
        );
    }

    private static GetReflectionCollectionData GetReflectionCollectionData(ReflectionCollection.Item item) =>
        new(new(item.Location, item.Selection.Assemblies.Data), new(item.Selection.Types!.Data));

    private ReflectionCollection.Item GetReflectionFromData(ImmutableDictionary<string, IAssemblySymbol> assemblySymbols, GetReflectionCollectionData config) =>
        new(config.Assembly.Location, LoadSelection(assemblySymbols, config.Assembly.Assembly, config.Reflection.Type));

    private ScanSelection LoadSelection(ImmutableDictionary<string, IAssemblySymbol> assemblySymbols, AssemblyFilterData assemblies, TypeFilterData types) =>
        new(environment, AssemblySelection.Load(assemblies, assemblySymbols), TypeSelection.Load(Compilation, types, assemblySymbols));

    private ReflectionCollection.Item GetReflectionFromString(
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols,
        string value
    )
    {
        var result = DecompressString(value);
        // ReSharper disable once NullableWarningSuppressionIsUsed
        var config = JsonSerializer.Deserialize(result, JsonSourceGenerationContext.Default.GetReflectionCollectionData)!;
        Debug.Assert(config.Type == nameof(JsonSourceGenerationContext.GetReflectionCollectionData));
        return GetReflectionFromData(assemblySymbols, config);
    }

    private static string GetReflectionToString(ReflectionCollection.Item item)
    {
        var data = GetReflectionCollectionData(item);
        var result = JsonSerializer.SerializeToUtf8Bytes(data, JsonSourceGenerationContext.Default.GetReflectionCollectionData);
        return CompressString(result);
    }

    private static GetServiceDescriptorCollectionData GetServiceDescriptorCollectionData(HashSet<Diagnostic> diagnostics, ServiceDescriptorCollection.Item item)
    {
        var data = new GetServiceDescriptorCollectionData(
            new(item.Location, item.Selection.Assemblies.Data),
            new(item.Selection.Types!.Data),
            new(LoadServiceDescriptorsData(diagnostics, item.ServicesTypeFilter), item.Lifetime)
        );
        return data;
    }

    private ServiceDescriptorCollection.Item GetServiceDescriptorFromData(
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols,
        GetServiceDescriptorCollectionData config
    )
    {
        var assemblyData = config.Assembly;
        var serviceDescriptorData = config.ServiceDescriptor;
        var selection = LoadSelection(assemblySymbols, assemblyData.Assembly, config.Reflection.Type);
        var servicesFilter = LoadServiceDescriptorFilter(Compilation, serviceDescriptorData.ServiceDescriptor, assemblySymbols);
        return new(assemblyData.Location, selection, servicesFilter, serviceDescriptorData.Lifetime);
    }

    private ServiceDescriptorCollection.Item GetServiceDescriptorFromString(
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols,
        string value
    )
    {
        var result = DecompressString(value);
        // ReSharper disable once NullableWarningSuppressionIsUsed
        var config = JsonSerializer.Deserialize(result, JsonSourceGenerationContext.Default.GetServiceDescriptorCollectionData)!;
        Debug.Assert(config.Type == nameof(JsonSourceGenerationContext.Default.GetServiceDescriptorCollectionData));
        return GetServiceDescriptorFromData(assemblySymbols, config);
    }

    private static string GetServiceDescriptorToString(HashSet<Diagnostic> diagnostics, ServiceDescriptorCollection.Item item)
    {
        var data = GetServiceDescriptorCollectionData(diagnostics, item);
        var result = JsonSerializer.SerializeToUtf8Bytes(data, JsonSourceGenerationContext.Default.GetServiceDescriptorCollectionData);
        return CompressString(result);
    }

    private static CompiledServiceTypeDescriptors LoadServiceDescriptorFilter(
        Compilation compilation,
        ServiceDescriptorFilterData data,
        ImmutableDictionary<string, IAssemblySymbol> assemblySymbols
    )
    {
        var resolver = new TypeReferenceResolver(compilation, assemblySymbols);
        var descriptors = ImmutableArray.CreateBuilder<IServiceTypeDescriptor>();
        foreach (var item in data.ServiceTypeDescriptors)
        {
            IServiceTypeDescriptor serviceTypeDescriptor = item switch
            {
                { Identifier: 'c', TypeData: { } typeData } when resolver.Resolve(TypeSelection.ToTypeReference(typeData.Assembly, typeData.Type, false)) is { } type =>
                    new CompiledServiceTypeDescriptor(type),
                { Identifier: 'c', TypeData: { } typeData } => new UnknownCompiledServiceTypeDescriptor(typeData),
                { Identifier: 'i', TypeFilter: { } typeFilter } =>
                    new ImplementedInterfacesServiceTypeDescriptor(TypeSelection.Load(compilation, typeFilter, assemblySymbols)),
                { Identifier: 'i' } => new ImplementedInterfacesServiceTypeDescriptor(null),
                { Identifier: 'm' } => new MatchingInterfaceServiceTypeDescriptor(),
                { Identifier: 's' } => new SelfServiceTypeDescriptor(),
                { Identifier: 'a' } => new AsTypeFilterServiceTypeDescriptor(),
                _ => throw new ArgumentOutOfRangeException(nameof(data), data, $"The type name was {data.GetType().FullName}"),
            };
            descriptors.Add(
                serviceTypeDescriptor
            );
        }

        return new(descriptors.ToImmutable(), data.Lifetime);
    }

    private static ServiceDescriptorFilterData LoadServiceDescriptorsData(
        HashSet<Diagnostic> diagnostics,
        CompiledServiceTypeDescriptors serviceTypeDescriptors
    )
    {
        var serviceDescriptors = serviceTypeDescriptors
                                .ServiceTypeDescriptors
                                .Where(z =>
                                       {
                                           if (z is not UnknownCompiledServiceTypeDescriptor descriptor)
                                           {
                                               return true;
                                           }

                                           _ = diagnostics.Add(Diagnostic.Create(Diagnostics.CouldNotFindServiceType, null, descriptor.Data.Assembly, descriptor.Data.Type));
                                           return false;
                                       }
                                 )
                                .Select(descriptor => descriptor switch
                                                      {
                                                          ImplementedInterfacesServiceTypeDescriptor i => new(
                                                              'i',
                                                              TypeFilter: i is { InterfaceFilter: { } filter } ? filter.Data : null
                                                          ),
                                                          MatchingInterfaceServiceTypeDescriptor => new('m'),
                                                          SelfServiceTypeDescriptor => new('s'),
                                                          AsTypeFilterServiceTypeDescriptor => new('a'),
                                                          CompiledServiceTypeDescriptor { Type: { } namedType } => new ServiceTypeData(
                                                              'c',
                                                              new(namedType.ContainingAssembly.MetadataName, namedType.MetadataName, namedType.IsUnboundGenericType)
                                                          ),
                                                          _ => throw new ArgumentOutOfRangeException(nameof(descriptor), descriptor, $"The type name was {descriptor.GetType().FullName}"),
                                                      }
                                 );
        return new(serviceDescriptors.ToImmutableArray(), serviceTypeDescriptors.Lifetime);
    }

    private const string AssembliesKey = "AssemblyProvider.GetAssemblies";
    private const string ReflectionTypesKey = "AssemblyProvider.ReflectionTypes";
    private const string ServiceDescriptorTypesKey = "AssemblyProvider.ServiceDescriptorTypes";
}
